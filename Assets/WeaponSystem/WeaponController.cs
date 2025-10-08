using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
	public Button PoliceBatonButton;
	public Button HarmonicaRevolverButton;
	public Button PlungerCrossbowButton;
	public Button EugenicGenieButon;

	private WeaponClass currentWeapon;

	private void Start()
	{
		// Назначаем обработчики событий для кнопок
		PoliceBatonButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPoliceBaton)));
		HarmonicaRevolverButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponHarmonicaRevolver)));
		PlungerCrossbowButton.onClick.AddListener(() => SelectWeapon(typeof(WeaponPlungerCrossbow)));
		EugenicGenieButon.onClick.AddListener(() => SelectWeapon(typeof(WeaponEugenicGenie)));
	}

	private void Update()
	{
		if (currentWeapon != null && currentWeapon.weaponModel != null)
		{
			// Обновляем положение 3D модели оружия, чтобы она следовала за игроком
		//	currentWeapon.weaponModel.transform.position = transform.position ;
			//currentWeapon.weaponModel.transform.rotation = transform.rotation;
		}
	}







		private void SelectWeapon(System.Type weaponType)
	{
		
		if (currentWeapon != null && currentWeapon.GetType() == weaponType)
		{
			/*
			// Если текущее оружие совпадает с выбранным, дезэкипируем его
			currentWeapon.Unequip();
			Destroy(currentWeapon);
			currentWeapon = null;
			*/



			// Если текущее оружие совпадает с выбранным, ничего не делаем
			return;
		}
		else
		{
			if (currentWeapon != null)
			{
				currentWeapon.Unequip(); // Добавляем вызов Unequip()
				Destroy(currentWeapon); // Уничтожаем предыдущее оружие
			}

			// Создаем новый экземпляр оружия
			currentWeapon = (WeaponClass)gameObject.AddComponent(weaponType);
			currentWeapon.Equip();
		}
	}
	

	public void UseCurrentWeapon()
	{
		if (currentWeapon != null)
		{
			currentWeapon.WeaponAttack();
		}
	}
}