using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; // ���� SceneManager�� ����Ƽ ���� SceneManager�� ��ħ ����

// �� �� ���� �ִ� BaseScene ã�Ƽ� ������ ��
public class SceneManager : MonoBehaviour
{
	private LoadingUI loadingUI;

	private BaseScene curScene;
	public BaseScene CurScene
	{
		get
		{
			// FindObjectOfType�� ���� ���� �δ��� �Ǵϱ� Null�� Ȯ����
			if (curScene == null)
				curScene = GameObject.FindObjectOfType<BaseScene>(); 

			return curScene;
		}
	}

	private void Awake()
	{
		LoadingUI loadingUI = GameManager.Resource.Load<LoadingUI>("UI/LoadingUI");
		loadingUI = Instantiate(loadingUI);
		loadingUI.transform.SetParent(transform, false);
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadingRoutine(sceneName));
	}

	private IEnumerator LoadingRoutine(string sceneName)
	{
		loadingUI.SetProgress(0f);
		loadingUI.FadeOut();
		yield return new WaitForSeconds(0.5f);
		//�ε� �߿��� ������ �ð��� ������
		Time.timeScale = 0f;  

		// �񵿱�� �ε�
		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		
		while (!oper.isDone)
		{
			loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
			yield return null;		
		}

		CurScene.LoadAsync();
		while (CurScene.progress < 1f)
		{
			loadingUI.SetProgress(Mathf.Lerp(0.5f, 1.0f, CurScene.progress));
			yield return null;
		}

		//�ε� �߿��� ������ �ð��� ���� �� ����
		Time.timeScale = 1f;
		loadingUI.FadeIn();
		yield return new WaitForSeconds(1f);
	}
}