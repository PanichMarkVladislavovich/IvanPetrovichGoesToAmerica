using UnityEngine;
using UnityEngine.UI;

public class SettingsSubMenuController : MonoBehaviour
{
	//InputManager playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas SettingsSubMenuCanvas;

	public Button CloseSettingsSubMenuButton;
	void Start()
	{
		//playerInputsList = GetComponent<InputManager>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseSettingsSubMenuButton.onClick.AddListener(CloseSettingsSubMenu);
	}

	private void Update()
	{
		if (InputManager.Instance.GetKeyPauseMenu() && SettingsSubMenuCanvas.gameObject.activeInHierarchy)
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
