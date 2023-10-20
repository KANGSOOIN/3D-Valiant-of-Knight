using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class ItemData : ScriptableObject
{
	public int ID => id;
	public string ItemName => itmeName;
	public string Tooltip => tooltip;
	public Sprite IconSprite => iconSprite;

	[SerializeField] private int id;
	[SerializeField] private string itmeName;    // ������ �̸�
	[Multiline]
	[SerializeField] private string tooltip; // ������ ����
	[SerializeField] private Sprite iconSprite; // ������ ������
	[SerializeField] private GameObject dropItemPrefab; // �ٴڿ� ������ �� ������ ������

	public abstract Item CreateItem();
}
