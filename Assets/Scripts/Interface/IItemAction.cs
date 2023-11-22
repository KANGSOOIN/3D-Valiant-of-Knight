using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ü�� ȸ��
public interface IItemAction
{
	public string ActionName { get; }

	public AudioClip actionSFX { get; }
	
	bool PerformAction(GameObject character, List<ItemParameter> itemState);
}
