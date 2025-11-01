﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;

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



	public void NewGame()
	{
		this.gameData = new GameData();
		whatSaveNumberWasLoaded = 0;
		SaveGame(-1);


		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(gameData);
		}
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

			string sceneName = gameData.CurrentScene;

			SceneManager.LoadSceneAsync(sceneName);
			Debug.Log($"Scene {sceneName} loaded");
		}
	}


	private void ReloadCurrentScene()
	{
		string currentScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadSceneAsync(currentScene);
	}


	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
