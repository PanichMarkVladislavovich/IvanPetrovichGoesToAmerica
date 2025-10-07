using UnityEngine;

public class LedgeClimbingPlayerMovementState : PlayerMovementState
{
	public LedgeClimbingPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		Debug.Log("Player LedgeClimbing");
	}

	public override void ChangePlayerMovement()
	{

		playerMovementController.StartPlayerLedgeClimbing();
	}

	public override void PlayerMovementSpeed()
	{
		// speed is 0
		//playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerCrouchingSpeed);
	}
}
