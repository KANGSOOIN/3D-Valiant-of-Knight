using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private TMP_Text AmountText;

	private Item _item;
	public Item item
	{
		get { return _item; }
		set
		{
			_item = value;
			// AddItem()�� FreshSlot() �Լ����� ���
			// Inventory�� List<Item> items�� ��ϵ� �������� �ִٸ� itemImage�� image�� ���� �׸��� Image�� ���� ���� 1�� �Ͽ� �̹����� ǥ��
			// ���� item�� null �̸�(�󽽷� �̸�) Image�� ���� �� 0�� �־� ȭ�鿡 ǥ������ ����
			if (_item != null)
			{
				image.sprite = item.itemIcon;
				image.color = new Color(1, 1, 1, 1);
			}
			else
			{
				image.color = new Color(1, 1, 1, 0);
			}
		}
	}

}
