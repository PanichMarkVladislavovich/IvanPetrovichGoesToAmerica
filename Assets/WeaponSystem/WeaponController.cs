using UnityEngine;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour
{
	public Button PoliceBatonButton;
	public Button HarmonicaRevolverButton;
	public Button PlungerCrossbowButton;
	public Button EugenicGenieButton;

	PlayerInputsList playerInputsList;
	WeaponWheelController weaponWheelController;
	PlayerBehaviour playerBehaviour;
	public WeaponWheelbuttonscript weaponWheelbuttonscript;

	public WeaponClass LeftHandWeapon {  get; private set; }
	//public string CurrentLeftHandWeaponName { get; private set; }
	public WeaponClass RightHandWeapon {  get; private set; }
	//public string CurrentRightHandWeaponName { get; private set; }

	private void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		weaponWheelController = GetComponent<WeaponWheelController>();
		playerBehaviour = GetComponent<PlayerBehaviour>();

		// Назначаем обработчики событий для кнопок
		PoliceBatonButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPoliceBaton)));
		HarmonicaRevolverButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponHarmonicaRevolver)));
		PlungerCrossbowButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPlungerCrossbow)));
		EugenicGenieButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponEugenicGenie)));
	}

	private void Update()
	{
		//Debug.Log(LeftHandWeapon?.WeaponName);
		//Debug.Log(RightHandWeapon?.WeaponName);

		if (playerInputsList.GetKeyRightHandWeaponAttack() && !GameManager.IsAnyMenuOpened)
		{
			RightWeaponAttack();
		}

		if (playerInputsList.GetKeyLeftHandWeaponAttack() && !GameManager.IsAnyMenuOpened)
		{
			LeftWeaponAttack();
		}
	}

	private void SelectWeapon(System.Type weaponType)
	{
		bool isLeftHand = weaponWheelController.IsWeaponLeftHand;

		// Проверяем, есть ли оружие в левой руке
		if (weaponWheelController.IsWeaponLeftHand && LeftHandWeapon != null && LeftHandWeapon.GetType() == weaponType)
		{
			// Если текущее оружие совпадает с выбранным, ничего не делаем
			playerBehaviour.ArmPlayer();
			return;
		}
		// Проверяем, есть ли оружие в правой руке
		else if (!isLeftHand && RightHandWeapon != null && RightHandWeapon.GetType() == weaponType)
		{
			// Если текущее оружие совпадает с выбранным, ничего не делаем
			playerBehaviour.ArmPlayer();
			return;
		}
		else
		{
			// Если оружие не найдено ни в одной руке, создаем новый экземпляр оружия
			if (isLeftHand)
			{
				if (LeftHandWeapon != null)
				{
					RemoveWeapon("left");
					//CurrentLeftHandWeaponName = "-";
				}
				else if (RightHandWeapon != null && RightHandWeapon.GetType() == weaponType)
				{
					RemoveWeapon("right");
				}

				// Создаем новый экземпляр оружия
				LeftHandWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
				ChangeWheaponWheelButtonColor("left");
				LeftHandWeapon.Equip(true); // Передаем флаг isLeftHand
				//CurrentLeftHandWeaponName = LeftHandWeapon.WeaponName;
				playerBehaviour.ArmPlayer();
			}
			else
			{
				if (RightHandWeapon != null)
				{
					RemoveWeapon("right");
				}
				else if (LeftHandWeapon != null && LeftHandWeapon.GetType() == weaponType)
				{
					RemoveWeapon("left");
					//CurrentLeftHandWeaponName = "-";
				}

				// Создаем новый экземпляр оружия
				RightHandWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
				ChangeWheaponWheelButtonColor("right");
				RightHandWeapon.Equip(false); // Передаем флаг isLeftHand
			//	CurrentRightHandWeaponName = RightHandWeapon.WeaponName;
				playerBehaviour.ArmPlayer();
			}

			if (LeftHandWeapon != null && RightHandWeapon != null && RightHandWeapon.WeaponNameSystem == LeftHandWeapon.WeaponNameSystem)
			{
				if (isLeftHand == true)
				{
					RemoveWeapon("right");
				}
				else if (isLeftHand == false)
				{
					RemoveWeapon("left");
				}
			}

			Debug.Log("LeftHand: " + (LeftHandWeapon?.WeaponNameSystem ?? "null") + " | RightHand: " + (RightHandWeapon?.WeaponNameSystem ?? "null"));
		}
	}

	public void RightWeaponAttack()
	{
		if (RightHandWeapon != null)
		{
			RightHandWeapon.WeaponAttack();
		}
	}

	public void LeftWeaponAttack()
	{
		if (LeftHandWeapon != null)
		{
			LeftHandWeapon.WeaponAttack();
		}
	}
	public void RemoveWeapon(string handType)
	{
		if (handType == "right")
		{
			RightHandWeapon.Unequip(); // Добавляем вызов Unequip()
			Destroy(RightHandWeapon); // Уничтожаем предыдущее оружие
			RightHandWeapon = null;
		}
		else if (handType == "left")
		{
			LeftHandWeapon.Unequip(); // Добавляем вызов Unequip()
			Destroy(LeftHandWeapon); // Уничтожаем предыдущее оружие
			LeftHandWeapon = null;
		}
	}

	public void ShowWeapon(string handType)
	{ 
		if (handType == "right")
		{
			RightHandWeapon.weaponMeshRenderer.enabled = true;
		}
		else if (handType == "left")
		{
			LeftHandWeapon.weaponMeshRenderer.enabled = true;
		}
	}

	public void HideWeapon(string handType)
	{
		if (handType == "right")
		{
			RightHandWeapon.weaponMeshRenderer.enabled = false;
		}
		else if (handType == "left")
		{
			LeftHandWeapon.weaponMeshRenderer.enabled = false;
		}
	}

	public void ChangeWheaponWheelButtonColor(string handType)
	{
		if (handType == "right")
		{
			if (RightHandWeapon?.WeaponNameSystem == "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(PoliceBatonButton);
			}
			if (RightHandWeapon?.WeaponNameSystem == "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(HarmonicaRevolverButton);
			}
			if (RightHandWeapon?.WeaponNameSystem == "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(PlungerCrossbowButton);
			}
			if (RightHandWeapon?.WeaponNameSystem == "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(EugenicGenieButton);
			}

			if (RightHandWeapon?.WeaponNameSystem != "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(PoliceBatonButton);
			}
			if (RightHandWeapon?.WeaponNameSystem != "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(HarmonicaRevolverButton);
			}
			if (RightHandWeapon?.WeaponNameSystem != "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(PlungerCrossbowButton);
			}
			if (RightHandWeapon?.WeaponNameSystem != "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(EugenicGenieButton);
			}
		}
		else if (handType == "left")
		{
			if (LeftHandWeapon?.WeaponNameSystem == "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(PoliceBatonButton);
			}
			if (LeftHandWeapon?.WeaponNameSystem == "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(HarmonicaRevolverButton);
			}
			if (LeftHandWeapon?.WeaponNameSystem == "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(PlungerCrossbowButton);
			}
			if (LeftHandWeapon?.WeaponNameSystem == "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToActive(EugenicGenieButton);
			}

			if (LeftHandWeapon?.WeaponNameSystem != "PoliceBaton")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(PoliceBatonButton);
			}
			if (LeftHandWeapon?.WeaponNameSystem != "HarmonicaRevolver")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(HarmonicaRevolverButton);
			}
			if (LeftHandWeapon?.WeaponNameSystem != "PlungerCrossbow")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(PlungerCrossbowButton); ;
			}
			if (LeftHandWeapon?.WeaponNameSystem != "EugenicGenie")
			{
				weaponWheelbuttonscript.ChangeWeaponWheelButtonColorToDefault(EugenicGenieButton);
			}
		}
	}

}