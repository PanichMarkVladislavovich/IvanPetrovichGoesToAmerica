﻿using UnityEngine;

public class JumpingPlayerMovementState : PlayerMovementState
{
	public JumpingPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;
		//Debug.Log("Player Jumping");
		
	}

	public override void ChangePlayerMovementState()
	{
		if (playerMovementController.IsPlayerFalling == true)
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerFalling);
		}

		if (playerMovementController.IsPlayerAbleToClimbLedge == true && InputManager.Instance.GetKeyJumpBeingHeld())
		{
			playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerLedgeClimbing);
		}

	}

	public override void ChangePlayerMovementSpeed()
	{
		playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerCrouchingSpeed);
	}
}
