using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	private static PoolManager pool;
	private static ResourceManager resource;
	//private static UIManager ui;
	private static SoundManager sound;
	private static SceneManager scene;
	

	public static GameManager Instance { get { return instance; } }
	public static PoolManager Pool { get { return pool; } }
	public static ResourceManager Resource { get { return resource; } }
	//public static UIManager UI { get { return ui; } }
	public static SoundManager Sound { get { return sound; } }
	public static SceneManager Scene { get { return scene; } }
	

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);
		InitManagers();
	}

	private void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}

	private void InitManagers()
	{
		GameObject resourceObj = new GameObject();
		resourceObj.name = "ResourceManager";
		resourceObj.transform.parent = transform;
		resource = resourceObj.AddComponent<ResourceManager>();

		GameObject poolObj = new GameObject();
		poolObj.name = "PoolManager";
		poolObj.transform.parent = transform;
		pool = poolObj.AddComponent<PoolManager>();

		//GameObject uiObj = new GameObject();
		//uiObj.name = "UIManager";
		//uiObj.transform.parent = transform;
		//ui = uiObj.AddComponent<UIManager>();

		GameObject soundObj = new GameObject();
		soundObj.name = "SoundManager";
		soundObj.transform.parent = transform;
		sound = soundObj.AddComponent<SoundManager>();

		GameObject sceneObj = new GameObject();
		sceneObj.name = "SceneManager";
		sceneObj.transform.parent = transform;
		scene = sceneObj.AddComponent<SceneManager>();
	}
}
