using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorSCENE : OpenableObjectAbstract
{
	//public override string InteractionItemName => "Дверь";

	

	[SerializeField] private string goToSceneName;

	public override void Interact()
	{
		StartCoroutine(GameSceneManager.Instance.GoToScene(goToSceneName));
	}
}

	

