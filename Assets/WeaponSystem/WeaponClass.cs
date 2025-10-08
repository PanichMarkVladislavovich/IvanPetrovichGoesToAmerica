using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string WeaponName;
    public float WeaponDamage;
	public GameObject weaponModel; // Ссылка на 3D модель оружия
	private GameObject currentModelInstance; // Ссылка на инстанцированную модель

	public virtual void WeaponAttack()
    {

    }

	public virtual void Equip()
	{
		Debug.Log(WeaponName + " Equiped");
		InstantiateWeaponModel();
	}

	public virtual void Unequip()
	{
		Debug.Log(WeaponName + " Unequiped");
		DestroyWeaponModel();
	}

	protected void InstantiateWeaponModel()
	{
		if (weaponModel != null)
		{
			currentModelInstance = Instantiate(weaponModel);
			currentModelInstance.transform.parent = transform;
			currentModelInstance.transform.localPosition = new Vector3(0, 1.75f, 0.5f); // Локальная позиция относительно персонажа
			currentModelInstance.transform.localRotation = Quaternion.Euler(0, 90, 0); // Локальное вращение
		}
	}

	protected void DestroyWeaponModel()
	{
		if (weaponModel != null)
		{
			Destroy(currentModelInstance);
			currentModelInstance = null;
		}
	}
}
