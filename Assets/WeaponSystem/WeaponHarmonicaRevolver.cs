using UnityEngine;

public class WeaponHarmonicaRevolver : WeaponClass
{
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
	}

}
