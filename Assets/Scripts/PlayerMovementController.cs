using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
	public PlayerInputsList playerInputsList;
	PlayerBehaviour playerBehaviour;
	PlayerCamera playerCamera;
	private Animator playerAnimator;
	private string currentPlayerAnimation = "";
	public float PlayerCurrentHeight { get; private set; }
	public bool IsPLayerSliding { get; private set; }
	public float PlayerCrouchingHeight { get; private set; }
	public float PlayerStandingHeight { get; private set; }
	private float angle;
	private float moveFactor;
	public bool IsPlayerOnSlope { get; private set; }

	public GameObject PlayerCameraObject;
	public Transform PlayerTransform;
	public Rigidbody PlayerRigidBody;

	public Vector3 PlayerWorldMovement;

	public CapsuleCollider playerCapsuleCollider;
	public Transform playerCapsuleColliderMesh;
	
	public float PlayerRotationSpeed { get; private set; }
	public float PlayerDownRayYPosition { get; private set; }
	public float PlayerUpRayYPosition { get; private set; }

	private Vector3 _playerPreviousFramePositionChange;
	private Vector3 _playerPreviousFramePosition;

	//private Vector3 PlayerSlopeMovementDirection;

	private Vector3 PlayerMovementDirectionWithCamera;
	private Vector3 PlayerMovement;
	private RaycastHit hitInfo;
	public bool IsPlayerMoving { get; private set; }
	public bool IsPlayerAbleToMove { get; private set; }
	public bool IsPlayerGrounded { get; private set; }
	public bool IsPlayerAbleToStandUp { get; private set; }
	public bool IsPlayerFalling { get; private set; }
	public bool IsPlayerCrouching { get; private set; }

	public bool IsPlayerAbleToSlide { get; private set; }

	public PlayerMovementStateType playerMovementStateType;
	public PlayerMovementState playerMovementState;

	public float PlayerMovementSpeed { get; private set; }
	public float PlayerWalkingSpeed { get; private set; }
	public float PlayerRunningSpeed { get; private set; }

	public float PlayerSlidingSpeed { get; private set; }
	public float PlayerCrouchingSpeed { get; private set; }
	private void Awake()
	{
		playerMovementStateType = PlayerMovementStateType.PlayerIdle;
	}

	void Start()
	{
		playerAnimator = GetComponent<Animator>();

		IsPlayerAbleToSlide = true;

		PlayerCurrentHeight = 2;
		PlayerCrouchingHeight = 1;
		PlayerStandingHeight = 2;

		ChangePlayerAnimation("Idle");

		SetPlayerMovementState(playerMovementStateType);

		_playerPreviousFramePosition = transform.position;

		playerInputsList = GetComponent<PlayerInputsList>();
		playerBehaviour = GetComponent<PlayerBehaviour>();
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();


		PlayerMovementSpeed = 3f;
		//PlayerMovementSpeed = 5f;
		PlayerWalkingSpeed = 3f;
		PlayerRunningSpeed = 6f;
		PlayerCrouchingSpeed = 1.8f;

		PlayerSlidingSpeed = 7.5f;
	}


	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawRay(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down * 0.2f);
		Gizmos.DrawRay(transform.position + new Vector3(0, PlayerUpRayYPosition, 0), Vector3.up * 0.3f);
	}
	void Update()
	{
		IsPlayerMoving = (Mathf.Abs(_playerPreviousFramePositionChange.x) > 0.001f || Mathf.Abs(_playerPreviousFramePositionChange.z) > 0.001f);

		if (IsPlayerCrouching == false)
		{
			PlayerDownRayYPosition = 0.1f;
			PlayerUpRayYPosition = 1.9f;
		}
		else if (IsPlayerCrouching == true)
		{
			PlayerDownRayYPosition = 0.1f;
			PlayerUpRayYPosition = 0.9f;
		}
		
		IsPlayerGrounded = Physics.Raycast(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down, out hitInfo, 0.3f);

		IsPlayerAbleToStandUp = !Physics.Raycast(transform.position + new Vector3(0, PlayerUpRayYPosition, 0), Vector3.up, out hitInfo, 0.3f);


		/////////////
		IsPlayerFalling = (_playerPreviousFramePositionChange.y < -0.01f && IsPlayerGrounded == false);
		////////////



		if( Physics.Raycast(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down, out hitInfo, 0.3f))
		{
			if (hitInfo.normal != Vector3.up)
			{
				
				IsPlayerOnSlope = true;
			}
			else
			{
				
				IsPlayerOnSlope = false;
			}
		}

		//Debug.Log(IsPlayerOnSlope);

		if (_playerPreviousFramePositionChange.y < -0.01f)
		{
			//Debug.Log("Falling");
		}


		if (playerInputsList.GetKeyJump())
		{
			PlayerRigidBody.AddForce(transform.up * 5, ForceMode.Impulse);
		}

		if (playerInputsList.GetKeyRight())
		{
			PlayerWorldMovement.x = 1;
		}
		else if (playerInputsList.GetKeyLeft())
		{
			PlayerWorldMovement.x = -1;
		}
		else PlayerWorldMovement.x = 0;

		if (playerInputsList.GetKeyUp())
		{
			PlayerWorldMovement.z = 1;
		}
		else if (playerInputsList.GetKeyDown())
		{
			PlayerWorldMovement.z = -1;
		}
		else PlayerWorldMovement.z = 0;

		playerMovementState.ChangePlayerMovement();
		playerMovementState.PlayerMovementSpeed();




		//PlayerSlopeMovementDirection =  Vector3.ProjectOnPlane(PlayerWorldMovement, hitInfo.normal);

		/*
		if (IsPlayerGrounded == true && IsPlayerOnSlope == true)
		{
			PlayerMovementDirectionWithCamera = (PlayerSlopeMovementDirection.z * playerCamera.CameraForward + PlayerSlopeMovementDirection.x * playerCamera.CameraRight);
		}
		else
		{
			PlayerMovementDirectionWithCamera = (PlayerWorldMovement.z * playerCamera.CameraForward + PlayerWorldMovement.x * playerCamera.CameraRight);

		}
		*/
		PlayerMovementDirectionWithCamera = (PlayerWorldMovement.z * playerCamera.CameraForward + PlayerWorldMovement.x * playerCamera.CameraRight);









		//Debug.Log(IsPlayerGrounded);


		//PlayerMovement = new Vector3(PlayerSlopeMovementDirection.x, 0, PlayerSlopeMovementDirection.z);

		


		PlayerMovement = new Vector3(PlayerMovementDirectionWithCamera.x, 0, PlayerMovementDirectionWithCamera.z);

		PlayerMovement.Normalize();


		angle = Vector3.Angle(hitInfo.normal, Vector3.up);
		//Debug.Log(angle);

		moveFactor = 1 / Mathf.Cos(Mathf.Deg2Rad * angle);
		//Debug.Log(moveFactor);

		/*
		Vector3 correction = Vector3.Project(PlayerWorldMovement, hitInfo.normal);
		if (IsPlayerOnSlope == true)
		{
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerWorldMovement * PlayerMovementSpeed * Time.deltaTime - correction * Time.deltaTime);
		}
		else
		{
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * PlayerMovementSpeed * Time.deltaTime);
		}
		*/

		if (IsPlayerGrounded == true && IsPlayerOnSlope == true)
		{
			
			PlayerRigidBody.useGravity = false;
			/////////////!!!!!!!!!!!!!!!!!!!
			////////////////!!!!!!!!!!!!!!!!!!!
			////////////////!!!!!!!!!!!!!!!!!!!
			if (IsPLayerSliding == false)
			{
				PlayerRigidBody.linearVelocity = Vector3.zero;
			}
		}
        else
        {
			PlayerRigidBody.useGravity = true;
		}
		//Debug.Log(PlayerRigidBody.linearVelocity);



		if (IsPlayerOnSlope == true)
		{
			// Просто перемещаем персонажа параллельно поверхности
			Vector3 correctedMovement = PlayerMovement * PlayerMovementSpeed * Time.deltaTime;
			Vector3 projection = Vector3.Project(correctedMovement, hitInfo.normal);

			PlayerRigidBody.MovePosition(PlayerRigidBody.position + correctedMovement - projection);
		}
		else
		{
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * PlayerMovementSpeed * Time.deltaTime);
		}


		/*
        if (IsPlayerOnSlope == true)
		{
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * moveFactor * PlayerMovementSpeed * Time.deltaTime);
		}
		else
		{
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * PlayerMovementSpeed * Time.deltaTime);
		}
		*/


		//PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * PlayerMovementSpeed * Time.deltaTime);



		if ((playerBehaviour.GetPlayerBehaviour() == 0) && (PlayerMovement != Vector3.zero) && (playerCamera.GetCurrentPlayerCameraType() == PlayerCameraStateType.ThirdPerson.ToString()))
		{
			Quaternion CharacterRotation = Quaternion.LookRotation(PlayerMovement, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, CharacterRotation, PlayerRotationSpeed * Time.deltaTime);
		}
		else if ((playerBehaviour.GetPlayerBehaviour() == 1) || (playerCamera.GetCurrentPlayerCameraType() == PlayerCameraStateType.FirstPerson.ToString()))
		{
			Quaternion PlayerRotateWhereCameraIsLooking = Quaternion.Euler(transform.localEulerAngles.x, playerCamera.CameraRotationY, transform.localEulerAngles.z);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, PlayerRotateWhereCameraIsLooking, PlayerRotationSpeed * Time.deltaTime);
		}
	}

	private void FixedUpdate()
	{
		_playerPreviousFramePositionChange = transform.position - _playerPreviousFramePosition;
		_playerPreviousFramePosition = transform.position;
	}

	public void SetPlayerMovementState(PlayerMovementStateType playerMovementStateType)
	{
		PlayerMovementState newState;

		if (playerMovementStateType == PlayerMovementStateType.PlayerIdle)
		{
			newState = new IdlePlayerMovementState(this);
			IsPlayerAbleToMove = true;
			IsPlayerCrouching = false;
			PlayerRotationSpeed = 300f;
			ChangePlayerAnimation("Idle");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerWalking)
		{
			newState = new WalkingPlayerMovementState(this);
			IsPlayerCrouching = false;
			ChangePlayerAnimation("Walking");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerRunning)
		{
			newState = new RunningPlayerMovementState(this);
			IsPlayerCrouching = false;
			ChangePlayerAnimation("Running");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerJumping)
		{
			newState = new JumpingPlayerMovementState(this);
			IsPlayerCrouching = false;
			ChangePlayerAnimation("Jumping");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerFalling)
		{
			newState = new FallingPlayerMovementState(this);
			IsPlayerCrouching = false;
			ChangePlayerAnimation("Falling");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerCrouchingIdle)
		{
			newState = new CrouchingIdlePlayerMovementState(this);
			IsPlayerCrouching = true;
			IsPlayerAbleToMove = true;
			PlayerRotationSpeed = 300f;
			ChangePlayerAnimation("CrouchingIdle");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerCrouchingWalking)
		{
			newState = new CrouchingWalkingPlayerMovementState(this);
			IsPlayerCrouching = true;
			ChangePlayerAnimation("CrouchingWalking");
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerSliding)
		{
			newState = new SlidingPlayerMovementState(this);
			IsPlayerCrouching = true;
			IsPlayerAbleToMove = false;
			PlayerRotationSpeed = 0;
			ChangePlayerAnimation("Sliding");
		}
		else
		{
			newState = null;
		}
		playerMovementState = newState;
	}

	public float SetPlayerMovementSpeed(float SetSpeed)
	{
		PlayerMovementSpeed = SetSpeed;
		return PlayerMovementSpeed;
	}

	IEnumerator PlayerSlidingCourutine()
	{
		IsPlayerAbleToSlide = false;
		IsPLayerSliding = true;

		// Ускоряем игрока
		PlayerRigidBody.AddForce(transform.forward * PlayerSlidingSpeed, ForceMode.Impulse);
		
		// Запрещаем игроку поворачиваться во время скольжения
		PlayerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		
		yield return new WaitForSeconds(1f);

		// В сконце скольжения резко останавливаем игрока
		PlayerRigidBody.AddForce(Vector3.zero, ForceMode.Acceleration);
		PlayerRigidBody.linearVelocity = Vector3.zero;
		PlayerRigidBody.angularVelocity = Vector3.zero;
		PlayerRigidBody.MovePosition(PlayerRigidBody.transform.position);

		
		// StateMachine меняется на CrouchingIdle
		SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingIdle);



		IsPlayerAbleToSlide = true;
		IsPLayerSliding = false;
	}

	// а вот эту функцию с корутиной вызывает StateMachine которая НЕ monobehaviour
	public void StartPlayerSliding()
	{
		StartCoroutine(PlayerSlidingCourutine());
	}

	private void ChangePlayerAnimation(string animation, float crossfade = 0.2f)
	{
		if(currentPlayerAnimation != animation)
		{
			currentPlayerAnimation = animation;
			playerAnimator.CrossFade(animation, crossfade);
		}
	}
}
