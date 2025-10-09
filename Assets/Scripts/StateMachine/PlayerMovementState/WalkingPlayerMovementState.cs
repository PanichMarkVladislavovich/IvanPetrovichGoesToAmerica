using UnityEngine;

public class WalkingPlayerMovementState: PlayerMovementState
{
	
	public WalkingPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		//Debug.Log("Player Walking");
		
	}
	public override void ChangePlayerMovementState()
	{
		if (playerMovementController.IsPlayerMoving == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerIdle);
		}
		
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyRun() && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerRunning);
		}
		
		if (playerMovementController.IsPlayerFalling == true  && playerMovementController.playerInputsList.GetKeyCrouch() == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerFalling);
		}
		
		if (playerMovementController.playerInputsList.GetKeyJump())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerJumping);
		}
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyCrouch())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingWalking);

		}
		if (playerMovementController.IsPlayerMoving == false && playerMovementController.playerInputsList.GetKeyCrouch())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingIdle);
		}


	}

	public override void ChangePlayerMovementSpeed()
	{
		
			playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerWalkingSpeed);
		
		
	}
}

