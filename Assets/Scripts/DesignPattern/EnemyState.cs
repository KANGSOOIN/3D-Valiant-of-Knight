using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// FSM
/// </summary>

public class EnemyState : MonoBehaviour, IDamageable
{
	// Enemy ���� ����
	public enum State
	{
		IDLE,
		TRACE,
		ATTACK,
		DIE
	}

	[Header("EnemyStats")]
	public State state = State.IDLE; // Enemy�� ���� ����
	public float traceDistance = 5.0f; // Enemy�� TRACE ������ �Ÿ�
	public float attackDistance = 3.0f; // Enemy�� ATTACK ������ �Ÿ�
	public bool isDie = false; // Enemy�� ��� ����
	[SerializeField] private int HP = 100;
	[SerializeField] private int curHP;

	[Header("UI")]
	[SerializeField] private Slider HPSlider;

	private Transform monster; // Enemy
	private Transform player; // Enemy�� ���� ���
	private NavMeshAgent navMeshAgent;
	private Animator animator;

	private PlayerState playerState; // Player�� ������ ������
	private PlayerAttacker playerAttacker;
	public int damage; // Enemy�� Player���� ���ϴ� ����

	private void OnEnable()
	{
		HPSlider.maxValue = HP;
		curHP = HP;
		HPSlider.value = curHP;
		state = State.IDLE;

		// �̺�Ʈ �߻� �� ������ �Լ� ����
		PlayerState.OnPlayerDie += OnPlayerDie;

		// ���� üũ�ϴ� �ڷ�ƾ ����
		StartCoroutine(EnemyCheckRoutine());
		// ����� ���� �����ϴ� �ڷ�ƾ ����
		StartCoroutine(EnemyActionRoutine());
	}

	// ��ũ��Ʈ�� ��Ȱ��ȭ�� ������ ȣ��Ǵ� �Լ�
	void OnDisable()
	{
		// ������ ����� �Լ� ����
		PlayerState.OnPlayerDie -= this.OnPlayerDie;
	}

	private void Awake()
	{
		monster = GetComponent<Transform>();
		player = GetComponent<Transform>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

		// ���� ��� Tag "Player"
		player = GameObject.FindGameObjectWithTag("Player").transform;

		// Player�� �����͸� ������
		playerState = FindObjectOfType<PlayerState>();
	}

	void Update()
	{
		HPSlider.value = curHP;
	}

	// 0.1�� �������� ���� üũ�ϴ� �ڷ�ƾ
	IEnumerator EnemyCheckRoutine()
	{
		while (!isDie)
		{
			// 0.1�� ���� ���
			yield return new WaitForSeconds(0.1f);

			// ������ ���°� DIE�� �� �ڷ�ƾ�� ����
			if (state == State.DIE)
				yield break;

			// �÷��̾���� �Ÿ� ����
			float distance = Vector3.Distance(player.position, monster.position);

			// ATTACK ������ ������ ���Դ��� Ȯ��
			if (distance <= attackDistance)
			{
				state = State.ATTACK;
				Debug.Log("ATTACK ���� Ȯ��");
			}
			// TRACE ������ ������ ���Դ��� Ȯ��
			else if (distance <= traceDistance)
			{
				state = State.TRACE;
				Debug.Log("TRACE ���� Ȯ��");
			}
			// �ƴ϶��, IDLE
			else
			{
				state = State.IDLE;
				Debug.Log("IDLE");
			}
		}
	}

	// ���¿� ���� ���� �����ϴ� �ڷ�ƾ
	IEnumerator EnemyActionRoutine()
	{
		// DIE ���°� �ƴ϶�� ��� ���� üũ��
		while (!isDie)
		{
			switch (state)
			{
				// IDLE ����
				case State.IDLE:
					// TRACE ����
					navMeshAgent.isStopped = true;
					animator.SetBool("IsMove", false);
					Debug.Log("State.IDLE");
					break;

				// TRACE ����
				case State.TRACE:
					// ���� ����
					navMeshAgent.SetDestination(player.position);
					navMeshAgent.isStopped = false;
					animator.SetBool("IsMove", true);
					animator.SetBool("IsAttack", false);
					Debug.Log("State.TRACE");
					break;

				// ATTACK ����
				case State.ATTACK:
					// �÷��̾��� ��ġ�� ���ϵ��� ������ ȸ���� ����
					transform.LookAt(player.position);
					animator.SetBool("IsAttack", true);
					yield return new WaitForSeconds(1f);
					// Player���� �������� ��
					playerState.TakeDamage(damage);
					Debug.Log("State.ATTACK");
					break;

				// DIE ����
				case State.DIE:
					isDie = true;
					Debug.Log("State.DIE");
					// ���� ����
					navMeshAgent.isStopped = true;

					// ��� �ִϸ��̼� ����
					animator.SetTrigger("IsDie");

					// ������ Collider ������Ʈ ��Ȱ��ȭ
					GetComponent<Collider>().enabled = false;

					// ���� �ð� ��� �� ������Ʈ Ǯ������ ȯ��
					yield return new WaitForSeconds(3.0f);

					// ��� �� �ٽ� ����� ���� ���� hp �� �ʱ�ȭ
					HP = 100;
					isDie = false;

					// ������ Collider ������Ʈ Ȱ��ȭ
					GetComponent<Collider>().enabled = true;
					// ���͸� ��Ȱ��ȭ
					this.gameObject.SetActive(false);
					break;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}

	// �÷��̾�� �������� ������ HP �����ǰ� �ǰ� ���׼� �����
	private void OnTriggerEnter(Collider coll)
	{
		if (curHP >= 0.0f && coll.CompareTag("Weapons"))
		{
			curHP -= damage;
			HPSlider.value = curHP;
			// �ǰ� ���׼� �ִϸ��̼� ����
			animator.SetTrigger("GetHit");
			Debug.Log($"Enemy HP = {curHP / HP}");

			if (curHP <= 0.0f)
			{
				state = State.DIE;
				// ToDo : ���Ͱ� ������� �� 50 ����ġ �߰�
			}
		}
	}

	// TRACE, ATTACK �����
	private void OnDrawGizmos()
	{
		// TRACE ���� �Ÿ� ǥ��
		if (state == State.TRACE)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(transform.position, traceDistance);
		}
		// ATTACK ���� �Ÿ� ǥ��
		if (state == State.ATTACK)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackDistance);
		}
	}

	// �÷��̾ ������� �� 
	private void OnPlayerDie()
	{
		// ������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ��� ��� ������Ŵ
		StopAllCoroutines();

		// ������ �����ϰ� �ִϸ��̼��� ����
		navMeshAgent.isStopped = true;
		animator.SetBool("IsMove", false);
		animator.SetTrigger("PlayerDie");
	}

	public void TakeDamage(int damage)
	{
		curHP -= damage;
		if (curHP <= 0) {
			curHP = 0;
			state = State.DIE;
		}
	}
}
