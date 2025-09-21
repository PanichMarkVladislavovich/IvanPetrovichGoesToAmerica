using UnityEngine;
using UnityEngine.InputSystem.XR;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class SlidingPlayerMovementState : PlayerMovementState
{
	

	public SlidingPlayerMovementState(PlayerMovementController playerMovementController)
	{
		this.playerMovementController = playerMovementController;

		Debug.Log("Player Sliding");

	}
	public override void ChangePlayerMovement()
	{
		if (playerMovementController.IsPlayerAbleToSlide == true)
		{
			
			playerMovementController.StartPlayerSliding();
		}
	}

	public override void PlayerMovementSpeed()
	{
		playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerSlidingSpeed);
	}
	
}