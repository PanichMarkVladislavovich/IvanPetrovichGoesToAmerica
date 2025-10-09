using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
	public Button PoliceBatonButton;
	public Button HarmonicaRevolverButton;
	public Button PlungerCrossbowButton;
	public Button EugenicGenieButton;

	PlayerInputsList playerInputsList;

	private WeaponClass leftHandWeapon;
	private WeaponClass rightHandWeapon;

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
	}

	private void Update()
	{
		/*
		if (leftHandWeapon != null && rightHandWeapon != null && rightHandWeapon.WeaponName == leftHandWeapon.WeaponName)
		{
			Debug.Log("Одинакого");
		}
		*/

		if (playerInputsList.GetKeyRightHandWeaponAttack())
		{
			RightWeaponAttack();
		}

		if (playerInputsList.GetKeyLeftHandWeaponAttack())
		{
			LeftWeaponAttack();
		}
	}

	private void SelectWeapon(System.Type weaponType)
	{
		bool isLeftHand = weaponWheelController.IsWeaponLeftHand;

		// Проверяем, есть ли оружие в левой руке
		if (weaponWheelController.IsWeaponLeftHand && leftHandWeapon != null && leftHandWeapon.GetType() == weaponType)
		{
			// Если текущее оружие совпадает с выбранным, ничего не делаем
			return;
		}
		// Проверяем, есть ли оружие в правой руке
		else if (!isLeftHand && rightHandWeapon != null && rightHandWeapon.GetType() == weaponType)
		{
			// Если текущее оружие совпадает с выбранным, ничего не делаем
			return;
		}
		else
		{
			

			// Если оружие не найдено ни в одной руке, создаем новый экземпляр оружия
			if (isLeftHand)
			{
			

				if (leftHandWeapon != null)
				{
					leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
					Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					leftHandWeapon = null;
				}
				else if (rightHandWeapon != null && rightHandWeapon.GetType() == weaponType)
				{
					rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
					Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
					rightHandWeapon = null;
				}

				

					// Создаем новый экземпляр оружия
					leftHandWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
				leftHandWeapon.Equip(true); // Передаем флаг isLeftHand
			}
			else
			{
				if (rightHandWeapon != null)
				{
					rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
					Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
					rightHandWeapon= null;
				}
				else if (leftHandWeapon != null && leftHandWeapon.GetType() == weaponType)
				{
					leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
					Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					leftHandWeapon= null;
				}

				

				// Создаем новый экземпляр оружия
				rightHandWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
				rightHandWeapon.Equip(false); // Передаем флаг isLeftHand




				

				
			}

				 if ( leftHandWeapon != null && rightHandWeapon != null && rightHandWeapon.WeaponName == leftHandWeapon.WeaponName)
				{
					if (isLeftHand == true)
					{
						rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
						Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
					rightHandWeapon = null;
					}
					else if (isLeftHand == false)
					{
						leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
						Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					leftHandWeapon = null;
					}
				}

			Debug.Log("LeftHand: " + (leftHandWeapon?.WeaponName ?? "null") + " | RightHand: " + (rightHandWeapon?.WeaponName ?? "null"));
		}
	}


	public void RightWeaponAttack()
	{
		if (rightHandWeapon != null)
		{
			rightHandWeapon.WeaponAttack();
		}
	}

	public void LeftWeaponAttack()
	{
		if (leftHandWeapon != null)
		{
			leftHandWeapon.WeaponAttack();
		}
	}

}