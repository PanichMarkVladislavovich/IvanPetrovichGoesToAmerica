
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	public PlayerInputsList playerInputsList;
	
	public PlayerMovementController playerMovementController;

	public PlayerCamera playerCamera;
	public GameObject playerCameraObject;

	private Animator playerAnimator;
	private string currentPlayerAnimation = "";
	
	public PlayerBehaviour playerBehaviour;

	public WeaponController weaponController;

	float lmao;
	void Start()
    {
		playerInputsList = GetComponent<PlayerInputsList>();

		playerMovementController = GetComponent<PlayerMovementController>();

		playerCamera = playerCameraObject.GetComponent<PlayerCamera>();

		playerAnimator = GetComponent<Animator>();
		ChangePlayerAnimation("Idle");

		playerBehaviour = GetComponent<PlayerBehaviour>();

		weaponController = GetComponent<WeaponController>();
	}

	private void Update()
	{
		// считаем поворот камеры X
		float bruh = playerCameraObject.transform.rotation.eulerAngles.x;
		if (bruh >= 0 && bruh < 180)
		{
			lmao = bruh;
		}
		else if (bruh < 360 && bruh > -180)
		{
			lmao = bruh - 360;
		}
	
		// игрок смотрит вниз/вверх когда вооружен от 3го лица
		if (playerBehaviour.IsPlayerArmed == true && playerCamera.GetCurrentPlayerCameraType() == PlayerCameraStateType.ThirdPerson.ToString())
		{
			// Шаг 1: Определяем начальное значение (текущее значение параметра)
			float startValue = playerAnimator.GetFloat("UpDown");

			// Шаг 2: Рассчитываем целевое значение на основе угла камеры
			float endValue = lmao * 0.0153846f;

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

		if (playerBehaviour.IsPlayerArmed == true && playerCamera.GetCurrentPlayerCameraType() == PlayerCameraStateType.FirstPerson.ToString())
		{
			playerAnimator.SetFloat("UpDown", 0);
		}



			// анимации PlayerMovement state машины
		if (playerMovementController.CurrentPlayerMovementStateType == "Idle")
		{
			
			ChangePlayerAnimation("Idle");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "Walking")
		{
			if (playerBehaviour.IsPlayerArmed == true || (playerCamera.GetCurrentPlayerCameraType() == PlayerCameraStateType.FirstPerson.ToString()))
			{
				if (playerInputsList.GetKeyUp())
				{
					ChangePlayerAnimation("Walking Forward");
				}
				else if (playerInputsList.GetKeyDown())
				{
					ChangePlayerAnimation("Walking Backwards");
				}
				if (playerInputsList.GetKeyRight())
				{
					ChangePlayerAnimation("Walking Right");
				}
				else if (playerInputsList.GetKeyLeft())
				{
					ChangePlayerAnimation("Walking Left");
				}
			}
			else ChangePlayerAnimation("Walking Forward");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "Running")
		{

			ChangePlayerAnimation("Running");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "Jumping")
		{

			ChangePlayerAnimation("Jumping");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "Falling")
		{

			ChangePlayerAnimation("Falling");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "CrouchingIdle")
		{

			ChangePlayerAnimation("CrouchingIdle");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "CrouchingWalking")
		{

			ChangePlayerAnimation("CrouchingWalking");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "Sliding")
		{

			ChangePlayerAnimation("Sliding");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "LedgeClimbing")
		{
			ChangePlayerAnimation("Ledge Climbing");
		}




		// анимации оружия
		if (weaponController.RightHandWeapon != null)
		{
			if (weaponController.RightHandWeapon.weaponMeshRenderer.enabled)
			{
				playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponRight"), 1);
				ChangePlayerAnimation("EquipRightWeapon");
			}
			else
			{
				ChangePlayerAnimation("UnequipRightWeapon");
				if (playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponRight")).IsName("UnequipRightWeapon") && playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponRight")).normalizedTime >= 0.99f)
				{
					playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponRight"), 0);
				}
			}
		}

		if (weaponController.LeftHandWeapon != null)
		{
			if (weaponController.LeftHandWeapon.weaponMeshRenderer.enabled)
			{
				playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponLeft"), 1);
				ChangePlayerAnimation("EquipLeftWeapon");
			}
			else
			{
				ChangePlayerAnimation("UnequipLeftWeapon");
				if (playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponLeft")).IsName("UnequipLeftWeapon") && playerAnimator.GetCurrentAnimatorStateInfo(playerAnimator.GetLayerIndex("WeaponLeft")).normalizedTime >= 0.99f)
				{
					playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("WeaponLeft"), 0);
				}
			}
		}








	}
		private void ChangePlayerAnimation(string animation, float crossfade = 0f)
		{
			if (currentPlayerAnimation != animation)
			{
				currentPlayerAnimation = animation;
				playerAnimator.CrossFade(animation, crossfade);
			}
		}
	}

