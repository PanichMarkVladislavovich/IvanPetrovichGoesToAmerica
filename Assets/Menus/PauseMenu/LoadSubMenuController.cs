﻿using UnityEngine;
using UnityEngine.UI;

public class LoadSubMenuController : MonoBehaviour
{
	//InputManager playerInputsList;
	PauseMenuController pauseMenuController;

	public Canvas LoadSubMenuCanvas;

	public Button CloseLoadSubMenuButton;

	public Button LoadGame1Button;
	public Button LoadGame2Button;
	public Button LoadGame3Button;
	public Button LoadGame4Button;
	public Button LoadGame5Button;
	void Start()
	{
		//playerInputsList = GetComponent<InputManager>();
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseLoadSubMenuButton.onClick.AddListener(CloseLoadSubMenu);

		LoadGame1Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(1));
		LoadGame2Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(2));
		LoadGame3Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(3));
		LoadGame4Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(4));
		LoadGame5Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(5));
	}

	private void Update()
	{
		if (InputManager.Instance.GetKeyPauseMenu() && LoadSubMenuCanvas.gameObject.activeInHierarchy)
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
