using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Scene : BaseScene
{
	//[SerializeField] GameObject playerPrefabs;
	//[SerializeField] GameObject petPrefabs;
	//[SerializeField] Transform playerSpawnPointPosition;
	//[SerializeField] Transform petSpawnPointPosition;

	protected override IEnumerator LoadingRoutine()
	{
		// ���� ����ũ �ε�
		GameManager.UI.UIRestart();
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
		//player.transform.position = playerSpawnPointPosition.position;
		//GameObject pet = Instantiate(petPrefabs);
		//pet.transform.position = playerSpawnPointPosition.position;

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