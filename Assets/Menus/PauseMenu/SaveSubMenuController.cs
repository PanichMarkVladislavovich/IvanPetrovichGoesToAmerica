using UnityEngine;
using UnityEngine.UI;

public class SaveSubMenuController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas SaveSubMenuCanvas;

	public Button CloseSaveSubMenuButton;

	public Button SaveGame1Button;
	public Button SaveGame2Button;
	public Button SaveGame3Button;
	public Button SaveGame4Button;
	public Button SaveGame5Button;
	void Start()
    {
		playerInputsList = GetComponent<PlayerInputsList>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseSaveSubMenuButton.onClick.AddListener(CloseSaveSubMenu);

		SaveGame1Button.onClick.AddListener(() => DataPersistenceManager.instance.SaveGame(1));
		SaveGame2Button.onClick.AddListener(() => DataPersistenceManager.instance.SaveGame(2));
		SaveGame3Button.onClick.AddListener(() => DataPersistenceManager.instance.SaveGame(3));
		SaveGame4Button.onClick.AddListener(() => DataPersistenceManager.instance.SaveGame(4));
		SaveGame5Button.onClick.AddListener(() => DataPersistenceManager.instance.SaveGame(5));

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
