using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy HPBar�� ī�޶� ���� �� �� �ֵ��� ��
public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
	}
}
