using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
	public Button PoliceBatonButton;
	public Button HarmonicaRevolverButton;
	public Button PlungerCrossbowButton;
	public Button EugenicGenieButton;

	PlayerInputsList playerInputsList;
	PlayerBehaviour playerBehaviour;

	public WeaponClass LeftHandWeapon {  get; private set; }
	public WeaponClass RightHandWeapon {  get; private set; }

	WeaponWheelController weaponWheelController;

	private void Start()
	{
		//leftHandWeapon = GetComponent<WeaponClass>();
		//rightHandWeapon = GetComponent<WeaponClass>();
		playerInputsList = GetComponent<PlayerInputsList>();

		weaponWheelController = GetComponent<WeaponWheelController>();

		// Назначаем обработчики событий для кнопок
		PoliceBatonButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPoliceBaton)));
		HarmonicaRevolverButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponHarmonicaRevolver)));
		PlungerCrossbowButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPlungerCrossbow)));
		EugenicGenieButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponEugenicGenie)));

		playerBehaviour = GetComponent<PlayerBehaviour>();
	}

	private void Update()
	{
		/*
		if (leftHandWeapon != null && rightHandWeapon != null && rightHandWeapon.WeaponName == leftHandWeapon.WeaponName)
		{
			Debug.Log("Одинакого");
		}
		*/

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
			return;
		}
		// Проверяем, есть ли оружие в правой руке
		else if (!isLeftHand && RightHandWeapon != null && RightHandWeapon.GetType() == weaponType)
		{
			// Если текущее оружие совпадает с выбранным, ничего не делаем
			return;
		}
		else
		{


			// Если оружие не найдено ни в одной руке, создаем новый экземпляр оружия
			if (isLeftHand)
			{


				if (LeftHandWeapon != null)
				{
					//leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
					//Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					//leftHandWeapon = null;
					RemoveLeftWeapon();
				}
				else if (RightHandWeapon != null && RightHandWeapon.GetType() == weaponType)
				{
					//rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
					//Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
					//rightHandWeapon = null;
					RemoveRightWeapon();
				}



				// Создаем новый экземпляр оружия
				LeftHandWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
				LeftHandWeapon.Equip(true); // Передаем флаг isLeftHand
				playerBehaviour.ArmPlayer();
			}
			else
			{
				if (RightHandWeapon != null)
				{
					//rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
					//Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
					//rightHandWeapon = null;
					RemoveRightWeapon();
				}
				else if (LeftHandWeapon != null && LeftHandWeapon.GetType() == weaponType)
				{
					//leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
					//Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					//leftHandWeapon = null;
					RemoveLeftWeapon();
				}



				// Создаем новый экземпляр оружия
				RightHandWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
				RightHandWeapon.Equip(false); // Передаем флаг isLeftHand
				playerBehaviour.ArmPlayer();







			}

			if (LeftHandWeapon != null && RightHandWeapon != null && RightHandWeapon.WeaponName == LeftHandWeapon.WeaponName)
			{
				if (isLeftHand == true)
				{
					//rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
					//Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
					//rightHandWeapon = null;
					RemoveRightWeapon();
				}
				else if (isLeftHand == false)
				{
					//leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
					//Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					//leftHandWeapon = null;
					RemoveLeftWeapon();
				}
			}

			Debug.Log("LeftHand: " + (LeftHandWeapon?.WeaponName ?? "null") + " | RightHand: " + (RightHandWeapon?.WeaponName ?? "null"));
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
	public void RemoveRightWeapon()
	{
		RightHandWeapon.Unequip(); // Добавляем вызов Unequip()
		Destroy(RightHandWeapon); // Уничтожаем предыдущее оружие
		RightHandWeapon = null;
	}

	public void RemoveLeftWeapon()
	{
		LeftHandWeapon.Unequip(); // Добавляем вызов Unequip()
		Destroy(LeftHandWeapon); // Уничтожаем предыдущее оружие
		LeftHandWeapon = null;
	}

	public void ShowRightWeapon()
	{
		RightHandWeapon.weaponMeshRenderer.enabled = true;
	}

	public void ShowLeftWeapon()
	{
		LeftHandWeapon.weaponMeshRenderer.enabled = true;
	}
	public void HideRightWeapon()
	{
		RightHandWeapon.weaponMeshRenderer.enabled = false;
	}

	public void HideLeftWeapon()
	{
		LeftHandWeapon.weaponMeshRenderer.enabled = false;
	}
}