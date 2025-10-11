using UnityEngine;
public class PlayerInputsList : MonoBehaviour
{
	PlayerMovementController playerMovementController;
	
	
	private KeyCode _keyUp;
	private KeyCode _keyDown;
	private KeyCode _keyRight;
	private KeyCode _keyLeft;

	private KeyCode _keyChangeCameraView;

	private KeyCode _keyShowWeapons;

	private KeyCode _keyEnterCutscene;

	private KeyCode _keyRun;

	private KeyCode _keyJump;
	private KeyCode _keyCrouch;

	private KeyCode _keyRightHandWeaponWheel;
	private KeyCode _keyLeftHandWeaponWheel;

	private KeyCode _keyRightHandWeaponAttack;
	private KeyCode _keyLeftHandWeaponAttack;

	private KeyCode _keyOpenMainMenu; 
	void Start()
	{
		playerMovementController = GetComponent<PlayerMovementController>();
		

		_keyUp = KeyCode.W;
		_keyDown = KeyCode.S;
		_keyRight = KeyCode.D;
		_keyLeft = KeyCode.A;

		_keyChangeCameraView = KeyCode.V;

		_keyShowWeapons = KeyCode.F;

		_keyEnterCutscene = KeyCode.C;

		_keyRun = KeyCode.LeftShift;

		_keyJump = KeyCode.Space;
		_keyCrouch = KeyCode.LeftControl;

		_keyRightHandWeaponWheel = KeyCode.Q;
		_keyLeftHandWeaponWheel = KeyCode.Tab;

		_keyRightHandWeaponAttack = KeyCode.Mouse1;
		_keyLeftHandWeaponAttack = KeyCode.Mouse0;

		_keyOpenMainMenu = KeyCode.Alpha1; //for now its not ESC as Unity and stuff...
	}

	
	public bool GetKeyUp()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyUp) && Input.GetKey(_keyDown))
		{
			return false;
		}
		else if (GameManager.IsPlayerControllable && Input.GetKey(_keyUp) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyDown()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyUp) && Input.GetKey(_keyDown))
		{
			return false;
		}
		else if (GameManager.IsPlayerControllable && Input.GetKey(_keyDown) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyRight()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyRight) && Input.GetKey(_keyLeft))
		{
			return false;
		}
		else if (GameManager.IsPlayerControllable && Input.GetKey(_keyRight) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyLeft()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyRight) && Input.GetKey(_keyLeft))
		{
			return false;
		}
		else if (GameManager.IsPlayerControllable && Input.GetKey(_keyLeft) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyChangeCameraView()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyChangeCameraView))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyEnterCutscene()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyEnterCutscene))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyShowWeapons()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyShowWeapons) && !GameManager.IsWeaponWheelMenuOpened)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyRun()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyRun) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyJump()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyJump) && playerMovementController.IsPlayerGrounded == true && playerMovementController.IsPlayerAbleToMove == true && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyJumpBeingHeld()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyJump))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyCrouch()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyCrouch))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyRightHandWeaponWheel()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyRightHandWeaponWheel))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyLeftHandWeaponWheel()
	{
		if (GameManager.IsPlayerControllable && Input.GetKey(_keyLeftHandWeaponWheel))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyRightHandWeaponAttack()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyRightHandWeaponAttack))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyLeftHandWeaponAttack()
	{
		if (GameManager.IsPlayerControllable && Input.GetKeyDown(_keyLeftHandWeaponAttack))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyOpenMainMenu()
	{
		if (Input.GetKeyDown(_keyOpenMainMenu))
		{
			return true;
		}
		else return false;
	}
}
