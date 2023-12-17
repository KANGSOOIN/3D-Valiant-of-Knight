using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ܼ��� ���� ���� ī��Ʈ�� ���� ���� ī��Ʈ�� ���ؼ� ������
/// </summary>
[CreateAssetMenu(menuName = "Quest/Task/Action/SimpleCount", fileName = "Simple Count")]
public class SimpleCount : TaskAction
{
	public override int Run(Task task, int currentSuccess, int successCount)
	{
		return currentSuccess + successCount;
	}
}
