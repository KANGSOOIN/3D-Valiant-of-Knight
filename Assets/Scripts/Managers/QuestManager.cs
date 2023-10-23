using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questNames;
    [SerializeField] bool[] questMarkerCompleted;

	private void Start()
	{
		questMarkerCompleted = new bool[questNames.Length];
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			print(CheckIfComplete("Map1�� ������ ���� 5���� ����ϱ�"));
			MarkQuestComplete("Map2�� ���̷��� ���� 10���� ����ϱ�");
			MarkQuestIncomplete("Map3��  ���� ���� 10���� ����ϱ�");
		}
	}

	public int GetQuestNumber(string questToFind)
	{
		for (int i = 0; i < questNames.Length; i++)
		{
			if (questNames[i] == questToFind)
			{ 
				return i;
			}
		}

		Debug.LogWarning("Quest: " + questToFind + "dose not exist");
		return 0;
	}

	public bool CheckIfComplete(string questToCheck)
	{ 
		int questNumberToCheck = GetQuestNumber(questToCheck);

		if (questNumberToCheck != 0)
		{
			return questMarkerCompleted[questNumberToCheck];
		}

		return false;
	}

	public void MarkQuestComplete(string questToMark)
	{
		int questNumberToCheck = GetQuestNumber(questToMark);
		questMarkerCompleted[questNumberToCheck] = true;
	}

	public void MarkQuestIncomplete(string questToMark)
	{
		int questNumberToCheck = GetQuestNumber(questToMark);
		questMarkerCompleted[questNumberToCheck] = false;
	}
}
