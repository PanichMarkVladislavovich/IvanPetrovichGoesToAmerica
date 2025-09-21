using System;
using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEngine.GraphicsBuffer;

public class PlayerCamera : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	public PlayerMovementController playerMovementController;
	public Vector2 MouseRotation;
	private float MouseRotationLimit = 45f;

	private RaycastHit hit;
	
	public Transform PlayerTransform;
	private string _currentPlayerCameraType;
	private string _previousPlayerCameraType;

	public CapsuleCollider PlayerCollider;

	public PlayerCameraStateType playerCameraStateType;
	public PlayerCameraState playerCameraState;

	public float PlayerCameraDistanceX;
	public float PlayerCameraDistanceY;
	public float PlayerCameraDistanceZ;

	public float CameraRotationY;

	public Vector3 CameraForward;
	public Vector3 CameraRight;

	
	public bool IsPlayerCameraFirstPerson { get; private set; }

	private bool canReturn = false;     // Возможность возврата камеры
	private float startTransitionTime; // Время начала перехода
	public float transitionDelay = 0.5f;// Задержка перед возвратом на большую дистанцию

	private float targetDistance;

		private void Awake()
	{
		playerCameraStateType = PlayerCameraStateType.ThirdPerson;
	}
	void Start()
	{
		
		

		SetPlayerCameraState(playerCameraStateType);

		playerInputsList = GetComponent<PlayerInputsList>();
		//playerMovementController = GetComponent<PlayerMovementController>();

		//PlayerCameraDistanceX = -0.4f;
		//PlayerCameraDistanceY = -1.5f;
		//PlayerCameraDistanceZ = 0.75f;


		PlayerCameraDistanceX = -0.85f;
		PlayerCameraDistanceY = -2;
		PlayerCameraDistanceZ = 2.5f;
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


		if (Physics.Linecast(PlayerCollider.transform.position, transform.position, out hit))
		{


			 targetDistance = hit.distance;
		}

		//Debug.Log(targetDistance);


		if (Physics.Linecast(PlayerCollider.transform.position, transform.position, out hit))
		{
			// Камера снова видит игрока
			if (!canReturn)
			{
				// Запускаем обратный отсчёт
				canReturn = true;
				startTransitionTime = Time.time;
			}
			else
			{
				// Проверяем, прошёл ли период ожидания
				if (Time.time - startTransitionTime >= transitionDelay)
				{
					if (PlayerCameraDistanceZ >= 0.75f)
					{
						// Потеря контакта с игроком, идём на минимальное расстояние
						PlayerCameraDistanceZ = Mathf.Lerp(PlayerCameraDistanceZ, hit.distance, Time.deltaTime * 4f);
						
					}
				}
			}
		}
		else
		{
			if (PlayerCameraDistanceZ <= 2.5f )
			{

				// Начинаем постепенное удаление камеры
				PlayerCameraDistanceZ = Mathf.Lerp(PlayerCameraDistanceZ, 2.5f, Time.deltaTime * 4f);
				
			}

			canReturn = false; // Отменяем возвращение
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
			IsPlayerCameraFirstPerson = true;
		}
		else if (playerCameraStateType == PlayerCameraStateType.ThirdPerson)
		{
			newState = new ThirdPersonPlayerCameraState(this);
			IsPlayerCameraFirstPerson = false;
		}
		else if (playerCameraStateType == PlayerCameraStateType.Cutscene)
		{
			newState = new CutscenePlayerCameraState(this);
			IsPlayerCameraFirstPerson = false;
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
