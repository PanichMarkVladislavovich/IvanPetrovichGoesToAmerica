using UnityEngine;
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

	private GameData gameData;
	//public GameData GameDataGet => gameData;
	public bool IsSavingFinished { get; private set; }

	private List<IDataPersistence> dataPersistenceObjects;
	private FileDataHandler fileDataHandler;
	[SerializeField] private static int whatSaveNumberWasLoaded;
	//public int WhatSaveNumberWasLoaded => whatSaveNumberWasLoaded;

	public static DataPersistenceManager Instance {  get; private set; }

	private void Awake()
	{
		

		//this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataTEMP);



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

		Debug.Log(whatSaveNumberWasLoaded);






		LootItemGoldBar[] goldBars = FindObjectsOfType<LootItemGoldBar>();
		for (int i = 0; i < goldBars.Length; i++)
		{
			goldBars[i].AssignLootItemIndex(i);
			//Debug.Log(i);
		}

		//Debug.Log("BRUH!");











		this.dataPersistenceObjects = FindAllDataPersistenceObjects();


	

		if (whatSaveNumberWasLoaded != 0)
		{
			if (whatSaveNumberWasLoaded == -1)
			{
				this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataTEMP);
				this.gameData = fileDataHandler.Load();
			}

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

			whatSaveNumberWasLoaded = 0;
		}
		




	
		if (this.gameData != null)
		{
			foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
			{
				dataPersistenceObj.LoadData(gameData);
			}

			Debug.Log("Data loaded from slot " + whatSaveNumberWasLoaded);

		}
		else if (this.gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
		}

		


	}


	

	private void Update()
	{
		//Debug.Log(gameData.CurrentScene);
	}


	private void OnApplicationQuit()
	{
		whatSaveNumberWasLoaded = 0;
	}



	public void NewGame()
	{
		this.gameData = new GameData();
		whatSaveNumberWasLoaded = 0;

		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			//dataPersistenceObj.LoadData(gameData);
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
		Debug.Log("TEMP Data rewritten to slot " + saveSlotNumber);
		
		
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
		

		Debug.Log("Data saved to slot " + saveSlotNumber);

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
			//GameSceneManager.Instance.LoadData(gameData);
			string sceneName = gameData.CurrentScene;

			SceneManager.LoadSceneAsync(sceneName);
			Debug.Log($"Scene {sceneName} loaded");
		}
	}

	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
