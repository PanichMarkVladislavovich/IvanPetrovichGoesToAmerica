using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public string WeaponName;
    public float WeaponDamage;

 public virtual void WeaponAttack()
    {

    }

	public virtual void Equip()
	{
		Debug.Log(WeaponName + " Equiped");
	}

	public virtual void Unequip()
	{
		Debug.Log(WeaponName + " Unequiped");
	}
}
