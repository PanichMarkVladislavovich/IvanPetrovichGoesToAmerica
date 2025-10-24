using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IDataPersistence
{
	public void LoadData(GameData data)
	{
		// Получаем имя текущей активной сцены
		//string sceneName = SceneManager.GetActiveScene().name;

		// Перезагружаем текущую сцену
		//SceneManager.LoadScene(sceneName);
		//Debug.Log("Bruh!");

	}

	public void SaveData(ref GameData data)
	{
		//throw new System.NotImplementedException();
	}

}
