using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorSCENE : DoorAbstract
{
	public override string InteractionItemName => "Дверь";

	//private float doorOpeningSpeed = 200f; // Скорость открытия-закрытия

	//private Coroutine currentAnimation;     // Переменная для хранения активной корутины

	//private Quaternion openedRotation;       // Угловое положение открытой двери
//	private Quaternion closedRotation;     // Угловое положение закрытой двери

	[SerializeField] private string goToSceneName;

	public override void Interact()
	{
		//string sceneName = SceneManager.GetActiveScene().name;

		SceneManager.LoadSceneAsync(goToSceneName);
	}

	

	
}