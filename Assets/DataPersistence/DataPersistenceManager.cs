using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;

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

    public static DataPersistenceManager instance {  get; private set; }


	private void Awake()
	{
		fileSaveDataName1 = "SAVEGAME1.json";
		fileSaveDataName2 = "SAVEGAME2.json";
		fileSaveDataName3 = "SAVEGAME3.json";
		fileSaveDataName4 = "SAVEGAME4.json";
		fileSaveDataName5 = "SAVEGAME5.json";

		if (instance != null)
		{
			Debug.Log("WRONG!");
		}

		instance = this;

		//this.DataHandler = new FileDataHandler(saveElsewhere, fileName);
		//this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName1);
		this.dataPersistenceObjects = FindAllDataPersistenceObjects();

		if (this.gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
			//LoadGame(1);
		}


		// Ќайти все золотые слитки и назначить им индексы
		LootItemGoldBar[] goldBars = FindObjectsOfType<LootItemGoldBar>();
		for (int i = 0; i < goldBars.Length; i++)
		{
			goldBars[i].AssignLootItemIndex(i, typeof(LootItemGoldBar));
		}





	}

	private void Update()
	{
		if (this.gameData == null)
		{
		//	Debug.Log("EMPTY");
		}
		//else Debug.Log("OKOK");
	}

	public void NewGame()
	{
		this.gameData = new GameData();
		//LoadGame(1);
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
		}
		else if (loadSlotNumber == 2)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName2);
		}
		else if (loadSlotNumber == 3)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName3);
		}
		else if (loadSlotNumber == 4)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName4);
		}
		else if (loadSlotNumber == 5)
		{
			this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileSaveDataName5);
		}


		this.gameData = fileDataHandler.Load();

		if (this.gameData == null)
		{
			Debug.Log("No data to load found in slot " + loadSlotNumber);
			NewGame();
			return;
		}
		else
		{


			foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
			{
				dataPersistenceObj.LoadData(gameData);
			}
			Debug.Log("Data loaded from slot " + loadSlotNumber);
		}
	}

	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}


	public void ReAssignLootItemIndexes()
	{
		LootItemGoldBar[] goldBars = FindObjectsOfType<LootItemGoldBar>();
		for (int i = 0; i < goldBars.Length; i++)
		{
			goldBars[i].AssignLootItemIndex(i, typeof(LootItemGoldBar));
		}
	}
}
