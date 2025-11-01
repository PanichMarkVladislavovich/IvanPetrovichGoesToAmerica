using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LONGLOAD : MonoBehaviour
{
	public float activationDelay = 5f; // �������� ����� ���������� �������� ��������

	void Start()
	{
		// ������������ �������� ������ (���� �����)
		gameObject.SetActive(false);

		// �������� ����� ����������
		Invoke(nameof(ActivateObjects), activationDelay);
	}

	void ActivateObjects()
	{
		// ����������� ��������
		gameObject.SetActive(true);

		// �����������: ������ ������� Canvas ��� ������ UI-��������
		GetComponentInChildren<Canvas>()?.gameObject.SetActive(true);
	}
}