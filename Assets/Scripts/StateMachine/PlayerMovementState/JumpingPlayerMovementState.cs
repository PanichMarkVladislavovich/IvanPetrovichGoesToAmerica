using UnityEngine;

public class JumpingPlayerMovementState : PlayerMovementState
{
	public JumpingPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		Debug.Log("Player Jumping");
	}

	public override void ChangePlayerMovement()
	{
		if (playerMovementController.IsPlayerFalling == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerFalling);
		}

		if (playerMovementController.IsPlayerAbleToClimbLedge == true && playerMovementController.playerInputsList.IsKeyJumpBeingHeld())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerLedgeClimbing);
		}

	}

	public override void PlayerMovementSpeed()
	{
		playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerCrouchingSpeed);
	}
}
