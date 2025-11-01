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

	private IEnumerator SaveAndLoadScene()
	{
		// ������ ���������� ����
		DataPersistenceManager.Instance.SaveGame(-1);

		// ��� ���������� ����������
		yield return new WaitUntil(() => DataPersistenceManager.Instance.IsSavingFinished);

		// ����� ���������� ���������� �������� �������� ����� �����
		SceneManager.LoadSceneAsync(goToSceneName);
	}
}

	

