using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI triggerText;
	[SerializeField] private GameObject dialogUI;
	[SerializeField] private TextMeshProUGUI dialogText;
	[SerializeField] private TextMeshProUGUI nPCNameText;
	[SerializeField] private GameObject quickSlots;

	[SerializeField, Multiline] private string npcName;
	[SerializeField] private string[] sentences;
	[SerializeField] private int currentSentence;

	private bool isTriggered;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			triggerText.gameObject.SetActive(true);
			triggerText.text = npcName + " �� ��ȭ�Ϸ��� [ E ] Ű�� ��������";
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!isTriggered)
			{
				triggerText.text = "";
				triggerText.gameObject.SetActive(false);
				dialogUI.gameObject.SetActive(false);
			}			
		}
	}

	private void Update()
	{
		TalkToNPC();		
	}

	private void TalkToNPC()
	{
		if (isTriggered && Input.GetKeyDown(KeyCode.E))
		{
			triggerText.text = "";
			triggerText.gameObject.SetActive(false);
			dialogUI.gameObject.SetActive(true);
			DialogSystem.Dialog.SetNPCName(npcName);
			DialogSystem.Dialog.ActivateDialog(sentences);
		}
	}
}
