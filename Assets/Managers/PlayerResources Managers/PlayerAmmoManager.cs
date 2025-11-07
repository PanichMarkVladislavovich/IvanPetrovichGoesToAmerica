using TMPro;
using UnityEngine;

public class PlayerAmmoManager : MonoBehaviour
{
	public static PlayerAmmoManager Instance { get; private set; } // Статическое поле экземпляра\

	public TMP_Text PlayerAmmoText;


	public int PlayerAmmoTotal { get; private set; }
	public int PlayerAmmoMagazine { get; private set; } = 5;
	public int PlayerAmmoReserve { get; private set; }






	private void Awake()
	{
		// Паттерн Singleton: предотвращаем создание второго экземпляра
		if (Instance == null)
		{
			Instance = this;
			//DontDestroyOnLoad(gameObject); // Сохраняется при смене уровней
		}
		else
		{
			Destroy(gameObject); // Уничтожаем лишние экземпляры
		}
	}


	public void Shoot(float weaponDamage)
	{
		if (PlayerAmmoMagazine > 0)
		{
			// Посылаем луч от положения камеры в направлении её обзора
			RaycastHit hitInfo;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100f))
			{
				// Проверяем, попал ли луч в объект с интерфейсом IDamageable
				IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
				if (damageable != null)
				{
					damageable.TakeDamage(weaponDamage); // Вызываем метод TakeDamage у объекта
				}

			}
			Debug.Log("RevolverAttack");
			PlayerAmmoMagazine--;

			Debug.Log($"Magazine ammo remaining: {PlayerAmmoMagazine}");
		}
		else if (PlayerAmmoMagazine == 0)
		{
			Debug.Log("Not enought Ammo");
		}

	}


}
