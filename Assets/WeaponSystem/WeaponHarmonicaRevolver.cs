using UnityEngine;

public class WeaponHarmonicaRevolver : WeaponClass
{
    WeaponHarmonicaRevolver()
    {
        WeaponName = "HarmonicaRevolver";
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
