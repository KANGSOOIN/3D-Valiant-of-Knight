using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; // ���� SceneManager�� ����Ƽ ���� SceneManager�� ��ħ ����

public class SceneManager : MonoBehaviour
{
	private LoadingScene loadingScene;

	private BaseScene curScene;
	public BaseScene CurScene
	{
		get
		{
			if (curScene == null)
				curScene = GameObject.FindObjectOfType<BaseScene>();

			return curScene;
		}
	}

	private void Awake()
	{
		LoadingScene loadingScene = Resources.Load<LoadingScene>("Scenes/LoadingScene");
		this.loadingScene = Instantiate(loadingScene);
		this.loadingScene.transform.SetParent(transform);
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadingRoutine(sceneName));
	}

	private IEnumerator LoadingRoutine(string sceneName)
	{
		loadingScene.SetProgress(0f);
		loadingScene.FadeOut();
		yield return new WaitForSeconds(0.5f); 
		Time.timeScale = 0f;  //�ε� �߿��� ������ �ð��� ������

		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		while (!oper.isDone)
		{
			loadingScene.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
			yield return null;

			Debug.Log("���� ���� ��ġ");
			loadingScene.SetProgress(0.6f);
			yield return new WaitForSecondsRealtime(1f); // ���� �ð��� ���������� ���� �ð���ŭ �귯���� �� 

			Debug.Log("���ҽ� �ҷ�����");
			loadingScene.SetProgress(0.7f);
			yield return new WaitForSecondsRealtime(1f);

			Debug.Log("Ǯ��");
			loadingScene.SetProgress(0.8f);
			yield return new WaitForSecondsRealtime(1f);


			Debug.Log("���� ������ ��ġ");
			loadingScene.SetProgress(0.9f);
			yield return new WaitForSecondsRealtime(1f);

			Debug.Log("���� �� ����");
			loadingScene.SetProgress(1.0f);
			yield return new WaitForSecondsRealtime(1f);
		}

		CurScene.LoadAsync();
		if (CurScene != null)
		{
			CurScene.LoadAsync();
			while (CurScene.progress < 1f)
			{
				loadingScene.SetProgress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
				yield return null;
			}
		}

		loadingScene.SetProgress(1f);
		loadingScene.FadeIn();
		Time.timeScale = 1f;
		yield return new WaitForSeconds(0.5f);
	}
}