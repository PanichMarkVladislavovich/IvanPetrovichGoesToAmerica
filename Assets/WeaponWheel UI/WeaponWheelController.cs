using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    PlayerInputsList playerInputsList;
	public Canvas canvas; // Привяжите ваш Canvas сюда
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
		}
		else
		{
			DisableCanvas();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}


	private void EnableCanvas()
	{
		// Включаем взаимодействие с Canvas
		//canvas.interactable = true;
	//	canvas.blocksRaycasts = true;
		canvas.gameObject.SetActive(true); // Делаем Canvas видимым
	}

	private void DisableCanvas()
	{
		// Отключаем взаимодействие с Canvas
		//canvas.interactable = false;
		//canvas.blocksRaycasts = false;
		canvas.gameObject.SetActive(false); // Делаем Canvas невидимым
	}
}
