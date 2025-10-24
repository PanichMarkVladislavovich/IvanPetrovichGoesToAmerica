using System.Collections;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject playerCamera;
	[SerializeField] private GameObject gameCanvas;
	[SerializeField] private GameObject dataPersistenceManager;
	[SerializeField] private GameObject gameSceneManager;



	private void Start()
	{
		//BindObjects();
	}

	private void BindObjects()
	{
		player = Instantiate(player);
		playerCamera = Instantiate(playerCamera);
		gameCanvas = Instantiate(gameCanvas);
		dataPersistenceManager = Instantiate(dataPersistenceManager);
		gameSceneManager = Instantiate(gameSceneManager);
	}






	/*
	// Метод запуска игры
	private void Start()
	{
		StartCoroutine(StartGame());
	}

	// Асинхронный запуск игровых процессов
	private IEnumerator StartGame()
	{
		Debug.Log("Начало инициализации...");

		yield return StartCoroutine(BindObjects()); // Ждем окончания выполнения корутины

		Debug.Log("Инициализация закончена.");
	}

	// Корутина выполняющая какую-то операцию
	private IEnumerator BindObjects()
	{
		for (int i = 0; i < 5; i++)
		{
			Debug.Log($"Выполняется операция {i}");
			yield return new WaitForSeconds(1f); // Пауза на секунду
		}

		Debug.Log("Операция выполнена успешно!");
	}
	*/
}
