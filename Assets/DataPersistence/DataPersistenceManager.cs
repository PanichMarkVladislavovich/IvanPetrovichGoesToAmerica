using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;

public class DataPersistenceManager : MonoBehaviour
{
	[SerializeField] private string fileName = "";

	private string saveElsewhere = @"C:\Users\PanichMark\Desktop";

	private GameData gameData;
	private List<IDataPersistence> dataPersistenceObjects;
	private FileDataHandler DataHandler;

    public static DataPersistenceManager instance {  get; private set; }


	private void Awake()
	{
		fileName = "SAVEGAME1.json";

		if (instance != null)
		{
			Debug.Log("WRONG!");
		}

		instance = this;

		//this.DataHandler = new FileDataHandler(saveElsewhere, fileName);
		this.DataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
		this.dataPersistenceObjects = FindAllDataPersistenceObjects();

		if (this.gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
			LoadGame();
		}
	}

	private void Start()
	{
		
		
	}

	public void NewGame()
	{
		this.gameData = new GameData();
		//Debug.Log("IsRightShoulder is " + gameData.IsCameraShoulderRight);
	}

	public void SaveGame()
	{
		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.SaveData(ref gameData);
		}

		//Debug.Log("Saved IsRightShoulder: " + gameData.IsCameraShoulderRight);
		//Debug.Log("Saved State: " + gameData.CurrentPlayerMovementStateType);
	//	Debug.Log("Saved Player Position: " + gameData.PlayerTransform);

		DataHandler.Save(gameData);
	}

	public void LoadGame()
	{
		this.gameData = DataHandler.Load();

		if (this.gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
		}



		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(gameData);
		}

		//Debug.Log("Loaded IsCameraShoulderRight: " + gameData.IsCameraShoulderRight);
		//Debug.Log("Loaded State: " + gameData.CurrentPlayerMovementStateType);
		//Debug.Log("Loaded Player Position: " + gameData.PlayerTransform);
	}

	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
