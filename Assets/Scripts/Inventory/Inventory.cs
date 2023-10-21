using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private GameObject inventoryUI;
	[SerializeField] private bool isOpen;

	private void Start()
	{
		// ���� �ÿ��� �κ��丮�� ���� ����
		isOpen = false;
	}

	void Update()
	{
		// �κ��丮 ���� �ݴ� ����Ű : I
		if (Input.GetKeyDown(KeyCode.I) && !isOpen)
		{
			Debug.Log("I�� ������, Open Inventory");
			inventoryUI.SetActive(true);
			isOpen = true;
		}
		else if (Input.GetKeyDown(KeyCode.I) && isOpen)
		{
			Debug.Log("I�� ������, Close Inventory");
			inventoryUI.SetActive(false);
			isOpen = false;
		}
	}
}
