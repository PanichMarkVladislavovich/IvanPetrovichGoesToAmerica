using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	public Canvas WeaponWheelMenuCanvas; // Привяжите ваш Canvas сюда
	public bool IsWeaponLeftHand { get; private set; }
	public bool IsWeaponWheelActive { get; private set; }

	private bool previousLeftHandPressed = false;
	private bool previousRightHandPressed = false;

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		WeaponWheelMenuCanvas.gameObject.SetActive(false);
		//DisableCanvas();
	}

	void Update()
	{
		
			bool currentLeftHandPressed = playerInputsList.GetKeyLeftHandWeaponWheel();
			bool currentRightHandPressed = playerInputsList.GetKeyRightHandWeaponWheel();

			// Обновляем состояние, только если изменилось нажатие кнопки
			if (currentLeftHandPressed != previousLeftHandPressed ||
				currentRightHandPressed != previousRightHandPressed)
			{
				HandleWeaponWheel(currentLeftHandPressed, currentRightHandPressed);
			}

			previousLeftHandPressed = currentLeftHandPressed;
			previousRightHandPressed = currentRightHandPressed;
		
	}

	private void HandleWeaponWheel(bool leftHandPressed, bool rightHandPressed)
	{
		if (leftHandPressed)
		{
			EnableWeaponWheelMenuCanvas();
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = true;
		}
		else if (rightHandPressed)
		{
			EnableWeaponWheelMenuCanvas();
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = false;
		}
		else
		{
			DisableWeaponWheelMenuCanvas();
			IsWeaponWheelActive = false;
		}
	}

	private void EnableWeaponWheelMenuCanvas()
	{
		WeaponWheelMenuCanvas.gameObject.SetActive(true); // Показываем Canvas
		GameManager.OpenWeaponWheelMenu();
	}

	private void DisableWeaponWheelMenuCanvas()
	{
		WeaponWheelMenuCanvas.gameObject.SetActive(false); // Скрываем Canvas
		if (!GameManager.IsMainMenuOpened)
		{
			GameManager.CloseWeaponWheelMenu();
		}
	}
}