using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorSCENE : DoorAbstract
{
	public override string InteractionItemName => "Дверь";

	

	[SerializeField] private string goToSceneName;

	public override void Interact()
	{
		StartCoroutine(SaveAndLoadScene());
	}

	private IEnumerator SaveAndLoadScene()
	{
		// Начало сохранения игры
		DataPersistenceManager.Instance.SaveGame(-1);

		// Ждём завершения сохранения
		yield return new WaitUntil(() => DataPersistenceManager.Instance.IsSavingFinished);

		// После завершения сохранения начинаем загрузку новой сцены
		SceneManager.LoadSceneAsync(goToSceneName);
	}
}

	

