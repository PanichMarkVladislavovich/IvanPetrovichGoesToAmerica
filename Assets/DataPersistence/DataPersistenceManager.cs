using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;

public class DataPersistenceManager : MonoBehaviour
{
	private GameData gameData;
	private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistenceManager instance {  get; private set; }


	private void Awake()
	{
		if (instance != null)
		{
			Debug.Log("WRONG!");
		}

		instance = this;


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

		Debug.Log("Saved IsRightShoulder: " + gameData.IsCameraShoulderRight);
		Debug.Log("Saved State: " + gameData.CurrentPlayerMovementStateType);
		Debug.Log("Saved Player Position: " + gameData.PlayerTransform);
	}

	public void LoadGame()
	{
		if (this.gameData == null)
		{
			Debug.Log("No data found. starting New game");
			NewGame();
		}



		foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
		{
			dataPersistenceObj.LoadData(gameData);
		}

		Debug.Log("Loaded IsCameraShoulderRight: " + gameData.IsCameraShoulderRight);
		Debug.Log("Loaded State: " + gameData.CurrentPlayerMovementStateType);
		Debug.Log("Loaded Player Position: " + gameData.PlayerTransform);
	}

	private List<IDataPersistence> FindAllDataPersistenceObjects()
	{
		IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

		return new List<IDataPersistence>(dataPersistenceObjects);
	}
}
