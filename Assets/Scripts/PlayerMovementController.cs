using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Collections;
using System.Threading.Tasks;	
using System.Diagnostics;
using Unity.Mathematics;


public class PlayerMovementController : MonoBehaviour
{
	public PlayerInputsList playerInputsList;
	PlayerBehaviour playerBehaviour;
	PlayerCamera playerCamera;

	public float PlayerCurrentHeight { get; private set; }
	public float PlayerCrouchingHeight { get; private set; }
	public float PlayerStandingHeight { get; private set; }

	public GameObject PlayerCameraObject;
	public Transform PlayerTransform;
	public Rigidbody PlayerRigidBody;

	public Vector3 PlayerWorldMovement;

	public float PlayerRotationSpeed { get; private set; }
	public float PlayerDownRayYPosition { get; private set; }
	public float PlayerUpRayYPosition { get; private set; }

	private Vector3 _playerPreviousFramePositionChange;
	private Vector3 _playerPreviousFramePosition;

	public bool IsPlayerMoving { get; private set; }
	public bool IsPlayerAbleToMove { get; private set; }
	public bool IsPlayerGrounded { get; private set; }
	public bool IsPlayerAbleToStandUp { get; private set; }
	public bool IsPlayerFalling { get; private set; }
	public bool IsPlayerCrouching { get; private set; }

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
		
		PlayerCrouchingHeight = 0.5f;
		PlayerStandingHeight = 1;


	SetPlayerMovementState(playerMovementStateType);


		_playerPreviousFramePosition = transform.position;

		playerInputsList = GetComponent<PlayerInputsList>();
		playerBehaviour = GetComponent<PlayerBehaviour>();
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();

		PlayerMovementSpeed = 3f;
		PlayerWalkingSpeed = 3f;
		PlayerRunningSpeed = 6f;
		PlayerCrouchingSpeed = 1.8f;
		PlayerSlidingSpeed = 10;

	}


	void OnDrawGizmosSelected()
	{
		// Визуализируем луч в редакторе Unity
		Gizmos.color = Color.red;

		Gizmos.DrawRay(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down * 0.2f);
		Gizmos.DrawRay(transform.position + new Vector3(0, PlayerUpRayYPosition, 0), Vector3.up * 0.3f);
	}
	void Update()
	{
		//Debug.Log(_playerMovementChange);
		//Debug.Log(IsPlayerGrounded);
		//Debug.Log(IsPlayerFalling);


		//UnityEngine.Debug.Log(IsPlayerAbleToMove);

		
		//PlayerRigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
		 transform.localScale = new Vector3(transform.localScale.x, PlayerCurrentHeight, transform.localScale.z);

		//Debug.Log(IsPlayerCrouching);

		IsPlayerMoving = (Mathf.Abs(_playerPreviousFramePositionChange.x) > 0.001f || Mathf.Abs(_playerPreviousFramePositionChange.z) > 0.001f);

		
		RaycastHit hitInfo;

		if (IsPlayerCrouching == false)
		{
			PlayerDownRayYPosition = -0.9f;
			PlayerUpRayYPosition = 0.9f;
			
			PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);
		}
		else if (IsPlayerCrouching == true)
		{
			PlayerDownRayYPosition = -0.4f;
			PlayerUpRayYPosition = 0.4f;
			PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);
			
		}
		//UnityEngine.Debug.Log(PlayerCurrentHeight);
		IsPlayerGrounded = Physics.Raycast(transform.position + new Vector3(0, PlayerDownRayYPosition, 0), Vector3.down, out hitInfo, 0.2f);

		IsPlayerAbleToStandUp = !Physics.Raycast(transform.position + new Vector3(0, PlayerUpRayYPosition, 0), Vector3.up, out hitInfo, 0.3f);
		//Debug.Log(IsPlayerAbleToStandUp);

		IsPlayerFalling = (_playerPreviousFramePositionChange.y < -0.01f && IsPlayerGrounded == false);


		

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

		Vector3 PlayerMovementDirectionWithCamera = (PlayerWorldMovement.z * playerCamera.CameraForward + PlayerWorldMovement.x * playerCamera.CameraRight);

		Vector3 PlayerMovement = new Vector3(PlayerMovementDirectionWithCamera.x, 0, PlayerMovementDirectionWithCamera.z);
		PlayerMovement.Normalize();

		PlayerRigidBody.MovePosition(PlayerRigidBody.position + PlayerMovement * PlayerMovementSpeed * Time.deltaTime);


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
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerWalking)
		{
			newState = new WalkingPlayerMovementState(this);
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerRunning)
		{
			newState = new RunningPlayerMovementState(this);
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerJumping)
		{
			newState = new JumpingPlayerMovementState(this);
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerFalling)
		{
			newState = new FallingPlayerMovementState(this);
			IsPlayerCrouching = false;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerCrouchingIdle)
		{
			newState = new CrouchingIdlePlayerMovementState(this);
			IsPlayerCrouching = true;
			IsPlayerAbleToMove = true;
			PlayerRotationSpeed = 300f;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerCrouchingWalking)
		{
			newState = new CrouchingWalkingPlayerMovementState(this);
			IsPlayerCrouching = true;
		}
		else if (playerMovementStateType == PlayerMovementStateType.PlayerSliding)
		{
			newState = new SlidingPlayerMovementState(this);
			IsPlayerCrouching = true;
			IsPlayerAbleToMove = false;
			PlayerRotationSpeed = 0;
			newState.ChangePlayerMovementDelayed();
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
}
