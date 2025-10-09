using UnityEngine;

public class WeaponPlungerCrossbow : WeaponClass
{
    public WeaponPlungerCrossbow()
    {
        WeaponName = "PlungerCrossbow";
    }

	public void Awake()
	{
		weaponModel = Resources.Load<GameObject>("WeaponPlungerCrossbow"); // Загружаем префаб револьвера
		//Debug.Log("Загружен префаб: " + weaponModel);
	}

	public override void WeaponAttack()
	{
		Debug.Log("CrossbowAttack");
	}
}
