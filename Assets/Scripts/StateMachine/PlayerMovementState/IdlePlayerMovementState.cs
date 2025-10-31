using UnityEngine;

public class IdlePlayerMovementState : PlayerMovementState
{ 
	
	public IdlePlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		
		//Debug.Log("Player Idle");
		
	}


	public override void ChangePlayerMovementState()
	{
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyRun() == false)
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
		if (playerMovementController.IsPlayerFalling == true && playerMovementController.playerInputsList.GetKeyCrouch() == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerFalling);
		}
		
		if (playerMovementController.IsPlayerMoving == false && playerMovementController.playerInputsList.GetKeyCrouch())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingIdle);
		}
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyCrouch())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingWalking);
		}
		
	}

	public override void ChangePlayerMovementSpeed()
	{
		// just idle
	}

	
}




