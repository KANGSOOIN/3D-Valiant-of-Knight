using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
	[SerializeField] private GameObject triggerText;
	[SerializeField] private GameObject dialogUI;
	[SerializeField] private bool canAtivateBox;

	[SerializeField, Multiline] private string npcName;
	[SerializeField] private string[] sentences;
	[SerializeField] private int currentSentence;

	private void Update()
	{
		TalkToNPC();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canAtivateBox = true;
			triggerText.gameObject.SetActive(true);			
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			canAtivateBox = false;
			triggerText.gameObject.SetActive(false);			
			dialogUI.SetActive(false);
		}
	}

	private void TalkToNPC()
	{
		if (canAtivateBox && Input.GetKeyDown(KeyCode.C))
		{
			triggerText.gameObject.SetActive(false);
			dialogUI.SetActive(true);
			canAtivateBox = true;
			DialogSystem.Dialog.SetNPCName(npcName);
			DialogSystem.Dialog.ActivateDialog(sentences);
		}
	}
}
