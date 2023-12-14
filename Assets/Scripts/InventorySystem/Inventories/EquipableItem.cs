using UnityEngine;

namespace Inventories
{
	/// <summary>
	/// �÷��̾ ������ �� �ִ� �κ��丮 ������
	/// </summary>
	[CreateAssetMenu(menuName = ("UI.InventorySystem/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [Tooltip("��� ���� ��ġ")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        public EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
    }
}