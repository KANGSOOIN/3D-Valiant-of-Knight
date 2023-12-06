using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct InventoryItem
{
	public int quantity; // ������ ����
	public ItemData item; // ������ ������
	public List<ItemParameter> itemState; // ������ ���� ����Ʈ
	public bool IsEmpty => item == null; // �������� ����ִ��� ����

	// ������ ������ ���ο� �κ��丮 ������ ��ȯ
	public InventoryItem ChangeQuantity(int newQuantity)
	{
		return new InventoryItem
		{
			item = this.item,
			quantity = newQuantity,
			itemState = new List<ItemParameter>(this.itemState)
		};
	}

	// �� �κ��丮 ������ ��ȯ
	public static InventoryItem GetEmptyItem() => new InventoryItem
	{
		item = null,
		quantity = 0,
		itemState = new List<ItemParameter>()
	};
}

namespace Inventory
{
    [CreateAssetMenu]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems; // �κ��丮 ������ ����Ʈ
		[field: SerializeField] public int Size { get; private set; } // �κ��丮 ũ��

		// �κ��丮 ���� �̺�Ʈ
		public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

		// �κ��丮 �ʱ�ȭ
		public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

		// ������ �߰�
		public int AddItem(ItemData item, int quantity, List<ItemParameter> itemState = null)
        {
			// ��ø �Ұ����� �������� ���
			if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
					// ���� ������ �ְ�, �κ��丮�� ���� ���� �ʾ��� ��
					while (quantity > 0 && IsInventoryFull() == false)
                    {
						// �� ���Կ� �������� �߰��ϰ�, �߰��� ������ ��
						quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChange();
					return quantity;
                }
            }
			// ��ø ������ �������� ���
			quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

		// ù ��° �� ���Կ� ������ �߰�
		private int AddItemToFirstFreeSlot(ItemData item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = 
                new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

		// �κ��丮�� ���� á���� Ȯ��
		private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

		// ��ø ������ ������ �߰�
		private int AddStackableItem(ItemData item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
						// ��ø ������ �ִ� �������� �������� �߰��ϰ� ���� ������ �����
						inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
						// ���� ������ ��ø ������ �ִ� �������� ���� ��� ��ø�� ������ �����ϰ� ������
						inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
			// ���� ���� ������ �ִٸ�, �� ���Կ� ��ø ������ �ִ� �������� �������� �߰���
			while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);

                InformAboutChange();
            }
        }

		// �κ��丮 ������ �߰�
		public void AddItem(InventoryItem item)
        {
			// AddItem �޼��带 ���� ������ �߰�
			AddItem(item.item, item.quantity);
        }

		// ���� �κ��丮 ���� ��ȯ
		public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

			// �κ��丮�� ��ȸ�ϸ鼭 ������� ���� ���� ������ ��ȯ���� �߰�
			for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

		// Ư�� �ε����� �κ��丮 ������ ��ȯ
		public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

		// �� ������ ���� ��ȯ
		public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
			// ù ��° ������ ������ �ӽ÷� ����
			InventoryItem item1 = inventoryItems[itemIndex_1];
			// �� ������ ������ ��ȯ
			inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

		// �κ��丮 ���� �̺�Ʈ ȣ��
		private void InformAboutChange()
        {
		    // ��ϵ� �̺�Ʈ �����ʿ��� ���� �κ��丮 ���¸� �˸�
			OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
}
