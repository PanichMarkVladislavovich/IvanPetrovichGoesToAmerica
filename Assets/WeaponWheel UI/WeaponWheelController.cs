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

	PlayerBehaviour playerBehaviour;
	public TextMeshProUGUI WeaponWheelName;
	public WeaponWheelbuttonscript weaponWheelbuttonscript;
	 WeaponController weaponController;

	void Start()
	{
		playerBehaviour = GetComponent<PlayerBehaviour>();
		playerInputsList = GetComponent<PlayerInputsList>();
		weaponController = GetComponent<WeaponController>();
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
			playerBehaviour.ArmPlayer();
			ChangeWheaponWheelButtonColor("right");
			weaponWheelbuttonscript.HoverExit();
			WeaponWheelName.text = "ПРАВАЯ РУКА";
		}

		// Обработка левой руки
		else if (leftHandPressed && !rightHandPressed && !IsWeaponWheelActive)
		{
			EnableWeaponWheelMenuCanvas(false);
			
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = true;
			playerBehaviour.ArmPlayer();
			ChangeWheaponWheelButtonColor("left");
			weaponWheelbuttonscript.HoverExit();
			WeaponWheelName.text = "ЛЕВАЯ РУКА";
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

	public void ChangeWheaponWheelButtonColor(string handType)
	{
		if (handType == "right")
		{
			if (weaponController.RightHandWeapon?.WeaponNameSystem == "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.PoliceBatonButton);
			}
			if (weaponController.RightHandWeapon?.WeaponNameSystem == "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.HarmonicaRevolverButton);
			}
			if (weaponController.RightHandWeapon?.WeaponNameSystem == "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.PlungerCrossbowButton);
			}
			if (weaponController.RightHandWeapon?.WeaponNameSystem == "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.EugenicGenieButton);
			}

			if (weaponController.RightHandWeapon?.WeaponNameSystem != "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.PoliceBatonButton);
			}
			if (weaponController.RightHandWeapon?.WeaponNameSystem != "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.HarmonicaRevolverButton);
			}
			if (weaponController.RightHandWeapon?.WeaponNameSystem != "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.PlungerCrossbowButton);
			}
			if (weaponController.RightHandWeapon?.WeaponNameSystem != "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.EugenicGenieButton);
			}
		}

		else if (handType == "left")
		{
			if (weaponController.LeftHandWeapon?.WeaponNameSystem == "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.PoliceBatonButton);
			}
			if (weaponController.LeftHandWeapon?.WeaponNameSystem == "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.HarmonicaRevolverButton);
			}
			if (weaponController.LeftHandWeapon?.WeaponNameSystem == "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.PlungerCrossbowButton);
			}
			if (weaponController.LeftHandWeapon?.WeaponNameSystem == "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(weaponController.EugenicGenieButton);
			}

			if (weaponController.LeftHandWeapon?.WeaponNameSystem != "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.PoliceBatonButton);
			}
			if (weaponController.LeftHandWeapon?.WeaponNameSystem != "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.HarmonicaRevolverButton);
			}
			if (weaponController.LeftHandWeapon?.WeaponNameSystem != "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.PlungerCrossbowButton); ;
			}
			if (weaponController.LeftHandWeapon?.WeaponNameSystem != "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(weaponController.EugenicGenieButton);
			}
		}
	}
}