using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsterOrbit : MonoBehaviour
{
	/// <summary>
	/// ���� Ÿ��
	/// </summary>
	[Header("Target")]
	[SerializeField] private Transform orbitTarget;
	/// <summary>
	/// ���� ���ǵ�
	/// </summary>
	[Header("Speed")]
	[SerializeField] private float orbitSpeed;
	///  <summary>
	/// ��ǥ���� �Ÿ�(������)
	/// </summary>
	private Vector3 offSet;

	private void Start()
	{
		offSet = transform.position - orbitTarget.position;
	}

	private void Update()
	{
		transform.position = orbitTarget.position + offSet;

		// RotateAround : Ÿ�� ������ ȸ���ϴ� �Լ�
		// -> ���� ��ġ�� ������ ��ǥ���� �Ÿ��� ����
		transform.RotateAround(orbitTarget.position, Vector3.up, orbitSpeed * Time.deltaTime);
		offSet = transform.position - orbitTarget.position;
	}
}
