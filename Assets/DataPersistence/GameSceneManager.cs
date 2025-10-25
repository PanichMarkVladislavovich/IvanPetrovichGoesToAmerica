using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IDataPersistence
{
	public static GameSceneManager Instance { get; private set; } // Статическое поле экземпляра

	private void Awake()
	{
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
	}
	public void LoadData(GameData data)
	{

		string sceneName = SceneManager.GetActiveScene().name;

		SceneManager.LoadScene(sceneName);
		Debug.Log("Scene reloaded");

	}

	public void SaveData(ref GameData data)
	{
		//throw new System.NotImplementedException();
	}

}
