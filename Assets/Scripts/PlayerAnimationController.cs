using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	private Animator playerAnimator;
	private string currentPlayerAnimation = "";
	public PlayerMovementController playerMovementController;
	public PlayerInputsList playerInputsList;
	public PlayerBehaviour playerBehaviour;
	public PlayerCamera playerCamera;
	public GameObject PlayerCameraObject;

	void Start()
    {

		
		playerBehaviour = GetComponent<PlayerBehaviour>();
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();
		playerMovementController = GetComponent<PlayerMovementController>();
		playerInputsList = GetComponent<PlayerInputsList>();
		playerAnimator = GetComponent<Animator>();
		ChangePlayerAnimation("Idle");
	}

	private void Update()
	{
		if (playerMovementController.CurrentPlayerMovementStateType == "Idle")
		{
			
			ChangePlayerAnimation("Idle");
		}
		else if (playerMovementController.CurrentPlayerMovementStateType == "Walking")
		{
			if ((playerBehaviour.GetPlayerBehaviour() == 1) || (playerCamera.GetCurrentPlayerCameraType() == PlayerCameraStateType.FirstPerson.ToString()))
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
	}
	

		private void ChangePlayerAnimation(string animation, float crossfade = 0.2f)
		{
			if (currentPlayerAnimation != animation)
			{
				currentPlayerAnimation = animation;
				playerAnimator.CrossFade(animation, crossfade);
			}
		}
	}

