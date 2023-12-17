using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Quest/QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
	[SerializeField] private List<Quest> quests;

	public IReadOnlyList<Quest> Quests => quests;

	public Quest FindQuestBy(string codeName) => quests.FirstOrDefault(x => x.CodeName == codeName);

	// 퀘스트 목록을 찾아 오는 기능
#if UNITY_EDITOR
	[ContextMenu("FindQuests")]
	private void FindQuests()
	{
		FindQuestsBy<Quest>();
	}

	[ContextMenu("FindAchievements")]
	private void FindAchievements()
	{
		FindQuestsBy<Achievement>();
	}

	private void FindQuestsBy<T>() where T : Quest
	{
		quests = new List<Quest>();

		// GUID를 통해 가져옴
		string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
		foreach (var guid in guids)
		{
			string assetPath = AssetDatabase.GUIDToAssetPath(guid);
			var quest = AssetDatabase.LoadAssetAtPath<T>(assetPath);

			if (quest.GetType() == typeof(T))
				quests.Add(quest);
			// SerializeField 변수가 변화가 생겼으니, 에셋을 저장할 때 반영함
			EditorUtility.SetDirty(this);
			// 에셋 저장
			AssetDatabase.SaveAssets();
		}
	}
#endif
}
