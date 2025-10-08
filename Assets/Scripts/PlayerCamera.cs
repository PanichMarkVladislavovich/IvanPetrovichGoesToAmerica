using UnityEngine;
public class PlayerCamera : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	public PlayerMovementController playerMovementController;
	public CapsuleCollider PlayerCollider;
	public Transform PlayerTransform;

	public PlayerCameraState playerCameraState;
	public PlayerCameraStateType playerCameraStateType;

	public Vector2 MouseRotation;

	public Vector3 CameraForward;
	public Vector3 CameraRight;

	private RaycastHit hit;

	public float PlayerCameraDistanceX;
	public float PlayerCameraDistanceY;
	public float PlayerCameraDistanceZ;

	public float CameraRotationY;
	private float MouseRotationLimit = 45f;
	public bool IsPlayerCameraFirstPerson { get; private set; }
	private string _currentPlayerCameraType;
	private string _previousPlayerCameraType;

	private bool canReturn = false;     
	private float startTransitionTime; 
	public float transitionDelay = 0.5f;

	private float targetDistance;

	private void Awake()
	{
		playerCameraStateType = PlayerCameraStateType.ThirdPerson;
	}
	void Start()
	{
		SetPlayerCameraState(playerCameraStateType);

		playerInputsList = GetComponent<PlayerInputsList>();

		PlayerCameraDistanceX = -0.85f;
		PlayerCameraDistanceY = -2;
		PlayerCameraDistanceZ = 5f;
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
			if (PlayerCameraDistanceZ <= 5f )
			{
				// Начинаем постепенное удаление камеры
				PlayerCameraDistanceZ = Mathf.Lerp(PlayerCameraDistanceZ, 5f, Time.deltaTime * 4f);
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
}
