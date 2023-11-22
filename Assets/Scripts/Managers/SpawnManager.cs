using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public List<Transform> points = new List<Transform>(); // ���Ͱ� ������ ��ġ�� ������ List Ÿ�� ����
	public List<GameObject> monsterPool = new List<GameObject>(); // ���͸� �̸� ������ ������ ����Ʈ �ڷ���
	public int maxMonsters = 10; // ������Ʈ Ǯ(Object Pool)�� ������ ������ �ִ� ����
	public List<GameObject> monster; // ���� �������� ������ ����
	public float createTime = 1f; // ������ ���� ����

	[Header("UI")]
	public TMP_Text scoreText;
	public int totalScore;
	public int score;

	void Start()
	{
		// ���� ������Ʈ Ǯ ����
		CreateMonsterPool();

		// SpawnPointGroup ���ӿ�����Ʈ�� Transform ������Ʈ ����
		Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

		// SpawnPointGroup ������ �ִ� ��� ���ϵ� ���ӿ�����Ʈ�� Transform ������Ʈ ����
		foreach (Transform point in spawnPointGroup)
		{
			points.Add(point);
		}

		// ������ �ð� �������� �Լ��� ȣ��
		InvokeRepeating("CreateMonster", 2.0f, createTime);

		// ���ھ� ���� ���
		totalScore = PlayerPrefs.GetInt("Total_Score", 0);
		DisplayScore(0);
	}

	void CreateMonster()
	{
		// ������ �ұ�Ģ�� ���� ��ġ ����
		int index = Random.Range(0, points.Count);

		// ������Ʈ Ǯ���� ���� ����
		GameObject _monster = GetMonsterInPool();
		// ������ ������ ��ġ�� ȸ���� ����
		_monster?.transform.SetPositionAndRotation(points[index].position, points[index].rotation);

		// ������ ���͸� Ȱ��ȭ
		_monster?.SetActive(true);
	}

	// ������Ʈ Ǯ�� ���� ����
	void CreateMonsterPool()
	{
		for (int i = 0; i < maxMonsters; i++)
		{
			// �ʱ�ȭ
			GameObject _monster = null;

			// 3���� ������ ���͸� �����Ͽ� ���� Ǯ�� �߰�
			if (i % 3 == 0)
			{
				_monster = GameManager.Resource.Instantiate(monster[0]);
				_monster.name = $"MonsterType1_{i / 3:00}";
			}
			else if (i % 3 == 1)
			{
				_monster = GameManager.Resource.Instantiate(monster[1]);
				_monster.name = $"MonsterType2_{i / 3:00}";
			}
			else if (i % 3 == 2)
			{
				_monster = GameManager.Resource.Instantiate(monster[2]);
				_monster.name = $"MonsterType3_{i / 3:00}";
			}

			// ������Ʈ Ǯ�� �߰��� ���ʹ� �������ڸ��� ��Ȱ��ȭ ��
			// -> ������ ����� �� Ȱ��ȭ ��                                         
			_monster.SetActive(false);
			monsterPool.Add(_monster);
		}

	}

	// ������Ʈ Ǯ���� ��� ������ ���͸� ������ ��ȯ�ϴ� �Լ�
	public GameObject GetMonsterInPool()
	{
		// ������Ʈ Ǯ�� ó������ ������ ��ȸ
		foreach (var _monster in monsterPool)
		{
			// ��Ȱ��ȭ ���η� ��� ������ ���͸� �Ǵ�
			if (_monster.activeSelf == false)
			{
				// ���� ��ȯ5
				return _monster;
			}
		}
		return null;
	}

	public void DisplayScore(int score)
	{
		totalScore += score;
		scoreText.text = "SCORE   :   " + totalScore.ToString();
		PlayerPrefs.SetInt("Total_Score", totalScore);
	}
}
