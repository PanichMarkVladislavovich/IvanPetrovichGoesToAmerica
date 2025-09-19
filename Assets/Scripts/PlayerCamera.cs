using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerCamera : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	public PlayerMovementController playerMovementController;
	public Vector2 MouseRotation;
	private float MouseRotationLimit = 45f;


	
	public Transform PlayerTransform;
	private string _currentPlayerCameraType;
	private string _previousPlayerCameraType;

	public PlayerCameraStateType playerCameraStateType;
	public PlayerCameraState playerCameraState;

	public float PlayerCameraDistanceX;
	public float PlayerCameraDistanceY;
	public float PlayerCameraDistanceZ;

	public float CameraRotationY;

	public Vector3 CameraForward;
	public Vector3 CameraRight;

	private void Awake()
	{
		playerCameraStateType = PlayerCameraStateType.ThirdPerson;
	}
	void Start()
	{
		SetPlayerCameraState(playerCameraStateType);

		playerInputsList = GetComponent<PlayerInputsList>();
		//playerMovementController = GetComponent<PlayerMovementController>();

		PlayerCameraDistanceX = -0.85f;
		PlayerCameraDistanceY = -1;
		PlayerCameraDistanceZ = 1.8f;


		
	}

	void Update()
	{
		
		
		
		
		MouseRotation.y += Input.GetAxis("Mouse X");
		MouseRotation.x += Input.GetAxis("Mouse Y");
		MouseRotation.x = Mathf.Clamp(MouseRotation.x, MouseRotationLimit * -1, MouseRotationLimit);

		playerCameraState.PlayerCameraPosition();

		if (playerInputsList.GetKeyChangeCameraView())
		{
			ChangePlayerCameraView();
		}

		if (playerInputsList.GetKeyEnterCutscene())
		{
			if (_currentPlayerCameraType != PlayerCameraStateType.Cutscene.ToString())
			{
				EnterCutscene();
			}
			else
			{
				ExitCutscene();
			}
		}

		
		CameraForward = transform.forward;
		CameraRight = transform.right;

		transform.rotation = Quaternion.Euler(-MouseRotation.x, MouseRotation.y, 0);

		CameraRotationY = transform.eulerAngles.y;
	}
	public void SetPlayerCameraState(PlayerCameraStateType playerCameraStateType)
	{
		PlayerCameraState newState;

		if (playerCameraStateType == PlayerCameraStateType.FirstPerson)
		{
			newState = new FirstPersonPlayerCameraState(this);
		}
		else if (playerCameraStateType == PlayerCameraStateType.ThirdPerson)
		{
			newState = new ThirdPersonPlayerCameraState(this);
		}
		else if (playerCameraStateType == PlayerCameraStateType.Cutscene)
		{
			newState = new CutscenePlayerCameraState(this);
		}
		else
		{
			newState = null;
		}

		playerCameraState = newState;
	}
	public void ChangePlayerCameraView()
	{
		playerCameraState.ChangePlayerCameraView();
	}
	public void EnterCutscene()
	{
		playerCameraState.EnterCutscene();
	}
	public void ExitCutscene()
	{
		playerCameraState.ExitCutscene();
	}
	public void FirstPersonCameraTransform()
	{
		transform.position = PlayerTransform.position + Quaternion.Euler(0, MouseRotation.y, 0) *
		new Vector3(0, playerMovementController.PlayerCurrentHeight, 0.05f);
	}
	public void ThirdPersonCameraTransform()
	{
		transform.position = PlayerTransform.position - Quaternion.Euler(-MouseRotation.x, MouseRotation.y, 0) *
		new Vector3(PlayerCameraDistanceX, PlayerCameraDistanceY, PlayerCameraDistanceZ);
	}
	public void CutsceneCameraTransform()
	{
		transform.position = new Vector3(0, 5, -7);
	}
	public void SetPlayerCameraType(PlayerCameraStateType newCameraType)
	{
		/*
		if (newCameraType < 0 || newCameraType > 3)
		{
			Debug.Log("Wrong PlayerCameraType set: " + newCameraType);
			return;
		}
		*/
		_previousPlayerCameraType = _currentPlayerCameraType;
        _currentPlayerCameraType = newCameraType.ToString();
		Debug.Log("Previous Cam: " + _previousPlayerCameraType);
		Debug.Log("Current Cam: " + _currentPlayerCameraType);
		
	}
	public string GetCurrentPlayerCameraType()
	{
		return _currentPlayerCameraType.ToString();
	}
	public string GetPreviousPlayerCameraType()
	{
		return _previousPlayerCameraType.ToString();
	}
}
