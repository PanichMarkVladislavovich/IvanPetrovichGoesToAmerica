using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IDataPersistence
{
	public string CurrentSceneName {  get; private set; }
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

		CurrentSceneName = SceneManager.GetActiveScene().name;
	}


	void Start()
    {
		Debug.Log($"Current scene is {CurrentSceneName}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SaveData(ref GameData data)
	{
		data.CurrentScene = SceneManager.GetActiveScene().name;
	}

	public void LoadData(GameData data)
	{
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		SceneManager.LoadSceneAsync(data.CurrentScene);
	}
}
