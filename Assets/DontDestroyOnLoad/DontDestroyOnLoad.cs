using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	private void Awake()
	{
		// Запрещаем уничтожение объекта при загрузке новых сцен
		//DontDestroyOnLoad(this.gameObject);
	}
}
