using Inventories;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
	[Header("PlayerStats")]
	public float curHP;
	public float maxHP;

   [Header("UI")]
	public Slider HPSlider;
	public TMP_Text HPText;

	[Header("")]
	public TMP_Text hitDamageText;
	public bool isDie;

	[Header("")]
	[SerializeField] private string sceneToLoad;

	// ��������Ʈ ����
	public delegate void PlayerDieHandler();
	// �̺�Ʈ ����
	public static event PlayerDieHandler OnPlayerDie;

	private Animator animator;

	bool movementStarted = false;

	public void Start()
	{

		HPSlider.maxValue = maxHP;
		curHP = maxHP;
		HPSlider.value = curHP;
		HPText.text = curHP.ToString("f0") + "/" + maxHP.ToString("f0");

		hitDamageText.gameObject.SetActive(false);
		hitDamageText.text = "0";

		isDie = false;
	}

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		CheckSpecialAbilityKeys();
		if (Input.GetMouseButtonUp(0))
		{
			movementStarted = false;
		}
	}

	// ������ ������ HP ����
	public void TakeDamage(float damage)
	{
		// �������� ���� HP���� ū ���, �������� ���� HP�� ����
		if (damage > curHP)
		{
			damage = curHP;
		}

		curHP -= damage;
		animator.SetTrigger("GetHit");
		HPSlider.value = curHP;

		if (curHP <= 0)
		{
			PlayerDie();
		}

		// ���� HP�� �ִ� HP �ؽ�Ʈ�� ǥ�õ�
		HPText.text = curHP.ToString("f0") + "/" + maxHP.ToString("f0");
		// ������ ���ڸ� �ؽ�Ʈ�� ����
		hitDamageText.text = "-" + damage.ToString();

		StartCoroutine(ShowDamageTextRoutine());
	}

	// ������ ���ڸ� ǥ���ϰ� ���� �ð� �Ŀ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
	private IEnumerator ShowDamageTextRoutine()
	{
		hitDamageText.gameObject.SetActive(true); // ������ ���� Ȱ��ȭ

		yield return new WaitForSeconds(1.0f); // 1�� ���� ��� (������ ���ڸ� ǥ���� �ð�)

		hitDamageText.gameObject.SetActive(false); // ������ ���� ��Ȱ��ȭ
	}

	// �� ������ ������ HP ����
	public void Heal(int healthBoost)
	{
		
	}

	// Player�� ��� ó��
	private void PlayerDie()
	{
		Debug.Log("Player Die !");

		// ���ΰ� ��� �̺�Ʈ ȣ��(���� �⻵��)
		OnPlayerDie();
		isDie = true;

		// �ִϸ����Ϳ��� "IsDie" Ʈ���Ÿ� �����Ͽ� ��� �ִϸ��̼� ���
		Debug.Assert(animator != null);
		animator.SetTrigger("IsDie");

		StartCoroutine(WaitForAnimationToEndRoutine());		
	}

	private IEnumerator WaitForAnimationToEndRoutine()
	{		
		yield return new WaitForSeconds(5f);

		// GameManager ��ũ��Ʈ�� IsGameOver ������Ƽ ���� ����
		GameManager.Instance.IsGameOver = true;

		// �ִϸ��̼� ����� ���� �� ���� ���� ������ ��ȯ
		GameManager.Scene.LoadScene(sceneToLoad);
		Debug.Log("Enter the " + sceneToLoad);
	}

	// �� ���� ����ϴ� Ű
	private void CheckSpecialAbilityKeys()
	{
		var actionStore = GetComponent<ActionStore>();
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			actionStore.Use(0, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			actionStore.Use(1, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			actionStore.Use(2, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			actionStore.Use(3, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			actionStore.Use(4, gameObject);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			actionStore.Use(5, gameObject);
		}
	}
}
