using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using System;

public class DataPersistenceManager : MonoBehaviour
{
	[SerializeField] private string fileSaveDataTEMP = "";
	[SerializeField] private string fileSaveDataName1 = "";
	[SerializeField] private string fileSaveDataName2 = "";
	[SerializeField] private string fileSaveDataName3 = "";
	[SerializeField] private string fileSaveDataName4 = "";
	[SerializeField] private string fileSaveDataName5 = "";

	private static bool isFirstTimeLoaded = true; // Флаг для первой загрузки



	private GameData gameData;
	public bool IsSavingFinished { get; private set; }

	private List<IDataPersistence> dataPersistenceObjects;
	private FileDataHandler fileDataHandler;
	[SerializeField] private static int whatSaveNumberWasLoaded;

	public static DataPersistenceManager Instance {  get; private set; }

	private void Awake()
	{
		// Только при первой загрузке игры выполняем перезагрузку
		if (isFirstTimeLoaded && Application.isPlaying)
		{

			ReloadCurrentScene();
			isFirstTimeLoaded = false; // Меняем флаг, чтобы предотвратить последующую перезагрузку

		}

		//Debug.Log(whatSaveNumberWasLoaded);



		// Паттерн Singleton: предотвращаем создание второго экземпляра
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // Сохраняется при смене уровней
		}
		else
		{
			Destroy(gameObject); // Уничтожаем лишние экземпляры
		}

		fileSaveDataTEMP = "SaveGameTEMP.json";
		fileSaveDataName1 = "SaveGame1.json";
		fileSaveDataName2 = "SaveGame2.json";
		fileSaveDataName3 = "SaveGame3.json";
		fileSaveDataName4 = "SaveGame4.json";
		fileSaveDataName5 = "SaveGame5.json";


		Time.timeScale = 1.0f;




		this.dataPersistenceObjects = FindAllDataPersistenceObjects();



		
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		LootItemGoldBar[] goldBars = FindObjectsOfType<LootItemGoldBar>();
		for (int i = 0; i < goldBars.Length; i++)
		{
			goldBars[i].AssignLootItemIndex(i);
		}



		this.dataPersistenceObjects = FindAllDataPersistenceObjects();


