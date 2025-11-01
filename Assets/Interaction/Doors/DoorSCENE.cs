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


	/*
	private IEnumerator SaveAndLoadScene()
	{
		// Начало сохранения игры
		DataPersistenceManager.Instance.SaveGame(-1);

		// Ждём завершения сохранения
		yield return new WaitUntil(() => DataPersistenceManager.Instance.IsSavingFinished);

		

		// После завершения сохранения начинаем загрузку новой сцены
		SceneManager.LoadSceneAsync(goToSceneName);
	}
	*/

	private IEnumerator SaveAndLoadScene()
	{
		// Останавливаем время игры
		Time.timeScale = 0f;

		// Начало сохранения игры
		DataPersistenceManager.Instance.SaveGame(-1);

		// Ждём завершения сохранения
		yield return new WaitUntil(() => DataPersistenceManager.Instance.IsSavingFinished);

		// Загружаем сцену асинхронно, предварительно запретив автоматическую активацию
		AsyncOperation asyncOp = SceneManager.LoadSceneAsync(goToSceneName);
		asyncOp.allowSceneActivation = false;

		// Ждём фиксированное количество кадров (примерно 5 секунд)
		int framesToWait = 120; // Примерно 5 секунд при 60 FPS
		for (int i = 0; i < framesToWait; i++)
		{
			yield return null; // Ждать один кадр
		}

		// Разрешить активацию сцены
		asyncOp.allowSceneActivation = true;

		// Восстанавливаем нормальный ход времени
		//Time.timeScale = 1.0f;
	}
}

	

