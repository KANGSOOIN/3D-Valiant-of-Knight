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
			if (curScene == null)
				curScene = GameObject.FindObjectOfType<BaseScene>(); // FindObjectOfType�� ���� ���� �δ��� �Ǵϱ� Null�� Ȯ����

			return curScene;
		}
	}

	private void Awake()
	{
		LoadingUI loadingUI = GameManager.Resource.Instantiate<LoadingUI>("UI/LoadingUI");
		this.loadingUI = Instantiate(loadingUI);
		this.loadingUI.transform.SetParent(transform);
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
		Time.timeScale = 0f;  //�ε� �߿��� ������ �ð��� ������

		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		while (!oper.isDone)
		{
			loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
			yield return null;

			Debug.Log("���� ���� ��ġ");
			loadingUI.SetProgress(0.6f);
			yield return new WaitForSecondsRealtime(1f); // ���� �ð��� ���������� ���� �ð���ŭ �귯���� �� 

			Debug.Log("���ҽ� �ҷ�����");
			loadingUI.SetProgress(0.7f);
			yield return new WaitForSecondsRealtime(1f);

			Debug.Log("Ǯ��");
			loadingUI.SetProgress(0.8f);
			yield return new WaitForSecondsRealtime(1f);


			Debug.Log("���� ������ ��ġ");
			loadingUI.SetProgress(0.9f);
			yield return new WaitForSecondsRealtime(1f);

			Debug.Log("���� �� ����");
			loadingUI.SetProgress(1.0f);
			yield return new WaitForSecondsRealtime(1f);
		}

		//CurScene.LoadAsync();
		//if (CurScene != null)
		//{
		//	CurScene.LoadAsync();
		//	while (CurScene.progress < 1f)
		//	{
		//		loadingScene.SetProgress(Mathf.Lerp(0.5f, 1f, CurScene.progress));
		//		yield return null;
		//	}
		//}

		loadingUI.SetProgress(1f);
		loadingUI.FadeIn();
		Time.timeScale = 1f;
		yield return new WaitForSeconds(0.5f);
	}
}