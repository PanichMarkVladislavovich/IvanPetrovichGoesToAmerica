using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
	public CapsuleCollider PlayerColliderCapsuleObject;
	PlayerMovementController playerMovementController;
	// NEED TO REFACTOR THIS AS DIFFERENT SCRIPT



	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		playerMovementController = GetComponent<PlayerMovementController>();
	}

	// Update is called once per frame
	void Update()
	{
		// Debug.Log(PlayerColliderCapsuleObject.height);

		if (playerMovementController.IsPlayerCrouching == false)
		{
			//PlayerDownRayYPosition = -0.9f;
			//PlayerUpRayYPosition = 0.9f;

			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);

			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);
			//playerCapsuleCollider.height = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);
		}
		else if (playerMovementController.IsPlayerCrouching == true)
		{
			//PlayerDownRayYPosition = -0.4f;
			//PlayerUpRayYPosition = 0.4f;

			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);

			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);
			//playerCapsuleCollider.height = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);


		}
	}
}
