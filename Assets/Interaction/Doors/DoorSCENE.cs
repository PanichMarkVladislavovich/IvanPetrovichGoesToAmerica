using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorSCENE : DoorAbstract
{
	public override string InteractionItemName => "�����";

	

	[SerializeField] private string goToSceneName;

	public override void Interact()
	{
		StartCoroutine(SaveAndLoadScene());
	}


	/*
	private IEnumerator SaveAndLoadScene()
	{
		// ������ ���������� ����
		DataPersistenceManager.Instance.SaveGame(-1);

		// ��� ���������� ����������
		yield return new WaitUntil(() => DataPersistenceManager.Instance.IsSavingFinished);

		

		// ����� ���������� ���������� �������� �������� ����� �����
		SceneManager.LoadSceneAsync(goToSceneName);
	}
	*/

	private IEnumerator SaveAndLoadScene()
	{
		// ������������� ����� ����
		Time.timeScale = 0f;

		// ������ ���������� ����
		DataPersistenceManager.Instance.SaveGame(-1);

		// ��� ���������� ����������
		yield return new WaitUntil(() => DataPersistenceManager.Instance.IsSavingFinished);

		// ��������� ����� ����������, �������������� �������� �������������� ���������
		AsyncOperation asyncOp = SceneManager.LoadSceneAsync(goToSceneName);
		asyncOp.allowSceneActivation = false;

		// ��� ������������� ���������� ������ (�������� 5 ������)
		int framesToWait = 120; // �������� 5 ������ ��� 60 FPS
		for (int i = 0; i < framesToWait; i++)
		{
			yield return null; // ����� ���� ����
		}

		// ��������� ��������� �����
		asyncOp.allowSceneActivation = true;

		// ��������������� ���������� ��� �������
		//Time.timeScale = 1.0f;
	}
}

	

