
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
	PlayerInputsList playerInputsList;
	public Canvas canvas; // Привяжите ваш Canvas сюда
	public bool IsWeaponLeftHand {  get; private set; }
	public bool IsWeaponWheelActive { get; private set; }

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();
	}

	void Update()
	{
		if (playerInputsList.GetKeyLeftHandWeaponWheel() == true)
		{
			EnableCanvas();
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = true;
		}
		else if (playerInputsList.GetKeyRightHandWeaponWheel() == true)
		{
			EnableCanvas();
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			IsWeaponWheelActive = true;
			IsWeaponLeftHand = false;
		}
		else
		{
			DisableCanvas();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			IsWeaponWheelActive = false;
		}
	}

	private void EnableCanvas()
	{
		canvas.gameObject.SetActive(true); // Делаем Canvas видимым
	}

	private void DisableCanvas()
	{
		canvas.gameObject.SetActive(false); // Делаем Canvas невидимым
	}
}