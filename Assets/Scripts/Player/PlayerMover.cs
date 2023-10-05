using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[Header("Gizmo")]
	[SerializeField] bool debug;

	[Header("Speed")]
	[SerializeField] float walkSpeed;
	[SerializeField] float runSpeed;
	[SerializeField] float jumpSpeed;

	[Header("ViewingRange")]
	[SerializeField] float walkStepRange;
	[SerializeField] float runStepRange;

	private CharacterController characterController;
	private Animator animator;

	private Vector3 moveDirection = Vector3.zero;
	private float currentSpeed;

	private float ySpeed; // ySpeed : ���� ������ �������� �ӷ��� ���� ���ӽ�Ŵ -> �߷�ó�� �������� ����
	private bool walk;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Move();
		Jump();
	}

	float lastStepTime = 0.5f;
	private void Move()
	{
		// �̵� �� ������ �� �̵� ������ �ƴ϶� ������ �ٶ󺸴� ���� ����
		// magnitude : ũ��
		if (moveDirection.magnitude == 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.1f);
			animator.SetFloat("MoveSpeed", currentSpeed);
			return;
		}

		Vector3 forwardVector = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
		Vector3 rightVector = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
		// normalized : Vector�� �Ϲ�ȭ 

		if (walk)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, 0.1f);
		}
		else
		{
			currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, 0.1f);
		}

		characterController.Move(forwardVector * moveDirection.z * currentSpeed * Time.deltaTime);
		characterController.Move(rightVector * moveDirection.x * currentSpeed * Time.deltaTime);
		animator.SetFloat("MoveSpeed", currentSpeed);

		// ȸ��
		Quaternion lookRotaion = Quaternion.LookRotation(forwardVector * moveDirection.z + rightVector * moveDirection.x);
		transform.rotation = Quaternion.Lerp(transform.rotation, lookRotaion, 0.1f);
		// Lerp : ���� ����

		// ���� ���� �� �ִ� �Ҹ� ����
		lastStepTime -= Time.deltaTime;
		if (lastStepTime < 0)
		{
			lastStepTime = 0.5f;
			GenerateFootStepSound();
		}
	}

	private void GenerateFootStepSound()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, walk ? walkStepRange : runStepRange);
		foreach (Collider collider in colliders)
		{
			IListenable listenable = collider.GetComponent<IListenable>();
			listenable?.Listen(transform);
		}
	}

	private void OnMove(InputValue value)
	{
		// �Է� ���� ���� moveDirection�� ��� ����
		// �����ϴ� ������ ���̹Ƿ� ������ ��, ��, ������, ����
		// -> x�� x, z�� y�� �־���
		moveDirection.x = value.Get<Vector2>().x;
		moveDirection.z = value.Get<Vector2>().y;
	}

	private void OnWalk(InputValue value)
	{
		walk = value.isPressed;
	}

	private void Jump()
	{
		// �߷� �������� �ӷ��� ������
		ySpeed += Physics.gravity.y * Time.deltaTime;

		// ���� ���ٴڿ� �پ� �ִ� ��쿡 ySpeed�� -1�� �����ؼ� ���Ʒ��� �������� ����
		if (GroundCheck() && ySpeed < 0)
			ySpeed = -1;

		// -> Jump�� �� ��Ȳ�̸� ���� �ӷ��� �ö�
		// -> Jump�� �� �� ��Ȳ�̸� �Ʒ��� �ӷ��� ������
		characterController.Move(Vector3.up * ySpeed * Time.deltaTime);
	}

	private void OnJump(InputValue value)
	{
		if (GroundCheck())
			ySpeed = jumpSpeed;
	}

	// ���ٴڿ� �÷��̾ ���� ���� �����ϱ� ���� GroundCheck�� Ȯ��
	private bool GroundCheck()
	{
		RaycastHit hit;
		return Physics.SphereCast(transform.position + Vector3.up * 1, 0.5f, Vector3.down, out hit, 0.7f);
	}

	private void OnDrawGizmosSelected()
	{
		if (!debug)
			return;

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, walkSpeed);
		Gizmos.DrawWireSphere(transform.position, runSpeed);
	}
}

