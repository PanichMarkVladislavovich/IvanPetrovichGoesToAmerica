using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
	public PlayerInputsList playerInputsList;

	public PlayerMovementState playerMovementState;
	public PlayerMovementStateType playerMovementStateType;

	public Vector3 PlayerWorldMovement;
	private Vector3 PlayerMovement;
	private Vector3 PlayerMovementDirectionWithCamera;

	public PlayerCamera playerCamera;
	public GameObject PlayerCameraObject;

	public PlayerBehaviour playerBehaviour;

	public Transform PlayerTransform;
	public Rigidbody PlayerRigidBody;
	public CapsuleCollider playerCapsuleCollider;
	public Transform playerCapsuleColliderMesh;

	private Vector3 _playerPreviousFramePosition;
	private Vector3 _playerPreviousFramePositionChange;

	private RaycastHit hitInfo;

	public string CurrentPlayerMovementStateType { get; private set; }

	public float PlayerMovementSpeed { get; private set; }
	public float PlayerRotationSpeed { get; private set; }
	public float PlayerWalkingSpeed { get; private set; }
	public float PlayerRunningSpeed { get; private set; }
	public float PlayerCrouchingSpeed { get; private set; }
	public float PlayerSlidingSpeed { get; private set; }

	public float PlayerCurrentHeight { get; private set; }
	public float PlayerStandingHeight { get; private set; }
	public float PlayerCrouchingHeight { get; private set; }

	public bool IsPlayerMoving { get; private set; }
	public bool IsPlayerAbleToMove { get; private set; }
	public bool IsPlayerGrounded { get; private set; }
	public bool IsPlayerCrouching { get; private set; }
	public bool IsPlayerAbleToStandUp { get; private set; }
	public bool IsPlayerFalling { get; private set; }
	public bool IsPLayerSliding { get; private set; }
	public bool IsPlayerAbleToSlide { get; private set; }
	public bool IsPlayerAbleToClimbLedge { get; private set; }
	public bool IsPlayerOnSlope { get; private set; }

	public float PlayerUpRayYPosition { get; private set; }
	public float PlayerDownRayYPosition { get; private set; }
	
	private float angle;
	private float moveFactor;

	private void Awake()
	{
		playerMovementStateType = PlayerMovementStateType.PlayerIdle;
	}

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();
		playerBehaviour = GetComponent<PlayerBehaviour>();

		_playerPreviousFramePosition = transform.position;

		SetPlayerMovementState(playerMovementStateType);

		PlayerMovementSpeed = 3f;
		PlayerWalkingSpeed = 3f;
		PlayerRunningSpeed = 6f;
		PlayerCrouchingSpeed = 1.8f;
		PlayerSlidingSpeed = 7.5f;

		PlayerCurrentHeight = 2;
		PlayerCrouchingHeight = 1;
		PlayerStandingHeight = 2;

		IsPlayerAbleToSlide = true;
	}

	void OnDrawGizmos()
	{
		/*
		Gizmos.color = Color.red;
		
		Gizmos.DrawRay(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down * 0.2f);
		Gizmos.DrawRay(transform.position + new Vector3(0, PlayerUpRayYPosition, 0), Vector3.up * 0.3f);

		Gizmos.DrawCube(transform.position + transform.up * 1.75f + transform.forward * 0.75f + transform.right * -0.4f, new Vector3(0.25f, 0.25f, 0.25f));
		Gizmos.DrawCube(transform.position + transform.up * 1.75f + transform.forward * 1.5f + transform.right * -0.4f, new Vector3(0.25f, 0.25f, 0.25f));
		Gizmos.DrawCube(transform.position + transform.up * 1.75f + transform.forward * 0.75f + transform.right * 0.4f, new Vector3(0.25f, 0.25f, 0.25f));
		Gizmos.DrawCube(transform.position + transform.up * 1.75f + transform.forward * 1.5f + transform.right * 0.4f, new Vector3(0.25f, 0.25f, 0.25f));

		Gizmos.DrawCube(transform.position + transform.forward * 1.1f + new Vector3(0, 3, 0), new Vector3(1.25f, 2.25f, 1.25f));

		Gizmos.DrawCube(transform.position + transform.forward * 1.1f + new Vector3(0, 2.5f, 0), new Vector3(1.25f, 1.25f, 1.25f));
		*/
	}
	void Update()
	{
		// Player movement State Machine methods
		playerMovementState.ChangePlayerMovementState();
		playerMovementState.ChangePlayerMovementSpeed();

		//
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

		if (playerInputsList.GetKeyJump())
		{
			PlayerRigidBody.AddForce(transform.up * 5, ForceMode.Impulse);
		}

		//
		IsPlayerMoving = (Mathf.Abs(_playerPreviousFramePositionChange.x) > 0.001f || Mathf.Abs(_playerPreviousFramePositionChange.z) > 0.001f);
		IsPlayerGrounded = Physics.Raycast(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down, out hitInfo, 0.3f);
		IsPlayerAbleToStandUp = !Physics.Raycast(transform.position + new Vector3(0, PlayerUpRayYPosition, 0), Vector3.up, out hitInfo, 0.3f);
		IsPlayerFalling = (_playerPreviousFramePositionChange.y < -0.01f && IsPlayerGrounded == false);
		
		if (IsPlayerCrouching == false)
		{
			PlayerDownRayYPosition = 0.1f;
			PlayerUpRayYPosition = 1.9f;
		}
		else if (IsPlayerCrouching == true)
		{
			PlayerDownRayYPosition = 0.1f;
			PlayerUpRayYPosition = 1.2f;
		}
		
		if (IsPlayerGrounded == true && IsPlayerOnSlope == true)
		{
			PlayerRigidBody.useGravity = false;
			if (IsPLayerSliding == false)
			{
				PlayerRigidBody.linearVelocity = Vector3.zero;
			}
		}
        else
        {
			PlayerRigidBody.useGravity = true;
		}

		// Ledge Climbing BoxCast collision check
		bool isAllBoxesColliding;
		bool isBigRectangleClear;
		bool isSmallRectangleClear;

		if (
			Physics.OverlapBox(transform.position + transform.up * 1.75f + transform.forward * 0.75f + transform.right * -0.4f, new Vector3(0.25f, 0.25f, 0.25f) * 0.5f, Quaternion.identity).Length == 0 ||
			Physics.OverlapBox(transform.position + transform.up * 1.75f + transform.forward * 1.5f + transform.right * -0.4f, new Vector3(0.25f, 0.25f, 0.25f) * 0.5f, Quaternion.identity).Length == 0 ||
			Physics.OverlapBox(transform.position + transform.up * 1.75f + transform.forward * 0.75f + transform.right * 0.4f, new Vector3(0.25f, 0.25f, 0.25f) * 0.5f, Quaternion.identity).Length == 0 ||
			Physics.OverlapBox(transform.position + transform.up * 1.75f + transform.forward * 1.5f + transform.right * 0.4f, new Vector3(0.25f, 0.25f, 0.25f) * 0.5f, Quaternion.identity).Length == 0
			)
		{
			isAllBoxesColliding = false;
		}
		else isAllBoxesColliding = true;

		if (Physics.OverlapBox(transform.position + transform.forward * 1.1f + new Vector3(0, 3, 0), new Vector3(1.25f, 2.25f, 1.25f) * 0.5f, Quaternion.identity).Length > 0)
		{
			isBigRectangleClear = false;
		}
		else isBigRectangleClear = true;

		if (Physics.OverlapBox(transform.position + transform.forward * 1.1f + new Vector3(0, 2.5f, 0), new Vector3(1.25f, 1.25f, 1.25f) * 0.5f, Quaternion.identity).Length > 0)
		{
			isSmallRectangleClear = false;
		}
		else isSmallRectangleClear = true;

		if (isAllBoxesColliding && (isBigRectangleClear || isSmallRectangleClear) && playerMovementStateType != PlayerMovementStateType.PlayerLedgeClimbing)
		{
			IsPlayerAbleToClimbLedge = true;
		}
		else
		{
			IsPlayerAbleToClimbLedge = false;
		}

		// Slope 
		if ( Physics.Raycast(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down, out hitInfo, 0.3f))
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

		if (IsPlayerOnSlope == true)
		{
			Vector3 correctedMovement = PlayerMovement * PlayerMovementSpeed * Time.deltaTime;
			Vector3 projection = Vector3.Project(correctedMovement, hitInfo.normal);
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + correctedMovement - projection);
		}
		else
		{
			PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * PlayerMovementSpeed * Time.deltaTime);
		}

		//
		PlayerMovementDirectionWithCamera = (PlayerWorldMovement.z * playerCamera.CameraForward + PlayerWorldMovement.x * playerCamera.CameraRight);
		PlayerMovement = new Vector3(PlayerMovementDirectionWithCamera.x, 0, PlayerMovementDirectionWithCamera.z);
		PlayerMovement.Normalize();

		angle = Vector3.Angle(hitInfo.normal, Vector3.up);
		moveFactor = 1 / Mathf.Cos(Mathf.Deg2Rad * angle);
	
		//
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
			CurrentPlayerMovementStateType = "Idle";
			IsPlayerAbleToMove = true;
			IsPlayerCrouching = false;
			PlayerRotationSpeed = 300f;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerWalking)
		{
			newState = new WalkingPlayerMovementState(this);
			CurrentPlayerMovementStateType = "Walking";
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerRunning)
		{
			newState = new RunningPlayerMovementState(this);
			CurrentPlayerMovementStateType = "Running";
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerJumping)
		{
			newState = new JumpingPlayerMovementState(this);
			CurrentPlayerMovementStateType = "Jumping";
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerFalling)
		{
			newState = new FallingPlayerMovementState(this);
			CurrentPlayerMovementStateType = "Falling";
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerCrouchingIdle)
		{
			newState = new CrouchingIdlePlayerMovementState(this);
			CurrentPlayerMovementStateType = "CrouchingIdle";
			IsPlayerCrouching = true;
			IsPlayerAbleToMove = true;
			PlayerRotationSpeed = 300f;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerCrouchingWalking)
		{
			newState = new CrouchingWalkingPlayerMovementState(this);
			CurrentPlayerMovementStateType = "CrouchingWalking";
			IsPlayerCrouching = true;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerSliding)
		{
			newState = new SlidingPlayerMovementState(this);
			CurrentPlayerMovementStateType = "Sliding";
			IsPlayerCrouching = true;
			IsPlayerAbleToMove = false;
			PlayerRotationSpeed = 0;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerLedgeClimbing)
		{
			newState = new LedgeClimbingPlayerMovementState(this);
			CurrentPlayerMovementStateType = "LedgeClimbing";
			IsPlayerCrouching = false;
			IsPlayerAbleToMove = false;
			PlayerRotationSpeed = 0;
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

	IEnumerator PlayerLedgeClimbingCourutine()
	{
		
		bool Big;

		if (Physics.OverlapBox(transform.position + transform.forward * 1.1f + new Vector3(0, 3, 0), new Vector3(1.25f, 2.25f, 1.25f) * 0.5f, Quaternion.identity).Length > 0)
		{
			Big = false;
		}
		else Big = true;

		//Debug.Log(Big);
			PlayerRigidBody.AddForce(transform.up * 1.01f, ForceMode.Impulse);


		yield return new WaitForSeconds(0.25f);

		PlayerRigidBody.AddForce(Vector3.zero, ForceMode.Acceleration);
		PlayerRigidBody.linearVelocity = Vector3.zero;
		PlayerRigidBody.angularVelocity = Vector3.zero;
		PlayerRigidBody.MovePosition(PlayerRigidBody.transform.position);

		PlayerRigidBody.AddForce(transform.forward * 1.01f, ForceMode.Impulse);

		yield return new WaitForSeconds(0.1f);
		
		PlayerRigidBody.AddForce(Vector3.zero, ForceMode.Acceleration);
		PlayerRigidBody.linearVelocity = Vector3.zero;
		PlayerRigidBody.angularVelocity = Vector3.zero;
		PlayerRigidBody.MovePosition(PlayerRigidBody.transform.position);

		//Debug.Log(Big);
		// StateMachine меняется на Idle
		if (Big == true)
		{
			SetPlayerMovementState(PlayerMovementStateType.PlayerIdle);
		}
		else
		{
			SetPlayerMovementState(PlayerMovementStateType.PlayerCrouchingIdle);
		}
	}
	public void StartPlayerLedgeClimbing()
	{
		StartCoroutine(PlayerLedgeClimbingCourutine());
	}
}
