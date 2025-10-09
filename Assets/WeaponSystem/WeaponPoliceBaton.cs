using UnityEngine;

public class WeaponPoliceBaton : WeaponClass
{
    public WeaponPoliceBaton()
    {
        WeaponName = "PoliceBaton";
		//weaponModel = Resources.Load<GameObject>("Prefabs/PoliceBaton"); // Загружаем префаб дубинки
	}

	public void Awake()
	{
		weaponModel = Resources.Load<GameObject>("WeaponPoliceBaton"); // Загружаем префаб револьвера
		//Debug.Log("Загружен префаб: " + weaponModel);
	}

	public override void WeaponAttack()
	{
		Debug.Log("PoliceBatonAttack");
	}
}
