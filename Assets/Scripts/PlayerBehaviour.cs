using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	WeaponController weaponController;

	public bool IsPlayerArmed {  get; private set; }

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		weaponController = GetComponent<WeaponController>();

		IsPlayerArmed = false;
	}

	void Update()
	{
		if (playerInputsList.GetKeyShowWeapons())
		{
			if (!IsPlayerArmed)
			{
				ArmPlayer();
			}
			else DisarmPlayer();
		}

		


		//Debug.Log(GetPlayerBehaviour());
		//Debug.Log("Is player armed " + IsPlayerArmed);
	}
	

	public void ArmPlayer()
	{
		if (!IsPlayerArmed)
		{
			IsPlayerArmed = !IsPlayerArmed;
		}

		if (weaponController.RightHandWeapon != null)
		{
			weaponController.ShowWeapon("right");
		}

		if (weaponController.LeftHandWeapon != null)
		{
			weaponController.ShowWeapon("left");
		}


		Debug.Log("PlayerArmed");
	}

	public void DisarmPlayer()
	{
		if (IsPlayerArmed)
		{
			IsPlayerArmed = !IsPlayerArmed;

			if (weaponController.RightHandWeapon != null)
			{
				weaponController.HideWeapon("right");
			}

			if (weaponController.LeftHandWeapon != null)
			{
				weaponController.HideWeapon("left");
			}

			Debug.Log("PlayerDisarmed");
		}
		
	}

}
