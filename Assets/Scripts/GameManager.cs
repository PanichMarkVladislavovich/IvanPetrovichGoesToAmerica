using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerInputsList playerInputsList;

	public Canvas MainMenuCanvas;
	public static bool IsPlayerControllable { get; private set; }
	public static bool IsMainMenuOpened { get; private set; }
	public static bool IsWeaponWheelMenuOpened { get; private set; }
	public static bool IsAnyMenuOpened { get; private set; }

	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        playerInputsList = GetComponent<PlayerInputsList>();

		MainMenuCanvas.gameObject.SetActive(false);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		IsPlayerControllable = true;
		//CloseMenu();
		IsMainMenuOpened = false;
		IsWeaponWheelMenuOpened = false;
		IsAnyMenuOpened = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (playerInputsList.GetKeyOpenMainMenu() )
		{
			if (!IsMainMenuOpened)
			{
				OpenMainMenu();
			}
			else CloseMainMenu();
		}


		
	}

    public void OpenMainMenu()
    {
		if (IsWeaponWheelMenuOpened)
		{
			CloseWeaponWheelMenu();
		}


		MainMenuCanvas.gameObject.SetActive(true); // Скрываем Canvas

		Debug.Log("MainMenu is opened");
		OpenMenu();

		

        IsPlayerControllable = false;
		IsMainMenuOpened = true;

		// Полностью останавливаем игру
		Time.timeScale = 0f;

	}

	public void CloseMainMenu()
	{
		MainMenuCanvas.gameObject.SetActive(false); // Скрываем Canvas

		Debug.Log("MainMenu is closed");
		CloseMenu();

		IsPlayerControllable = true;
			IsMainMenuOpened = false;

		// Возвращаем нормальное течение времени
		Time.timeScale = 1f;
	}

	public static void OpenWeaponWheelMenu()
	{
		OpenMenu();
		IsWeaponWheelMenuOpened = true;
		

		Debug.Log("WeaponWheelMenu is opened");

	}

	public static void CloseWeaponWheelMenu()
	{
		CloseMenu();
		IsWeaponWheelMenuOpened = false;
		Debug.Log("WeaponWheelMenu is closed");

	}

	public static void OpenMenu()
	{
		IsAnyMenuOpened = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

			//Debug.Log("Menu is opened");
	}

	public static void CloseMenu()
	{
		IsAnyMenuOpened = false;

		

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		//Debug.Log("Menu is closed");
	}
}
