namespace Saving
{
	/// <summary>
	/// ���� �Ǵ� ������ �� ����
	/// </summary>
	public interface ISaveable
    {
		/// <summary>
		/// ��������� ���¸� ĸó�ϱ� ���� ������ �� ȣ���
		/// </summary>
		/// <returns>
		/// ���¸� ��Ÿ���� System.Serialized ��ü ��Ҹ� ��ȯ��
		/// </returns>
		object CaptureState();

		/// <summary>
		/// ����� ���¸� ������ �� ȣ���
		/// </summary>
		/// <param name="state">
		/// ������ �� CaptureState���� ��ȯ�� �Ͱ� ������ System.Serializable ��ü��
		/// </param>
		void RestoreState(object state);
    }
}