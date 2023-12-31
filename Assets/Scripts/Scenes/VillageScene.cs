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
		// UI 재로딩
		ReLoadUI();
		Debug.Log("UI 재로딩");
		progress = 0.0f;
		//// 게임 시간은 멈춰있으니 실제 시간만큼 흘러가게 함
		yield return new WaitForSecondsRealtime(1f);

		ReLoadPool();
		Debug.Log("Pool 재로딩");
		progress = 0.2f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("랜덤 아이템 생성");
		progress = 0.4f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("플레이어 배치");
		progress = 0.6f;
		//GameObject player = Instantiate(playerPrefabs);
		//player.transform.position = playerPosition.position;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("로딩 완료");
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
