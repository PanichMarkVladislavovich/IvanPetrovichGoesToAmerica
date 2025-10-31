using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
	[SerializeField] private string fileSaveDataName1 = "";
	[SerializeField] private string fileSaveDataName2 = "";
	[SerializeField] private string fileSaveDataName3 = "";
	[SerializeField] private string fileSaveDataName4 = "";
	[SerializeField] private string fileSaveDataName5 = "";

	//private string saveElsewhere = @"C:\Users\PanichMark\Desktop";

	private GameData gameData;
	private List<IDataPersistence> dataPersistenceObjects;
	private FileDataHandler fileDataHandler;
	[SerializeField] private static int WhatSaveNumberWasLoaded;

    public static DataPersistenceManager Instance {  get; private set; }




	private void Awake()
	{
		Time.timeScale = 1.0f;

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

		fileSaveDataName1 = "SAVEGAME1.json";
		fileSaveDataName2 = "SAVEGAME2.json";
		fileSaveDataName3 = "SAVEGAME3.json";
		fileSaveDataName4 = "SAVEGAME4.json";
		fileSaveDataName5 = "SAVEGAME5.json";


		LootItemGoldBar[] goldBars = FindObjectsOfType<LootItemGoldBar>();
		for (int i = 0; i < goldBars.Length; i++)
		{
			goldBars[i].AssignLootItemIndex(i);
		}


		this.dataPersistenceObjects = FindAllDataPersistenceObjects();



		if (WhatSaveNumberWasLoaded != 0)
		{
			string fileSaveDataName = null;
			if (WhatSaveNumberWasLoaded == 1)
			{
				fileSaveDataName = fileSaveDataName1;
			}
			else if (WhatSaveNumberWasLoaded == 2)
			{
				fileSaveDataName = fileSaveDataName2;
			}
			else if (WhatSaveNumberWasLoaded == 3)
			{
				fileSaveDataName = fileSaveDataName3;
			}
			else if (WhatSaveNumberWasLoaded == 4)
			{
				fileSaveDataName = fileSaveDataName4;
			}
			else if (WhatSaveNumberWasLoaded == 5)
			{
				fileSaveDataName = fileSaveDataName5;
			}
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName);
			this.gameData = fileDataHandler.Load();
		}

	
		if (this.gameData != null)
		{
			foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
			{
				dataPersistenceObj.LoadData(gameData);
			}

			Debug.Log("Data loaded from slot " + WhatSaveNumberWasLoaded);

		}
		else if (this.gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
		}

		


	}

	public void Update()
	{
		//Debug.Log(Time.timeScale);
	}


	private void OnApplicationQuit()
	{
		WhatSaveNumberWasLoaded = 0;
	}



	public void NewGame()
	{
		this.gameData = new GameData();
		WhatSaveNumberWasLoaded = 0;

		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(gameData);
		}
	}

	public void SaveGame(int saveSlotNumber)
	{
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
	}

	public void LoadGame(int loadSlotNumber)
	{
		if (loadSlotNumber == 1)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName1);
			WhatSaveNumberWasLoaded = 1;
		}
		else if (loadSlotNumber == 2)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName2);
			WhatSaveNumberWasLoaded = 2;
		}
		else if (loadSlotNumber == 3)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName3);
			WhatSaveNumberWasLoaded = 3;
		}
		else if (loadSlotNumber == 4)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName4);
			WhatSaveNumberWasLoaded = 4;
		}
		else if (loadSlotNumber == 5)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName5);
			WhatSaveNumberWasLoaded = 5;
		}

		
		this.gameData = fileDataHandler.Load();



		if (this.gameData == null)
		{
			Debug.Log("No data to load found in slot " + loadSlotNumber);
			WhatSaveNumberWasLoaded = 0;


			return;
		}
		else
		{
			string sceneName = SceneManager.GetActiveScene().name;

			SceneManager.LoadScene(sceneName);
			Debug.Log($"Scene {sceneName} reloaded");
		}
	}

	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
