using UnityEngine;
using System.Collections;

public class Door : OpenableObjectAbstract
{
	//public override string InteractionItemName => "Дверь";

	private float doorOpeningSpeed = 200f; // Скорость открытия-закрытия

	private Coroutine currentAnimation;     // Переменная для хранения активной корутины

	private Quaternion openedRotation;       // Угловое положение открытой двери
	private Quaternion closedRotation;     // Угловое положение закрытой двери
	[SerializeField] private int doorOpenAngle;
	

	void Start()
	{
		// Настройка состояний вращения
		Vector3 openedEulerAngles = new Vector3(0, 0, doorOpenAngle);
		openedRotation = Quaternion.Euler(openedEulerAngles);

		Vector3 closedEulerAngles = new Vector3(0, 0, 0);
		closedRotation = Quaternion.Euler(closedEulerAngles);

		IsDoorOpened = false;
	}

	public override void Interact()
	{
		// Останавливаем ранее запущенную корутину, если она существует
		if (currentAnimation != null)
		{
			StopCoroutine(currentAnimation);
		}

		if (!IsDoorOpened)
		{
			currentAnimation = StartCoroutine(OpenDoor()); // Начинаем новую корутину
		}
		else
		{
			currentAnimation = StartCoroutine(CloseDoor()); // Начинаем новую корутину
		}
	}

	private void Update()
	{
		//Debug.Log(currentAnimation);
	}

	IEnumerator OpenDoor()
	{
		Debug.Log($"Была открыта {InteractionItemNameUI}");
		IsDoorOpened = true;

		while (Quaternion.Angle(transform.localRotation, openedRotation) > 0.1f)
		{
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation, openedRotation, Time.deltaTime * doorOpeningSpeed);
			yield return null;
		}

		currentAnimation = null;
	}

	IEnumerator CloseDoor()
	{
		Debug.Log($"Была закрыта {InteractionItemNameUI}");
		IsDoorOpened = false;

		while (Quaternion.Angle(transform.localRotation, closedRotation) > 0.1f)
		{
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation, closedRotation, Time.deltaTime * doorOpeningSpeed);
			yield return null;
		}

		currentAnimation = null;
	}
}