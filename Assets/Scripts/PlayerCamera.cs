using UnityEngine;
public class PlayerCamera : MonoBehaviour, IDataPersistence
{
	PlayerInputsList playerInputsList;
	public PlayerMovementController playerMovementController;
	public CapsuleCollider PlayerCollider;
	public Transform PlayerTransform;

	public PlayerCameraState playerCameraState;
	public PlayerCameraStateType playerCameraStateType;

	public Vector2 MouseRotation;
	public Vector2 MouseScrollWheel;

	public Vector3 CameraForward;
	public Vector3 CameraRight;

	private RaycastHit hit;

	public bool IsAbleToZoomCameraOut = true;

	public float PlayerCameraDistanceX;
	public float PlayerCameraDistanceY;
	public float PlayerCameraDistanceZ;

	public float CameraRotationY;
	private float MouseRotationLimit = 65f;
	public bool IsPlayerCameraFirstPerson { get; private set; }
	private string _currentPlayerCameraType;
	private string _previousPlayerCameraType;

	private bool IsCameraShoulderRight = true;

	private bool canReturn = false;     
	private float startTransitionTime; 
	public float transitionDelay = 0.5f;

	private void Awake()
	{
		playerCameraStateType = PlayerCameraStateType.ThirdPerson;
	}
	void Start()
	{
		SetPlayerCameraState(playerCameraStateType);

		playerInputsList = GetComponent<PlayerInputsList>();

		PlayerCameraDistanceX = -0.85f;

		PlayerCameraDistanceY = -1.75f;
		PlayerCameraDistanceZ = 3.25f;

		// DO NOT DELETE
		// MAX AND MIN CONSTS
		//PlayerCameraDistanceY = -2;
		//PlayerCameraDistanceZ = 5;
		//
		//PlayerCameraDistanceY = -1.5f;
		//PlayerCameraDistanceZ = 1.5f;
	}

	void Update()
	{
		//Debug.Log(IsCameraShoulderRight);

        if (!MenuManager.IsAnyMenuOpened)
        {
			MouseRotation.y += Input.GetAxis("Mouse X");
			MouseRotation.x += Input.GetAxis("Mouse Y");
			MouseRotation.x = Mathf.Clamp(MouseRotation.x, MouseRotationLimit * -1, MouseRotationLimit);
			MouseScrollWheel = Input.mouseScrollDelta;
        }

		if (MouseScrollWheel.y < 0 && IsAbleToZoomCameraOut == true && IsPlayerCameraFirstPerson == false)
		{
			if (PlayerCameraDistanceY < -2)
			{
				PlayerCameraDistanceY += -0.05f;
			}
			if (PlayerCameraDistanceZ < 5)
			{
				PlayerCameraDistanceZ += 0.35f;
			}
		}
		if (MouseScrollWheel.y > 0 && IsPlayerCameraFirstPerson == false)
		{
			if (PlayerCameraDistanceY > -1.5f)
			{
				PlayerCameraDistanceY += 0.05f;
			}
			if (PlayerCameraDistanceZ > 1.5f)
			{
				PlayerCameraDistanceZ += -0.35f;
			}
		}

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

		if (playerInputsList.GetKeyChangeCameraShoulder() && IsPlayerCameraFirstPerson == false)
		{
			IsCameraShoulderRight = !IsCameraShoulderRight;
		}

		if (IsCameraShoulderRight == true)
		{
			PlayerCameraDistanceX = Mathf.Lerp(PlayerCameraDistanceX, -0.85f, Time.deltaTime * 4);
		}
		else
		{
			PlayerCameraDistanceX = Mathf.Lerp(PlayerCameraDistanceX, 0.85f, Time.deltaTime * 4);
		}
		
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
						IsAbleToZoomCameraOut = false;
					}
					//else 
				}
			}
		}
		else
		{
			if (PlayerCameraDistanceZ <= 5f )
			{
					IsAbleToZoomCameraOut = true;
				// Начинаем постепенное удаление камеры
			//	PlayerCameraDistanceZ = Mathf.Lerp(PlayerCameraDistanceZ, 5f, Time.deltaTime * 4f);
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
		new Vector3(0, playerMovementController.PlayerCurrentHeight -0.13f, 0.1f);
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
		_previousPlayerCameraType = _currentPlayerCameraType;
        _currentPlayerCameraType = newCameraType.ToString();
	}
	public string GetCurrentPlayerCameraType()
	{
		return _currentPlayerCameraType.ToString();
	}
	public string GetPreviousPlayerCameraType()
	{
		return _previousPlayerCameraType.ToString();
	}

	

	public void SaveData(ref GameData data)
	{
		data.IsCameraShoulderRight = this.IsCameraShoulderRight;
	}

	public void LoadData(GameData data)
	{
		this.IsCameraShoulderRight = data.IsCameraShoulderRight;
	}
}
