using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IDataPersistence
{
	public string CurrentSceneSystemName {  get; private set; }

	public string CurrentLevelNameUI { get; private set; }
	public static GameSceneManager Instance { get; private set; }

	private void Awake()
	{
		// Паттерн Singleton: предотвращаем создание второго экземпляра
		if (Instance == null)
		{
			Instance = this;

		}
		else
		{
			Destroy(gameObject); // Уничтожаем лишние экземпляры
		}

		CurrentSceneSystemName = SceneManager.GetActiveScene().name;


	}


	void Start()
    {
		Debug.Log($"Current scene name is {CurrentSceneSystemName}");

		if (SceneManager.GetActiveScene().name == "SceneTEST")
		{
			CurrentLevelNameUI = "Тестовая сцена";
		}
		else if (SceneManager.GetActiveScene().name == "Scene1")
		{
			CurrentLevelNameUI = "Сцена 1";
		}
	}

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(CurrentLevelNameUI);
    }

	public void SaveData(ref GameData data)
	{
		data.CurrentSceneSystemName = SceneManager.GetActiveScene().name;

		if (SceneManager.GetActiveScene().name == "SceneTEST")
		{
			data.CurrentLevelNameUI = "Тестовая сцена";
		}
		else if (SceneManager.GetActiveScene().name == "Scene1")
			{
				data.CurrentLevelNameUI = "Сцена 1";
			}
	}

	public void LoadData(GameData data)
	{
		this.CurrentLevelNameUI = data.CurrentLevelNameUI;
	}
}