		if (whatSaveNumberWasLoaded != 0)
		{
			if (whatSaveNumberWasLoaded == -1)
			{

				this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataTEMP);
				this.gameData = fileDataHandler.Load();
			}
			else
			{
				string fileSaveDataName = null;
				if (whatSaveNumberWasLoaded == 1)
				{
					fileSaveDataName = fileSaveDataName1;
				}
				else if (whatSaveNumberWasLoaded == 2)
				{
					fileSaveDataName = fileSaveDataName2;
				}
				else if (whatSaveNumberWasLoaded == 3)
				{
					fileSaveDataName = fileSaveDataName3;
				}
				else if (whatSaveNumberWasLoaded == 4)
				{
					fileSaveDataName = fileSaveDataName4;
				}
				else if (whatSaveNumberWasLoaded == 5)
				{
					fileSaveDataName = fileSaveDataName5;
				}

				this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName);
				this.gameData = fileDataHandler.Load();
			}
		}



		if (gameData != null)
		{
			foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
			{
				dataPersistenceObj.LoadData(gameData);
			}

			if (whatSaveNumberWasLoaded != 0)
			{
				Debug.Log("Data loaded from slot " + whatSaveNumberWasLoaded);
			}
			SaveGame(-1);

		}
		if (gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
		}

		whatSaveNumberWasLoaded = 0;
	}
	

	


	private void OnApplicationQuit()
	{
		isFirstTimeLoaded = true;
		whatSaveNumberWasLoaded = 0;
	}


	/*
	public void NewGame()
	{
		this.gameData = new GameData();
		//Debug.Log(gameData.HealingItems);
		whatSaveNumberWasLoaded = 0;
		SaveGame(-1);


		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(gameData);
		}
	}
	*/
	public void NewGame()
	{
		// Шаг 1: Создание нового объекта GameData с начальными значениями
		this.gameData = new GameData();

		// Шаг 2: Настройка обработки файла для временного хранилища
		this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataTEMP);

		// Шаг 3: Сохранение текущего состояния в временный файл
		fileDataHandler.Save(this.gameData);

		// Шаг 4: Теперь обновляем каждый объект, реализующий IDataPersistence,
		// используя ранее созданные данные из временного файла
		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(this.gameData);
		}

		Debug.Log("A new game has been started!");
	}



	public void SaveGame(int saveSlotNumber)
	{
		IsSavingFinished = false;

		if (saveSlotNumber == -1)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataTEMP);

			if (this.gameData == null)
			{
				this.gameData = new GameData();
			}
			//else this.gameData = fileDataHandler.Load();

		}
		/*
		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.SaveData(ref gameData);
		}
		

		fileDataHandler.Save(gameData);
		*/
		
		
		if (saveSlotNumber == 1)
        {
             this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName1);
        }
		else if (saveSlotNumber == 2)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName2);
		}
		else if (saveSlotNumber == 3)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName3);
		}
		else if (saveSlotNumber == 4)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName4);
		}
		else if (saveSlotNumber == 5)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName5);
		}

		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.SaveData(ref gameData);
		}

		fileDataHandler.Save(gameData);

		if (saveSlotNumber != -1)
		{
			Debug.Log("Data saved to slot " + saveSlotNumber);
		}

		if (whatSaveNumberWasLoaded != 0)
		{
			Debug.Log("TEMP Data rewritten from Data save " + whatSaveNumberWasLoaded);
		}
		else Debug.Log("TEMP Data rewritten from Default");


		IsSavingFinished = true;
	}

	public void LoadGame(int loadSlotNumber)
	{
		
		if (loadSlotNumber == 1)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName1);
			whatSaveNumberWasLoaded = 1;
		}
		else if (loadSlotNumber == 2)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName2);
			whatSaveNumberWasLoaded = 2;
		}
		else if (loadSlotNumber == 3)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName3);
			whatSaveNumberWasLoaded = 3;
		}
		else if (loadSlotNumber == 4)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName4);
			whatSaveNumberWasLoaded = 4;
		}
		else if (loadSlotNumber == 5)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName5);
			whatSaveNumberWasLoaded = 5;
		}

		
		this.gameData = fileDataHandler.Load();



		if (this.gameData == null)
		{
			Debug.Log("No data to load found in slot " + loadSlotNumber);
			whatSaveNumberWasLoaded = 0;


			return;
		}
		else
		{

			string sceneName = gameData.CurrentSceneSystemName;

			SceneManager.LoadSceneAsync(sceneName);
			Debug.Log($"Scene {sceneName} loaded");
		}
	}


	private void ReloadCurrentScene()
	{
		string currentScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadSceneAsync(currentScene);
	}

	// Метод для извлечения данных о сохранениях
	public string[] GetSaveInfo()
	{
		List<string> info = new List<string>();
		info.Add(GetSaveDataForFile(fileSaveDataName1)); // Имя первого слота
		info.Add(GetSaveDataForFile(fileSaveDataName2)); // Имя второго слота
		info.Add(GetSaveDataForFile(fileSaveDataName3)); // Имя третьего слота
		info.Add(GetSaveDataForFile(fileSaveDataName4)); // Имя четвёртого слота
		info.Add(GetSaveDataForFile(fileSaveDataName5)); // Имя пятого слота

		return info.ToArray(); // Возвращаем массив с информацией о каждом слоте
	}

	// Вспомогательная функция для получения данных из конкретного файла
	private string GetSaveDataForFile(string fileName)
	{
		try
		{
			GameData gameData = fileDataHandler.LoadFromFile(fileName);
			if (gameData != null)
			{
				return gameData.CurrentLevelNameUI; // Возвращаем имя уровня из файла
			}
			else
			{
				return "<Empty Slot>"; // Если файл пуст или отсутствует
			}
		}
		catch (Exception e)
		{
			Debug.LogWarning($"Ошибка при чтении файла '{fileName}'\n{e.Message}");
			return "<Invalid Slot>";
		}
	}


	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
