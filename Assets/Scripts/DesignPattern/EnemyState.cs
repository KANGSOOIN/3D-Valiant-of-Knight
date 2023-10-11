using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

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

	public State state = State.IDLE; // Enemy�� ���� ����
	public float traceDistance = 5.0f; // Enemy�� TRACE ������ �Ÿ�
	public float attackDistance = 3.0f; // Enemy�� ATTACK ������ �Ÿ�
	public bool isDie = false; // Enemy�� ��� ����

	private Transform monster; // Enemy
	private Transform player; // Enemy�� ���� ���
	private NavMeshAgent navMeshAgent;
	private Animator animator;

	[Header("EnemyStats")]
	private int HP = 100;
	[SerializeField] private int curHP;

	[Header("UI")]
	//public Slider HPSlider;
	//public TMP_Text HPText;

	//private PlayerState playerState; // Player�� ������ ������
	PlayerAttacker playerAttacker;
	public int damage; // Enemy�� Player���� ���ϴ� ����

	private void OnEnable()
	{
		// �̺�Ʈ �߻� �� ������ �Լ� ����
		//PlayerState.OnPlayerDie += this.OnPlayerDie;

		// ���� üũ�ϴ� �ڷ�ƾ ����
		StartCoroutine(EnemyCheckRoutine());
		// ����� ���� �����ϴ� �ڷ�ƾ ����
		StartCoroutine(EnemyActionRoutine());
	}

	// ��ũ��Ʈ�� ��Ȱ��ȭ�� ������ ȣ��Ǵ� �Լ�
	void OnDisable()
	{
		// ������ ����� �Լ� ����
		//PlayerState.OnPlayerDie -= this.OnPlayerDie;
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
		//playerState = FindObjectOfType<PlayerState>();
	}

	private void Start()
	{
		//HPSlider.maxValue = HP;
		//curHP = HP;
		//HPSlider.value = curHP;
	}

	void Update()
	{
		//HPSlider.value = curHP;
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
					animator.SetBool("IsAttack", true);
					// Player���� �������� ��
					//playerState.TakeDamage(attackDamage);
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
		if (curHP >= 0.0f && coll.CompareTag("MELEE"))
		{
			//curHP -= damage;
			//HPSlider.value = curHP;
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
	}
}
