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
		playerMovementController.PlayerRigidBody.AddForce(playerMovementController.transform.forward * playerMovementController.PlayerSlidingSpeed, ForceMode.Force);
		playerMovementController.PlayerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;



	}

	public async override void ChangePlayerMovementDelayed()
	{
		Debug.Log("Start");
		await Task.Delay(5000); 
		playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingIdle);
		Debug.Log("End");
		
	}

	public override void PlayerMovementSpeed()
	{
		playerMovementController.SetPlayerMovementSpeed(playerMovementController.PlayerSlidingSpeed);
	}
	
}