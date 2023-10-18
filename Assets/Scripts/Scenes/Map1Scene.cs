using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map1Scene : BaseScene
{
	protected override IEnumerator LoadingRoutine()
	{
		// ���� ����ũ �ε�
		Debug.Log("���� �� ����");
		progress = 0.0f;
		//// ���� �ð��� ���������� ���� �ð���ŭ �귯���� ��
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("���� ���� ����");
		progress = 0.2f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("���� ������ ����");
		progress = 0.4f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("�÷��̾� ��ġ");
		progress = 0.6f;
		yield return new WaitForSecondsRealtime(1f);

		Debug.Log("�ε� �Ϸ�");
		progress = 1.0f;
	}
}
