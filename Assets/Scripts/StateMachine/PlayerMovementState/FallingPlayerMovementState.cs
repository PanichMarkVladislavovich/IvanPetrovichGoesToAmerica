using UnityEngine;

public class FallingPlayerMovementState : PlayerMovementState
{
	public FallingPlayerMovementState(PlayerMovementController playerMovementController)
	{
	this.playerMovementController = playerMovementController;
	Debug.Log("Player Falling");
	}

	public override void ChangePlayerMovement()
	{
		if (playerMovementController.IsPlayerFalling == false && playerMovementController.IsPlayerMoving == false)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerIdle);
		}
		
		if (playerMovementController.IsPlayerFalling == false && playerMovementController.IsPlayerMoving == true && playerMovementController.playerInputsList.GetKeyRun())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerRunning);
		}
		 if (playerMovementController.IsPlayerFalling == false && playerMovementController.IsPlayerMoving == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerWalking);
		}
		//if (playerMovementController.playerInputsList.GetKeyJump())
		//{
		//	playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerJumping);
		//}

	}

	public override void PlayerMovementSpeed()
	{

	}
}
