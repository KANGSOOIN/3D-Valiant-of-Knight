using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
	[Header("Debug")]
	[SerializeField] bool debug;

	[Header("Attack")]
	[SerializeField] private int GetHitDamage;
	[SerializeField] private float range;
	[SerializeField, Range(0, 360)] private float angle;

	private WeaponSystem weapon;
	private EnemyState target;
	private Animator animator;
	private EnemyState enemyState;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Attack()
	{
		animator.SetTrigger("IsAttack");
	}

	private void OnAttack(InputValue value)
	{
		Attack();
	}

	public void StartAttack()
	{
		if (weapon != null)
			weapon.SetActive(true);
	}

	public void EndAttack()
	{
		if (weapon != null)
			weapon.SetActive(false);
	}

	// ���� ����
	// -> ����, ����
	// ���� ����
	public void AttackTiming()
	{
		// 1. ���� �ȿ� �ִ���
		Collider[] colliders = Physics.OverlapSphere(transform.position, range);
		foreach (Collider collider in colliders)
		{
			// 2. �տ� �ִ���
			// -> ����� ��, ������ �ڿ� ����
			Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
			if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad)) // => ������ ȣ�������� ��ȯ
				continue;

			IDamageable hittable = collider.GetComponent<IDamageable>();
			hittable?.TakeDamage(GetHitDamage);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// �ݶ��̴��� �浹�� ��ü�� Monster ������Ʈ�� ������ Ÿ������ ����
		target = other.GetComponent<EnemyState>();
		if (target != null)
			transform.LookAt(target.transform);
	}

	// ������ ���� �����
	private void OnDrawGizmosSelected()
	{
		if (!debug)
			return;

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);

		Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
		Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
		Debug.DrawRay(transform.position, rightDir * range, Color.yellow);
		Debug.DrawRay(transform.position, leftDir * range, Color.yellow);
	}

	private Vector3 AngleToDir(float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
	}
}
