using UnityEngine;
using UnityEngine.UI;

public class ImagesSubMenuController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas ImagesSubMenuCanvas;

	public Button CloseImagesSubMenuButton;
	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseImagesSubMenuButton.onClick.AddListener(CloseImagesSubMenu);
	}

	private void Update()
	{
		if (playerInputsList.GetKeyPauseMenu() && ImagesSubMenuCanvas.gameObject.activeInHierarchy)
		{
			CloseImagesSubMenu();
		}
	}

	public void CloseImagesSubMenu()
	{
		ImagesSubMenuCanvas.gameObject.SetActive(false);

		pauseMenuController.PauseMenuCanvas.gameObject.SetActive(true);

		Debug.Log("ImagesSubMenu closed");
	}
}
