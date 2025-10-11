using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	public Canvas WeaponWheelMenuCanvas; // Привяжите ваш Canvas сюда
	public bool IsWeaponLeftHand { get; private set; }
	public bool IsWeaponWheelActive { get; private set; }

	private bool previousRightHandPressed = false;
	private bool previousLeftHandPressed = false;
	

	public TextMeshProUGUI WeaponWheelName;


	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		WeaponWheelMenuCanvas.gameObject.SetActive(false);
		//DisableCanvas();
	}

	void Update()
	{
			bool currentRightHandPressed = playerInputsList.GetKeyRightHandWeaponWheel();
			bool currentLeftHandPressed = playerInputsList.GetKeyLeftHandWeaponWheel();
			

			// Обновляем состояние, только если изменилось нажатие кнопки
			if (currentRightHandPressed != previousRightHandPressed || currentLeftHandPressed != previousLeftHandPressed)
			{
				HandleWeaponWheel(currentRightHandPressed, currentLeftHandPressed);
			}

			previousRightHandPressed = currentRightHandPressed;
			previousLeftHandPressed = currentLeftHandPressed;
			
		
	}

	



	void HandleWeaponWheel(bool rightHandPressed, bool leftHandPressed)
	{
		
		
		// Обработка правой руки
		if (rightHandPressed && !leftHandPressed && !IsWeaponWheelActive)
		{
			EnableWeaponWheelMenuCanvas(true);
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = false;
		
			WeaponWheelName.text = "RIGHT HAND";
		}

		// Обработка левой руки
		else if (leftHandPressed && !rightHandPressed && !IsWeaponWheelActive)
		{
			EnableWeaponWheelMenuCanvas(false);
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = true;
		
			WeaponWheelName.text = "LEFT HAND";
		}

		

		// Деактивация, если ничего не нажато
		else if (!leftHandPressed && !rightHandPressed)
		{
			DisableWeaponWheelMenuCanvas(!IsWeaponLeftHand);
			IsWeaponWheelActive = false;
		}
	}


	private void EnableWeaponWheelMenuCanvas(bool IsItRightWeaponWheelMenuCanvas)
	{
		WeaponWheelMenuCanvas.gameObject.SetActive(true); // Показываем Canvas
		GameManager.OpenWeaponWheelMenu(IsItRightWeaponWheelMenuCanvas);
	}

	private void DisableWeaponWheelMenuCanvas(bool IsItRightWeaponWheelMenuCanvas)
	{
		WeaponWheelMenuCanvas.gameObject.SetActive(false); // Скрываем Canvas
		if (!GameManager.IsMainMenuOpened)
		{
			GameManager.CloseWeaponWheelMenu(IsItRightWeaponWheelMenuCanvas);
		}
	}
}