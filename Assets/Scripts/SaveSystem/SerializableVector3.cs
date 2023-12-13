using UnityEngine;

namespace Saving
{
    /// <summary>
    /// �÷��̾� ��ġ ����
    /// </summary>
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;

		/// <summary>
		/// ���� Vector3���� ���¸� ������
		/// </summary>
		public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

		/// <summary>
		/// �� Ŭ������ ���¿��� ���ο� Vector3�� ��ȯ��
		/// </summary>
		/// <returns></returns>
		public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}