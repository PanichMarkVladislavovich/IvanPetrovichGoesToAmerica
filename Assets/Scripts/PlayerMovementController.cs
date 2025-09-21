
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Collections;
using System.Threading.Tasks;	

using Unity.Mathematics;


public class PlayerMovementController : MonoBehaviour
{
	public PlayerInputsList playerInputsList;
	PlayerBehaviour playerBehaviour;
	PlayerCamera playerCamera;
	private Animator playerAnimator;
	private string currentPlayerAnimation = "";
	public float PlayerCurrentHeight { get; private set; }
	public float PlayerCrouchingHeight { get; private set; }
	public float PlayerStandingHeight { get; private set; }

	public GameObject PlayerCameraObject;
	public Transform PlayerTransform;
	public Rigidbody PlayerRigidBody;

	public Vector3 PlayerWorldMovement;

	public CapsuleCollider playerCapsuleCollider;
	public Transform playerCapsuleColliderMesh;
	//public CapsuleCollider PlayerColliderCapsuleObject;
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
		//PlayerTransform = GetComponent<Transform>();
		
		
		//playerCapsuleCollider.center = new Vector3(0, 1, 0);

		playerAnimator = GetComponent<Animator>();

		IsPlayerAbleToSlide = true;

		//PlayerTransform

		//PlayerCrouchingHeight = 0.5f;
		//PlayerStandingHeight = 1;

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
		PlayerWalkingSpeed = 3f;
		PlayerRunningSpeed = 6f;
		PlayerCrouchingSpeed = 1.8f;

		///////////////
		PlayerSlidingSpeed = 7.5f;
		///////////////
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
		//playerCapsuleCollider.center = new Vector3(0, 1, 0);
		//Debug.Log(PlayerColliderCapsuleObject.height);
		//Debug.Log(_playerMovementChange);
		//Debug.Log(IsPlayerGrounded);
		//Debug.Log(IsPlayerFalling);

		//playerCapsuleCollider.center = new Vector3(playerCapsuleCollider.center.x, transform.position.y, playerCapsuleCollider.center.z);

		//UnityEngine.Debug.Log(IsPlayerAbleToMove);


		//PlayerRigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);

		// transform.localScale = new Vector3(transform.localScale.x, PlayerCurrentHeight, transform.localScale.z);

		//Debug.Log(IsPlayerCrouching);

		IsPlayerMoving = (Mathf.Abs(_playerPreviousFramePositionChange.x) > 0.001f || Mathf.Abs(_playerPreviousFramePositionChange.z) > 0.001f);

		
		
		RaycastHit hitInfo;

		//PlayerCurrentHeight = PlayerTransform.position.y;

		if (IsPlayerCrouching == false)
		{
			PlayerDownRayYPosition = 0.1f;
			PlayerUpRayYPosition = 1.9f;

			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);
			//PlayerColliderCapsuleObject.height = 2;
			//playerCapsuleCollider.center = new Vector3(playerCapsuleCollider.center.x, transform.position.y - 0.5f, playerCapsuleCollider.center.z);

			//playerCapsuleCollider.height = 2;

			//playerCapsuleCollider.center = new Vector3(playerCapsuleCollider.center.x, playerCapsuleCollider.center.y, playerCapsuleCollider.center.z);

			//playerCapsuleCollider.center = new Vector3(playerCapsuleCollider.center.x, transform.position.y, playerCapsuleCollider.center.z);
			
			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);
			//PlayerColliderCapsuleObject.height = Mathf.Lerp(PlayerCurrentHeight, PlayerStandingHeight, Time.deltaTime * 15f);
		}
		else if (IsPlayerCrouching == true)
		{
			PlayerDownRayYPosition = 0.1f;
			PlayerUpRayYPosition = 0.9f;


			
			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);
			//PlayerColliderCapsuleObject.height = 1;

			//playerCapsuleCollider.height = transform.position.y + (0.5f);

			//playerCapsuleCollider.height = 1;

			//playerCapsuleCollider.center = new Vector3(playerCapsuleCollider.center.x, transform.position.y - 0.5f, playerCapsuleCollider.center.z);

			//PlayerCurrentHeight = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);
			//PlayerColliderCapsuleObject.height = Mathf.Lerp(PlayerCurrentHeight, PlayerCrouchingHeight, Time.deltaTime * 15f);

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

		/////////////////////////////
		/////////////////////////////
		//Debug.Log(IsPlayerAbleToSlide);

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
