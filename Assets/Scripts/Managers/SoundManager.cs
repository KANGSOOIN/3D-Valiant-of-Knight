using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolum;
    private AudioSource bgmPlayer;
	private AudioHighPassFilter bgmEffect;
	
	[Header("SFX")]
	public AudioClip[] sfxClips;
	public float sfxVolum;
	public int channels;
	private AudioSource[] sfxPlayers;
	private int channelIndex;

	[Header("Volum Slider")]
	[SerializeField] Slider volumSlider;

	public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win}

	private void tart()
	{
		if (!PlayerPrefs.HasKey("musicVolume"))
		{
			PlayerPrefs.SetFloat("musicVolume", 1);
			Load();
		}
		else
		{
			Load();
		}
	}

	private void Awake()
	{
		//SoundInit();
	}

	//private void SoundInit()
	//{
	//	// ����� �÷��̾� �ʱ�ȭ
	//	GameObject bgmObject = new GameObject("BgmPlayer");
	//	bgmObject.transform.parent = transform;
	//	bgmPlayer = bgmObject.AddComponent<AudioSource>();
	//	bgmPlayer.playOnAwake = false;
	//	bgmPlayer.volume = bgmVolum;
	//	bgmPlayer.clip = bgmClip;
	//	bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

	//	// ȿ���� �÷��̾� �ʱ�ȭ
	//	GameObject sfxObject = new GameObject("SfxPlayer");
	//	sfxObject.transform.parent = transform;
	//	sfxPlayers = new AudioSource[channels];

	//	for (int index = 0; index < sfxPlayers.Length; index++)
	//	{ 
	//		sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
	//		sfxPlayers[index].playOnAwake = false;
	//		sfxPlayers[index].bypassListenerEffects = true;
	//		sfxPlayers[index].volume = sfxVolum;
	//	}
	//}

	//public void PlayBgm(bool isPlay)
	//{
	//	if (isPlay) 
	//	{
	//		bgmPlayer.Play();
	//	}
	//	else 
	//	{
	//		bgmPlayer.Stop();
	//	}
	//}

	//// ������ �������� ���
	//public void EffectBgm(bool isPlay)
	//{
	//	bgmEffect.enabled = isPlay;
	//}

	//public void PlaySfx(Sfx sfx)
	//{
	//	// ä�� �� ��ŭ ��ȸ�ϵ��� ä�� �ε��� ���� Ȱ��
	//	for (int index = 0; index < sfxPlayers.Length; index++)
	//	{
	//		int loopIndex = (index + channelIndex) % sfxPlayers.Length;

	//		if (sfxPlayers[loopIndex].isPlaying)
	//		{
	//			continue;
	//		}

	//		// 3���� Switch������
	//		//int randIndex = 0;
	//		//if (sfx == Sfx.Hit || sfx == Sfx.Melee)
	//		//{
	//		//	randIndex = Random.Range(0, 2);
	//		//}

	//		channelIndex = loopIndex;
	//		sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
	//		sfxPlayers[loopIndex].Play();
	//		break;
	//	}
	//}

	public void ChangeVolume()
	{ 
		AudioListener.volume = volumSlider.value;
		Save();
	}

	private void Load()
	{
		volumSlider.value = PlayerPrefs.GetFloat("musicVolume");
	}

	private void Save()
	{
		PlayerPrefs.SetFloat("musicVolume", volumSlider.value);
	}
}
