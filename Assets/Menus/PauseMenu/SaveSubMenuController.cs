using UnityEngine;
using UnityEngine.UI;

public class SaveSubMenuController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas SaveSubMenuCanvas;

	public Button CloseSaveSubMenuButton;
	public Button SaveGameButton;
	void Start()
    {
		playerInputsList = GetComponent<PlayerInputsList>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseSaveSubMenuButton.onClick.AddListener(CloseSaveSubMenu);
		SaveGameButton.onClick.AddListener(DataPersistenceManager.instance.SaveGame);
	}

	private void Update()
	{
		if (playerInputsList.GetKeyPauseMenu() && SaveSubMenuCanvas.gameObject.activeInHierarchy)
		{
			CloseSaveSubMenu();
		}
	}

	public void CloseSaveSubMenu()
	{
		SaveSubMenuCanvas.gameObject.SetActive(false);
		
		pauseMenuController.PauseMenuCanvas.gameObject.SetActive(true);
		
		Debug.Log("SaveSubMenu closed");
	}
}
