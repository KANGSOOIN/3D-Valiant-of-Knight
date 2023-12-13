using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Dragging;
using Inventories;

namespace UI.Inventories
{
	/// <summary>
	/// �������� ���� �������� �� �����Ǵ� �Ⱦ��� ó���ϸ�,
	/// �� ���� ���� ������ ȣ���
	/// </summary>
	public class InventoryDropTarget : MonoBehaviour, IDragDestination<InventoryItem>
    {
        public void AddItems(InventoryItem item, int number)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ItemDropper>().DropItem(item, number);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }
    }
}