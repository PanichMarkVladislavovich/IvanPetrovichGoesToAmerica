using UnityEngine;

public class WeaponHarmonicaRevolver : WeaponClass
{
	public override float WeaponDamage => 30f; // Устанавливаем постоянное значение урона для револьвера

	WeaponHarmonicaRevolver()
    {
        WeaponNameSystem = "HarmonicaRevolver";
		WeaponNameUI = "Револьвер Гармоника";
	}

	public void Awake()
	{
		weaponModel = Resources.Load<GameObject>("WeaponHarmonicaRevolver"); // Загружаем префаб револьвера
		//Debug.Log("Загружен префаб: " + weaponModel);
	}

	public override void WeaponAttack()
	{
		Debug.Log("RevolverAttack");
		// Посылаем луч от положения камеры в направлении её обзора
		RaycastHit hitInfo;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100f))
		{
			// Проверяем, попал ли луч в объект с интерфейсом IDamageable
			IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(WeaponDamage); // Вызываем метод TakeDamage у объекта
			}

		}
	}

}
