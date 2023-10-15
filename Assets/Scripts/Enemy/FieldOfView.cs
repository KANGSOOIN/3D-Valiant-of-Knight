using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	// �þ� ����
	[SerializeField] float viewingRange;
	// �þ߰�
	[SerializeField, Range(0f, 360f)] float viewingAngle;
	[SerializeField] LayerMask targetMask;
	[SerializeField] LayerMask obstacleMask;

	private GameObject target;

	private void Update()
	{
		FindTarget();
	}

	/* ���� ����
	 -> ����, ���� */
	public void FindTarget()
	{
		// �þ߰�
		// 1. ���� �ȿ� �ִ���
		Collider[] colliders = Physics.OverlapSphere(transform.position, viewingRange, targetMask);
		foreach (Collider collider in colliders)
		{
			// 2. ���� �ȿ� �ִ���
			Vector3 directionToTarget = (collider.transform.position - transform.position).normalized;

			if (Vector3.Dot(transform.forward, directionToTarget) < Mathf.Cos(viewingAngle * 0.5f * Mathf.Deg2Rad))
				continue;

			// 3. �߰��� ��ֹ��� ������
			// -> Ray�� ��� Ȯ���� �� ����
			float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
			if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
				continue;

			Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.red);
			target = collider.gameObject;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, viewingRange);

		Vector3 lookDirection = AngleToDirection(transform.eulerAngles.y);
		Vector3 rightDirection = AngleToDirection(transform.eulerAngles.y + viewingAngle * 0.5f);
		Vector3 leftDirection = AngleToDirection(transform.eulerAngles.y - viewingAngle * 0.5f);

		Debug.DrawRay(transform.position, lookDirection * viewingRange, Color.green);
		Debug.DrawRay(transform.position, rightDirection * viewingRange, Color.yellow);
		Debug.DrawRay(transform.position, leftDirection * viewingRange, Color.yellow);
	}

	private Vector3 AngleToDirection(float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
	}
}