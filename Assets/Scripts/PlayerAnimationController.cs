﻿
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	//public InputManager playerInputsList;
	
	public PlayerMovementController playerMovementController;

	public PlayerCamera playerCamera;
	public GameObject playerCameraObject;

	private Animator playerAnimator;
	private string currentPlayerMovementAnimation = "";
	private string currentPlayerWeaponRightAnimation = "";
	private string currentPlayerWeaponLeftAnimation = "";
	private string currentPlayerLegKickAttackAnimation = "";

	public PlayerBehaviour playerBehaviour;

	public WeaponController weaponController;

	private bool wasPreviouslyKicking = false;



	private float adjustedCameraAngle;
	void Start()
    {
		//playerInputsList = GetComponent<InputManager>();

		playerMovementController = GetComponent<PlayerMovementController>();

		playerCamera = playerCameraObject.GetComponent<PlayerCamera>();

		playerAnimator = GetComponent<Animator>();
		ChangePlayerMovementAnimation("Idle");

		playerBehaviour = GetComponent<PlayerBehaviour>();

		weaponController = GetComponent<WeaponController>();


	}

	private void Update()
	{
		if (playerCameraObject != null)
		{
			// считаем поворот камеры X
			float cameraRotationX = playerCameraObject.transform.rotation.eulerAngles.x;
			if (cameraRotationX >= 0 && cameraRotationX < 180)
			{
				adjustedCameraAngle = cameraRotationX;
			}
			else if (cameraRotationX < 360 && cameraRotationX > -180)
			{
				adjustedCameraAngle = cameraRotationX - 360;
			}
		}
	
		// игрок смотрит вниз/вверх когда вооружен от 3го лица
		if (playerBehaviour.IsPlayerArmed == true && playerCamera.CurrentPlayerCameraStateType == "ThirdPerson")
		{
			// Шаг 1: Определяем начальное значение (текущее значение параметра)
			float startValue = playerAnimator.GetFloat("UpDown");

			// Шаг 2: Рассчитываем целевое значение на основе угла камеры
			float endValue = adjustedCameraAngle * 0.0153846f;

			// Шаг 3: Интерполируем значение
			float newValue = Mathf.Lerp(startValue, endValue, Time.deltaTime * 6);

			// Шаг 4: Обновляем параметр в аниматоре
			playerAnimator.SetFloat("UpDown", newValue);
		}
		else
		{
			// Шаг 1: Определяем начальное значение (текущее значение параметра)
			float startValue = playerAnimator.GetFloat("UpDown");

			// Шаг 2: Целевым значением теперь становится ноль
			float endValue = 0f;

			// Шаг 3: Интерполируем значение от текущего до нуля
			float newValue = Mathf.Lerp(startValue, endValue, Time.deltaTime * 6);

			// Шаг 4: Обновляем параметр в аниматоре
			playerAnimator.SetFloat("UpDown", newValue);
		}

		if (playerBehaviour.IsPlayerArmed == true && playerCamera.CurrentPlayerCameraStateType == "FirstPerson")
		{
			playerAnimator.SetFloat("UpDown", 0);
		}



			// анимации PlayerMovement state машины
		if (playerMovementController.CurrentPlayerMovementStateType == "PlayerIdle")
		{
			
			ChangePlayerMovementAnimation("Idle");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerWalking")
		{
			if (playerBehaviour.IsPlayerArmed == true || (playerCamera.CurrentPlayerCameraStateType == "FirstPerson"))
			{
				if (InputManager.Instance.GetKeyUp())
				{
					ChangePlayerMovementAnimation("Walking Forward");
				}
				else if (InputManager.Instance.GetKeyDown())
				{
					ChangePlayerMovementAnimation("Walking Backwards");
				}
				if (InputManager.Instance.GetKeyRight())
				{
					ChangePlayerMovementAnimation("Walking Right");
				}
				else if (InputManager.Instance.GetKeyLeft())
				{
					ChangePlayerMovementAnimation("Walking Left");
				}
			}
			else ChangePlayerMovementAnimation("Walking Forward");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerRunning")
		{

			ChangePlayerMovementAnimation("Running");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerJumping")
		{

			ChangePlayerMovementAnimation("Jumping");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerFalling")
		{

			ChangePlayerMovementAnimation("Falling");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerCrouchingIdle")
		{

			ChangePlayerMovementAnimation("CrouchingIdle");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerCrouchingWalking")
		{

			ChangePlayerMovementAnimation("CrouchingWalking");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerSliding")
		{

			ChangePlayerMovementAnimation("Sliding");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "PlayerLedgeClimbing")
		{
			ChangePlayerMovementAnimation("Ledge Climbing");
		}




		// анимации оружия
		if (weaponController.RightHandWeapon != null)
		{
			if (weaponController.RightHandWeapon.ThirdPersonWeaponModelInstance.activeInHierarchy)
			{
				playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponRight"), 1);
				ChangePlayerWeaponRightAnimation("EquipRightWeapon");
			}
			else 
			{
				ChangePlayerWeaponRightAnimation("UnequipRightWeapon");
				if (playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponRight")).IsName("UnequipRightWeapon") && playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponRight")).normalizedTime >= 0.99f)
				{
					playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponRight"), 0);
				}
			}
		}
		else
		{
				ChangePlayerWeaponRightAnimation("UnequipRightWeapon");
				if (playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponRight")).IsName("UnequipRightWeapon") && playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponRight")).normalizedTime >= 0.99f)
				{
					playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponRight"), 0);
				}
		}

		if (weaponController.LeftHandWeapon != null)
		{
			if (weaponController.LeftHandWeapon.ThirdPersonWeaponModelInstance.activeInHierarchy)
			{
				playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponLeft"), 1);
				ChangePlayerWeaponLeftAnimation("EquipLeftWeapon");
			}
			else 
			{
				ChangePlayerWeaponLeftAnimation("UnequipLeftWeapon");
				
				if (playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponLeft")).IsName("UnequipLeftWeapon") && playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponLeft")).normalizedTime >= 0.99f)
				{
					playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponLeft"), 0);
				}
			}
		}
		else
		{
			ChangePlayerWeaponLeftAnimation("UnequipLeftWeapon");

			if (playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponLeft")).IsName("UnequipLeftWeapon") && playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponLeft")).normalizedTime >= 0.99f)
			{
				playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponLeft"), 0);
			}
		}

		//Анимация атаки ногой
		if (playerMovementController.IsPlayerLegKicking == true)
		{
			
			playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("LegKick"), 1);

			if (!wasPreviouslyKicking)
			{
				playerAnimator.Play("LegKick", 4, 0f); // третий аргумент сбрасывает анимацию на начало
				//Debug.Log("LMAO");
			}
			
			//wasPreviouslyKicking = true;
			// Начинаем анимацию сначала
			
			//ChangePlayerLegKickAttackAnimation("LegKick");
			playerAnimator.Play("LegKick");
		}
		else if (playerMovementController.IsPlayerLegKicking == false)
		{
			playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("LegKick"), 0);
			//playerAnimator.Play("New State");
			//ChangePlayerLegKickAttackAnimation("New State");

		}
		wasPreviouslyKicking = playerMovementController.IsPlayerLegKicking;




	}
		private void ChangePlayerMovementAnimation(string animation, float crossfade = 0.2f)
		{
			if (currentPlayerMovementAnimation != animation)
			{
				currentPlayerMovementAnimation = animation;
				playerAnimator.CrossFade(animation, crossfade);
			}
		}


	private void ChangePlayerWeaponRightAnimation(string animation, float crossfade = 0.2f)
	{
		if (currentPlayerWeaponRightAnimation != animation)
		{
			currentPlayerWeaponRightAnimation = animation;
			playerAnimator.CrossFade(animation, crossfade);
		}
	}

	private void ChangePlayerWeaponLeftAnimation(string animation, float crossfade = 0.2f)
	{
		if (currentPlayerWeaponLeftAnimation != animation)
		{
			currentPlayerWeaponLeftAnimation = animation;
			playerAnimator.CrossFade(animation, crossfade);
		}
	}

	private void ChangePlayerLegKickAttackAnimation(string animation, float crossfade = 0.2f)
	{
		if (currentPlayerLegKickAttackAnimation != animation)
		{
			currentPlayerLegKickAttackAnimation = animation;
			playerAnimator.CrossFade(animation, crossfade);
		}
	}
}

