using UnityEngine;
using UnityEngine.UI;

public class SettingsSubMenuController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas SettingsSubMenuCanvas;

	public Button CloseSettingsSubMenuButton;
	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseSettingsSubMenuButton.onClick.AddListener(CloseSettingsSubMenu);
	}

	private void Update()
	{
		if (playerInputsList.GetKeyPauseMenu() && SettingsSubMenuCanvas.gameObject.activeInHierarchy)
		{
			CloseSettingsSubMenu();
		}
	}

	public void CloseSettingsSubMenu()
	{
		SettingsSubMenuCanvas.gameObject.SetActive(false);

		pauseMenuController.PauseMenuCanvas.gameObject.SetActive(true);

		Debug.Log("SettingsSubMenu closed");
	}
}
