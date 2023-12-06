using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
	/// <summary>
	/// UIInventoryItem Ŭ������ �κ��丮 UI���� ���� ������ ������
	/// </summary>
	public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image itemImage; // ������ �̹���
		[SerializeField] private TMP_Text quantityTxt; // ������ ����
		[SerializeField] private Image borderImage; // ���õ� �������� �����ϱ� ���� �׵θ� �̹���

		// ������ �̺�Ʈ�� ó���ϱ� ���� ��������Ʈ�� �̺�Ʈ
		public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

        private bool empty = true; // ������ ������ ����ִ��� ���� Ȯ��

		public void Awake()
        {
            ResetData();
            Deselect();
        }

		// ������ ������ �����͸� �ʱ�ȭ
		public void ResetData()
        {
			itemImage.gameObject.SetActive(false);
			empty = true;
        }

		// ������ ������ ������ ����
		public void Deselect()
        {
			// �׵θ� �̹��� ��Ȱ��ȭ
			borderImage.enabled = false;
        }

		// ������ ���Կ� �����͸� ����
		public void SetData(Sprite sprite, int quantity)
        {

            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            empty = false;
        }

		// ������ ������ ����
		public void Select()
        {
			// �׵θ� �̹��� ��Ȱ��ȭ
			borderImage.enabled = true;
        }

		// ���콺 ������ Ŭ�� �̺�Ʈ�� ó��
		public void OnPointerClick(PointerEventData pointerData)
        {
			// ���콺 ������ ��ư�� Ŭ���� ��� ������ ���콺 Ŭ�� �̺�Ʈ �߻��ϰ� �� �ܿ��� �Ϲ� Ŭ�� �̺�Ʈ �߻�
			if (pointerData.button == PointerEventData.InputButton.Right)
            {
				OnRightMouseButtonClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

		// �巡�װ� ������ �� �߻��ϴ� �̺�Ʈ�� ó��
		public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

		// �巡�װ� ���۵� �� �߻��ϴ� �̺�Ʈ�� ó��
		public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

		// �ٸ� ���� ���� �������� �巡�׵Ǿ��� �� �߻��ϴ� �̺�Ʈ�� ó��
		public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

		// �������� �巡�׵Ǵ� ���� ���������� �߻��ϴ� �̺�Ʈ�� ó��
		public void OnDrag(PointerEventData eventData) { }
    }
}