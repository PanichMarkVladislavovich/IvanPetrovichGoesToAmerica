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
	}
	public bool GetKeyUp()
	{
		if (Input.GetKey(_keyUp) && Input.GetKey(_keyDown))
		{
			return false;
		}
		else if (Input.GetKey(_keyUp) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyDown()
	{
		if (Input.GetKey(_keyUp) && Input.GetKey(_keyDown))
		{
			return false;
		}
		else if (Input.GetKey(_keyDown) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyRight()
	{
		if (Input.GetKey(_keyRight) && Input.GetKey(_keyLeft))
		{
			return false;
		}
		else if (Input.GetKey(_keyRight) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyLeft()
	{
		if (Input.GetKey(_keyRight) && Input.GetKey(_keyLeft))
		{
			return false;
		}
		else if (Input.GetKey(_keyLeft) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyChangeCameraView()
	{
		if (Input.GetKeyDown(_keyChangeCameraView))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyEnterCutscene()
	{
		if (Input.GetKeyDown(_keyEnterCutscene))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyShowWeapons()
	{
		if (Input.GetKeyDown(_keyShowWeapons))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyRun()
	{
		if (Input.GetKey(_keyRun) && playerMovementController.IsPlayerAbleToMove == true)
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyJump()
	{
		if (Input.GetKeyDown(_keyJump) && playerMovementController.IsPlayerGrounded == true && playerMovementController.IsPlayerAbleToMove == true && playerMovementController.IsPlayerAbleToStandUp == true)
		{
			return true;
		}
		else return false;
	}
	public bool IsKeyJumpBeingHeld()
	{
		if (Input.GetKey(_keyJump))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyCrouch()
	{
		if (Input.GetKeyDown(_keyCrouch))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyRightHandWeaponWheel()
	{
		if (Input.GetKey(_keyRightHandWeaponWheel))
		{
			return true;
		}
		else return false;
	}
	public bool GetKeyLeftHandWeaponWheel()
	{
		if (Input.GetKey(_keyLeftHandWeaponWheel))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyRightHandWeaponAttack()
	{
		if (Input.GetKeyDown(_keyRightHandWeaponAttack))
		{
			return true;
		}
		else return false;
	}

	public bool GetKeyLeftHandWeaponAttack()
	{
		if (Input.GetKeyDown(_keyLeftHandWeaponAttack))
		{
			return true;
		}
		else return false;
	}
}
