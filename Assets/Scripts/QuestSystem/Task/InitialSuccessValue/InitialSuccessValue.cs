using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ʈ�� �ʱ� ���� ���� ����
public abstract class InitialSuccessValue : ScriptableObject
{
	public abstract int GetValue(Task task);
}
