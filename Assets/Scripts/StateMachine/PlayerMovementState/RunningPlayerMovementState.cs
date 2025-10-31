using UnityEngine;

public class RunningPlayerMovementState : PlayerMovementState
{
	public RunningPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		//Debug.Log("Player Running");
		
	}
	public override void ChangePlayerMovementState()
	{
		
		if (playerMovementController.IsPlayerMoving == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerIdle);
		}
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyRun() == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerWalking);
		}
		if (playerMovementController.IsPlayerFalling == true && playerMovementController.playerInputsList.GetKeyCrouch() == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerFalling);
		}
		if (playerMovementController.playerInputsList.GetKeyJump())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerJumping);
		}
		if (playerMovementController.IsPlayerMoving == true && playerMovementController.IsPlayerFalling == false && playerMovementController.playerInputsList.GetKeyCrouch())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerSliding);
		}


	}

	public override void ChangePlayerMovementSpeed()
	{
		playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerRunningSpeed);
	}
}
