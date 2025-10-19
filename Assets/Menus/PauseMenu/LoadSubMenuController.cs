using UnityEngine;
using UnityEngine.UI;

public class LoadSubMenuController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas LoadSubMenuCanvas;

	public Button CloseLoadSubMenuButton;
	public Button LoadGameButton;
	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseLoadSubMenuButton.onClick.AddListener(CloseLoadSubMenu);
		LoadGameButton.onClick.AddListener(DataPersistenceManager.instance.LoadGame);
	}

	private void Update()
	{
		if (playerInputsList.GetKeyPauseMenu() && LoadSubMenuCanvas.gameObject.activeInHierarchy)
		{
			CloseLoadSubMenu();
		}
	}

	public void CloseLoadSubMenu()
	{
		LoadSubMenuCanvas.gameObject.SetActive(false);

		pauseMenuController.PauseMenuCanvas.gameObject.SetActive(true);

		Debug.Log("LoadSubMenu closed");
	}
}
