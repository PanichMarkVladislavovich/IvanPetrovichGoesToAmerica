using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
	//InputManager playerInputsList;
	WeaponController weaponController;

	public bool IsPlayerArmed { get; private set; } = false;

	void Start()
	{
		//playerInputsList = GetComponent<InputManager>();
		weaponController = GetComponent<WeaponController>();

		
	}

	void Update()
	{
		if (InputManager.Instance.GetKeyShowWeapons())
		{
			if (!IsPlayerArmed && (weaponController.RightHandWeapon != null || weaponController.LeftHandWeapon != null))
			{
				ArmPlayer();
			}
			else DisarmPlayer();
		}

		
	}
	

	public void ArmPlayer()
	{
		if (!IsPlayerArmed)
		{
			IsPlayerArmed = !IsPlayerArmed;


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
