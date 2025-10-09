using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
	public Button PoliceBatonButton;
	public Button HarmonicaRevolverButton;
	public Button PlungerCrossbowButton;
	public Button EugenicGenieButton;

	private WeaponClass leftHandWeapon;
	private WeaponClass rightHandWeapon;

	WeaponWheelController weaponWheelController;

	private void Start()
	{
		//leftHandWeapon = GetComponent<WeaponClass>();
		//rightHandWeapon = GetComponent<WeaponClass>();

		weaponWheelController = GetComponent<WeaponWheelController>();

		// Назначаем обработчики событий для кнопок
		PoliceBatonButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPoliceBaton)));
		HarmonicaRevolverButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponHarmonicaRevolver)));
		PlungerCrossbowButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPlungerCrossbow)));
		EugenicGenieButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponEugenicGenie)));
	}

	private void Update()
	{
		if (leftHandWeapon != null && rightHandWeapon != null && rightHandWeapon.WeaponName == leftHandWeapon.WeaponName)
		{
			Debug.Log("Одинакого");
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
				}
				else if (rightHandWeapon != null && rightHandWeapon.GetType() == weaponType)
				{
					rightHandWeapon.Unequip(); // Добавляем вызов Unequip()
					Destroy(rightHandWeapon); // Уничтожаем предыдущее оружие
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
				}
				else if (leftHandWeapon != null && leftHandWeapon.GetType() == weaponType)
				{
					leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
					Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
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
					}
					else if (isLeftHand == false)
					{
						leftHandWeapon.Unequip(); // Добавляем вызов Unequip()
						Destroy(leftHandWeapon); // Уничтожаем предыдущее оружие
					}
				}

		}
	}


	public void UseCurrentWeapon()
	{
		if (leftHandWeapon != null)
		{
			leftHandWeapon.WeaponAttack();
		}
		else if (rightHandWeapon != null)
		{
			rightHandWeapon.WeaponAttack();
		}
	}
}