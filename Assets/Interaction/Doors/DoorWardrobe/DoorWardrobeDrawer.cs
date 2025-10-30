using UnityEngine;
using System.Collections;

public class DoorWardrobeDrawer : DoorAbstract
{
	public override string InteractionItemName => "Выдвижной ящик";

	private float drawerOpeningSpeed = 3f; // Скорость открытия-закрытия ящика

	private Coroutine currentAnimation;     // Переменная для хранения активной корутины

	private Vector3 openedPosition;        // Открытое положение ящика
	private Vector3 closedPosition;        // Закрытое положение ящика

	void Start()
	{
		// Начальное положение закрылого ящика
		closedPosition = transform.localPosition;

		// Открываем ящик вперёд по оси Z на 0.45 единицы
		openedPosition = transform.localPosition + new Vector3(0, 0, 0.45f);

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
			currentAnimation = StartCoroutine(OpenDrawer()); // Начинаем новую корутину
		}
		else
		{
			currentAnimation = StartCoroutine(CloseDrawer()); // Начинаем новую корутину
		}
	}

	IEnumerator OpenDrawer()
	{
		Debug.Log($"Был открыт {InteractionItemName}");
		IsDoorOpened = true;

		while (Mathf.Abs(transform.localPosition.z - openedPosition.z) > 0.001f)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, openedPosition, Time.deltaTime * drawerOpeningSpeed);
			yield return null;
		}

		currentAnimation = null;
	}

	IEnumerator CloseDrawer()
	{
		Debug.Log($"Был закрыт {InteractionItemName}");
		IsDoorOpened = false;

		while (Mathf.Abs(transform.localPosition.z - closedPosition.z) > 0.001f)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, closedPosition, Time.deltaTime * drawerOpeningSpeed);
			yield return null;
		}

		currentAnimation = null;
	}
}