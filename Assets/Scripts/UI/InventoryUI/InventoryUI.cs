using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : WindowUI
{
	protected override void Awake()
	{
		base.Awake();

		//buttons["CloseButton"].onClick.AddListener(() => { CloseUI(); });
	}

}
