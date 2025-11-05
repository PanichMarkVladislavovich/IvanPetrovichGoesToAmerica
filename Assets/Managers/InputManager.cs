using UnityEngine;
public class InputManager : MonoBehaviour
{
	public PlayerMovementController playerMovementController;
	//public GameObject Player;

	public static InputManager Instance { get; private set; }


	private KeyCode _keyUp;
	private KeyCode _keyDown;
	private KeyCode _keyRight;
	private KeyCode _keyLeft;

	private KeyCode _keyChangeCameraView;
	private KeyCode _keyChangeCameraShoulder;

	private KeyCode _keyShowWeapons;

	private KeyCode _keyEnterCutscene;

	private KeyCode _keyRun;

	private KeyCode _keyJump;
	private KeyCode _keyCrouch;

	private KeyCode _keyLegKick;

	private KeyCode _keyInteract;

	private KeyCode _keyRightHandWeaponWheel;
	private KeyCode _keyLeftHandWeaponWheel;

	private KeyCode _keyRightHandWeaponAttack;
	private KeyCode _keyLeftHandWeaponAttack;

	private KeyCode _keyPauseMenu;

	private void Awake()
	{
		// Паттерн Singleton: предотвращаем создание второго экземпляра
		if (Instance == null)
		{
			Instance = this;

		}
		else
		{
			Destroy(gameObject); // Уничтожаем лишние экземпляры
		}
	}
	void Start()
	{
		
		//playerMovementController = GetComponent<PlayerMovementController>();
		

		_keyUp = KeyCode.W;
		_keyDown = KeyCode.S;
		_keyRight = KeyCode.D;
		_keyLeft = KeyCode.A;

		_keyChangeCameraView = KeyCode.V;
		_keyChangeCameraShoulder = KeyCode.C;

		_keyShowWeapons = KeyCode.X;

		///////////////////
		_keyEnterCutscene = KeyCode.Z;

		_keyRun = KeyCode.LeftShift;

		_keyJump = KeyCode.Space;
		_keyCrouch = KeyCode.LeftControl;

		_keyLegKick = KeyCode.Mouse2;


		_keyInteract = KeyCode.F;


		_keyRightHandWeaponWheel = KeyCode.E;
		_keyLeftHandWeaponWheel = KeyCode.Q;

		_keyRightHandWeaponAttack = KeyCode.Mouse1;
		_keyLeftHandWeaponAttack = KeyCode.Mouse0;

		_keyPauseMenu = KeyCode.Alpha1; //for now its not ESC as Unity and stuff...
	}

	
	public bool GetKeyUp()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyUp) && Input.GetKey(_keyDown))
		{
			return false;
		}
		else if (MenuManager.IsPlayerControllable && Input.GetKey(_keyUp) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyDown()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyUp) && Input.GetKey(_keyDown))
		{
			return false;
		}
		else if (MenuManager.IsPlayerControllable && Input.GetKey(_keyDown) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyRight()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyRight) && Input.GetKey(_keyLeft))
		{
			return false;
		}
		else if (MenuManager.IsPlayerControllable && Input.GetKey(_keyRight) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyLeft()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyRight) && Input.GetKey(_keyLeft))
		{
			return false;
		}
		else if (MenuManager.IsPlayerControllable && Input.GetKey(_keyLeft) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyChangeCameraView()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyChangeCameraView))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyChangeCameraShoulder()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyChangeCameraShoulder))
		{
			return true;
		}
		else return false;
	}

	////////////////////////
	//////////////////////
	public bool GetKeyEnterCutscene()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyEnterCutscene) && false)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyShowWeapons()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyShowWeapons) && !MenuManager.IsWeaponWheelMenuOpened)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyRun()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyRun) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyJump()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyJump) && playerMovementController.IsPlayerGrounded == true && playerMovementController.IsPlayerAbleToMove == true && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyJumpBeingHeld()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyJump))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyCrouch()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyCrouch))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyLegKick()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyLegKick))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyInteract()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyInteract))
		{
			return true;
		}
		else return false;
	}

	public string GetNameOfKeyInteract()
	{
		return _keyInteract.ToString();
	}

	public bool GetKeyRightHandWeaponWheel()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyRightHandWeaponWheel))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyLeftHandWeaponWheel()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKey(_keyLeftHandWeaponWheel))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyRightHandWeaponAttack()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyRightHandWeaponAttack))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyLeftHandWeaponAttack()
	{
		if (MenuManager.IsPlayerControllable && Input.GetKeyDown(_keyLeftHandWeaponAttack))
		{
			return true;
		}
		else return false;
	}

	public string GetNameOfKeyLeftHandWeaponAttack()
	{
		return _keyLeftHandWeaponAttack.ToString();
	}

	public bool GetKeyPauseMenu()
	{
		if (Input.GetKeyDown(_keyPauseMenu))
		{
			return true;
		}
		else return false;
	}
}
