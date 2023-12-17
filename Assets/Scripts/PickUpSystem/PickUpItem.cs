using UnityEngine;
using Inventories;
using TMPro;
using UnityEngine.Events;
using Control;

namespace Control
{
	[RequireComponent(typeof(Pickup))]
	public class PickUpItem : MonoBehaviour
	{
		private Pickup pickup;
		private InventorySystem inventorySystem;

		public TMP_Text triggerText;
		private bool isTriggered;

		public UnityEvent OnPickUpItme;

		private void Awake()
		{
			pickup = GetComponent<Pickup>();

			triggerText = GetComponentInChildren<TMP_Text>();
			if (triggerText == null)
			{
				Debug.LogError("TMP_Text component not found.");
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (isTriggered)
				return;

			triggerText.gameObject.SetActive(true);
			triggerText.text = "�������� ȹ���Ϸ��� [ E ] Ű�� �������� ";
			isTriggered = true;
		}

		private void OnTriggerExit(Collider other)
		{
			if (!isTriggered)
			{
				triggerText.text = "";
				triggerText.gameObject.SetActive(false);
			}
		}

		private void Update()
		{
			if (isTriggered && Input.GetKeyDown(KeyCode.E))
			{
				if (pickup.CanBePickedUp())
				{
					pickup.PickupItem();
					Destroy(gameObject);
					OnPickUpItme.Invoke();
				}
				else
				{
					Debug.Log("�κ��丮�� �� á���ϴ�.");
				}
			}
		}
	}
}

