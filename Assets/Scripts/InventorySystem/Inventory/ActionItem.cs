using System;
using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// �� ���Կ� ��ġ�� �� �ְ� �Ҹ��� �� �ִ� �κ��丮 ������
	/// </summary>
	[CreateAssetMenu(menuName = ("InventorySystem/Action Item"))]
    public class ActionItem : InventoryItem
    {
        [Tooltip("�Ҹ��ϴ� ������")]
        [SerializeField] bool consumable = false;

        /// <summary>
        /// ������ ���
        /// </summary>
        /// <param name="user">�÷��̾ �����<param>
        public virtual void Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}