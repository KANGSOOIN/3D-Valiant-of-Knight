using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

	private void Start()
	{
		//slider.onValueChanged.AddListener(OnSliderValueChanged);
	}

	private void ChangedMasterVoulum(float newValue)
	{
		// �����̴� ���� ����� �� ȣ��˴ϴ�.
		// newValue�� �����̴��� ���� ���Դϴ�.
		// �� ���� SoundManager�� �����Ͽ� ������ ������ �� �ֽ��ϴ�.

		// ���� ���, SoundManager�� SetMasterVolume �Լ��� ȣ���Ͽ� ������ ������ �� �ֽ��ϴ�.
		// ���⿡���� ���� ���� 0���� 1 ���̷� ����ȭ�� ����, SetMasterVolume �Լ��� �����մϴ�.
		float normalizedVolume = newValue; // �����̴� ���� �̹� 0���� 1 ���̷� ����ȭ�Ǿ� ���� ���Դϴ�.
		//GameManager.Sound.PlaySound(normalizedVolume);
	}
}
