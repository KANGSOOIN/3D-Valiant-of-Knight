using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	public Transform targetTransform; // ���󰡾� �� ����� ������ ����								  
	[Range(2.0f, 20.0f)] public float distance = 10.0f; // ���� ������κ��� ������ �Ÿ�	
	[Range(0.0f, 10.0f)] public float height = 2.0f; // Y������ �̵��� ����	
	public float damping = 10.0f; // ���� �ӵ�	
	public float targetOffset = 2.0f; // ī�޶� LookAt�� Offset ��

	private Transform camTransform;
	private Vector3 velocity = Vector3.zero;

	void Start()
	{
		camTransform = GetComponent<Transform>();
	}

	void LateUpdate()
	{
		// �����ؾ� �� ����� �������� distance��ŭ �̵� + ���̸� height��ŭ �̵� 
		Vector3 pos = targetTransform.position + (-targetTransform.forward * distance) + (Vector3.up * height);

		// SmoothDamp�� �̿��� ��ġ ���� (���� ��ġ, ��ǥ ��ġ, ���� �ӵ�, ��ǥ ��ġ���� ������ �ð�)
		camTransform.position = Vector3.SmoothDamp(camTransform.position, pos, ref velocity, damping);

		// Camera�� �ǹ� ��ǥ�� ���� ȸ��
		camTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));
	}
}
