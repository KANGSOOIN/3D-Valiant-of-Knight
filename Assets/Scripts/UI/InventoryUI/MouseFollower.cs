using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas; // UI ĵ����
	[SerializeField] private UIInventoryItem item; // UI �κ��丮 ������

	public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
		// UI �κ��丮 �����ۿ� ������ ����
		item.SetData(sprite, quantity);
    }

    void Update()
    {
		// ���콺 ��ġ�� ȭ�� ��ǥ���� ĵ���� ���� ��ǥ�� ��ȯ
		Vector2 position;
		// ĵ������ RectTransform, ���� ���콺 ��ġ, ĵ������ ���� ī�޶�
		RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
		// ��ȯ�� ���� ��ǥ�� ���� ��ǥ�� ��ȯ�Ͽ� ���콺 �ȷο��� ��ġ�� ����
		transform.position = canvas.transform.TransformPoint(position);
    }

	// Ȱ��ȭ/��Ȱ��ȭ ��� �Լ�
	public void Toggle(bool value)
    {
		// Ȱ��ȭ ���ο� ���� ���� ������Ʈ Ȱ��ȭ/��Ȱ��ȭ
		Debug.Log($"Item toggled {value}");
        gameObject.SetActive(value);
    }
}
