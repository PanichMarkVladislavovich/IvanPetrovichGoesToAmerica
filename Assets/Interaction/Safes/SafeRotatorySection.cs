using UnityEngine;
using System.Collections;

public class SafeRotatorySection : MonoBehaviour, IInteractable
{
	[SerializeField] private int safeRotatorySectionName;
	public string InteractionItemName => safeRotatorySectionName.ToString();


	[SerializeField] [Range(0, 9)] private int correctSafeRotatorySectionPosition;

	private int currentSafeRotatorySectionPosition;

	private float rotatorySafeSectionRotationSpeed = 0.15f;
	private Coroutine safeRotatorySectionCoroutine; // Переменная для хранения текущей корутины

	public virtual string InteractionHint => $"Повернуть ячейку #{InteractionItemName}";

	public void Interact()
	{
		// Проверяем, есть ли уже запущенная корутина
		if (safeRotatorySectionCoroutine == null)
		{
			// Можно либо остановить текущую корутину и начать новую, либо проигнорировать вызов
			//StopCoroutine(rotateRoutine); // Останавливаем текущую корутину
			safeRotatorySectionCoroutine = StartCoroutine(RotateSmoothly(rotatorySafeSectionRotationSpeed));

		}
		else
		{
			// Запускаем новую корутину
		}
	}

	IEnumerator RotateSmoothly(float duration)
	{
		//Debug.Log($"Rotating safe section #{InteractionItemName}");

		if (currentSafeRotatorySectionPosition < 9)
		{
			currentSafeRotatorySectionPosition++;
		}
		else
		{
			currentSafeRotatorySectionPosition -= 9;
		}
		

		Quaternion rotateFrom = transform.localRotation;
		Quaternion rotateTo = transform.localRotation * Quaternion.Euler(0, 36, 0);

		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			transform.localRotation = Quaternion.Slerp(rotateFrom, rotateTo, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		Debug.Log($"Section #{InteractionItemName} new position is {currentSafeRotatorySectionPosition}");


		transform.localRotation = rotateTo; // Гарантированно устанавливаем финальное положение
		safeRotatorySectionCoroutine = null; // Освобождаем переменную после завершения корутины
	}


}
