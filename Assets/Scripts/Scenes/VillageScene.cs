using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VillageScene : BaseScene
{
	//public GameObject playerPrefabs;
	//public Transform playerPosition;

	private void Awake()
	{
		Debug.Log("TitleScene Init");		
	}

	protected override IEnumerator LoadingRoutine()
	{
		// UI ��ε�
		ReLoadUI();
		Debug.Log("UI ��ε�");
		progress = 0.0f;
		//// ���� �ð��� ���������� ���� �ð���ŭ �귯���� ��
		yield return new WaitForSecondsRealtime(1f);

		ReLoadPool();
		Debug.Log("Pool ��ε�");
		progress = 0.2f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("���� ������ ����");
		progress = 0.4f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("�÷��̾� ��ġ");
		progress = 0.6f;
		//GameObject player = Instantiate(playerPrefabs);
		//player.transform.position = playerPosition.position;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("�ε� �Ϸ�");
		progress = 1.0f;
	}

	private void ReLoadUI()
	{
		GameManager.UI.UIRestart();
	}

	private void ReLoadPool()
	{
		GameManager.Pool.PoolRestart();
	}
}
