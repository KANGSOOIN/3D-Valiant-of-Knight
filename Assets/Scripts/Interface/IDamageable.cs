using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	/// <summary>
	/// ���ݴ��� �� �ִ� ��� ���
	/// </summary>
	/// <param name="damage"> ������ </param>
	public void TakeDamage(int damage); 
}
