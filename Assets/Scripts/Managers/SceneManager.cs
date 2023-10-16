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

		// �߰����� ������ �غ��� �ε��� �����ϰ� �Ѿ�� ��
		// ���� ����ũ �ε�
		Debug.Log("���� ���� ��ġ");
		loadingUI.SetProgress(0.6f);
		//// ���� �ð��� ���������� ���� �ð���ŭ �귯���� ��
		yield return new WaitForSecondsRealtime(1f);

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

		//�ε� �߿��� ������ �ð��� ���� �� ����
		Time.timeScale = 1f;
		loadingUI.FadeIn();
		yield return new WaitForSeconds(1f);
	}
}