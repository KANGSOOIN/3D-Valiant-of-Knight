using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	[SerializeField] private GameObject equipmentUI;
	[SerializeField] private bool isOpen;

	private void Start()
	{
		// ���� �ÿ��� ���â�� ���� ����
		isOpen = false;
	}

	void Update()
	{
		// ���â ���� �ݴ� ����Ű : I
		if (Input.GetKeyDown(KeyCode.E) && !isOpen)
		{
			Debug.Log("E�� ������, Open Equipment");
			equipmentUI.SetActive(true);
			isOpen = true;
		}
		else if (Input.GetKeyDown(KeyCode.E) && isOpen)
		{
			Debug.Log("E�� ������, Close Equipment");
			equipmentUI.SetActive(false);
			isOpen = false;
		}
	}
}
