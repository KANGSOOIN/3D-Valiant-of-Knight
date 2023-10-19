using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableHeaderUI : MonoBehaviour, IDragHandler, IPointerDownHandler
{
	[SerializeField] private Transform tartgetTransform; // UI

	private Vector2 startPoint;
	private Vector2 movePoint;

	private void Awake()
	{
		// �̵� ��� UI�� �������� ���� ���, �ڵ����� �θ�� �ʱ�ȭ
		if (tartgetTransform == null)
		tartgetTransform = transform.parent;
	}

	// �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		tartgetTransform.position = startPoint + (eventData.position - movePoint);
	}

	// �巡�� ���� ��ġ ����
	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		startPoint = tartgetTransform.position;
		movePoint = eventData.position;
	}
}
