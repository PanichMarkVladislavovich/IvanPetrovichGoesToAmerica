using UnityEngine;

public class CrouchingWalkingPlayerMovementState : PlayerMovementState
{
	public CrouchingWalkingPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		//Debug.Log("Player Crouching Walking");
		
	}
	public override void ChangePlayerMovementState()
	{
		if (playerMovementController.IsPlayerMoving == false && playerMovementController.playerInputsList.GetKeyCrouch() == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingIdle);
		}
		if (playerMovementController.IsPlayerMoving == false && playerMovementController.playerInputsList.GetKeyCrouch() == true && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerIdle);
		}
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyCrouch() == true && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerWalking);
		}
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyRun() && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerRunning);
		}
		if (playerMovementController.playerInputsList.GetKeyJump())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerJumping);
		}


	}

	public override void ChangePlayerMovementSpeed()
	{
		playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerCrouchingSpeed);
	}
}
