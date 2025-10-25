using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	private float interactionRange = 50f; // Диапазон взаимодействия
	public TextMeshProUGUI interactionText; // Подсказка (назначается вручную через Inspector)
	public PlayerCamera playerCamera;
	public GameObject PlayerCameraObject;
	private PlayerInputsList playerInputsList; // Список кнопок ввода
	private GameObject previousInteractableItem; // Переменная для хранения предыдущего объекта
	private GameObject currentInteractableItem; // Текущий объект взаимодействия

	private RaycastHit hitInfo;
	private bool isHit;

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>(); // Получаем список вводимых команд
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();
	}

	void Update()
	{
		if (playerCamera.CurrentPlayerCameraStateType == "FirstPerson")
		{
			interactionRange = 2.5f;
		}
		if (playerCamera.CurrentPlayerCameraStateType == "ThirdPerson")
		{
			interactionRange = 2f + playerCamera.PlayerCameraDistanceZ;
		}

		if (interactionText != null)
			interactionText.text = "";

		if (playerCamera != null)
		{
			isHit = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionRange);
		}

		if (isHit && hitInfo.collider != null && hitInfo.collider.tag == "Interactable")
		{
			var interactableObj = hitInfo.collider.GetComponent<IInteractable>();

			if (interactableObj != null)
			{
				GameObject renderer = hitInfo.collider.gameObject;

				if (renderer != null)
				{
					currentInteractableItem = renderer;

					// Новый объект найден, проверяем, изменился ли он
					if (previousInteractableItem != null && previousInteractableItem != currentInteractableItem)
					{
						// Предыдущий объект меняется на новый, сбрасываем слой предыдущего
						previousInteractableItem.layer = LayerMask.NameToLayer("Default");
					}

					// Ставим новый слой для текущего объекта
					currentInteractableItem.layer = LayerMask.NameToLayer("Outline");
				}

				// Устанавливаем подсказку с нужной кнопкой
				interactionText.text = $"{interactableObj.InteractionHint}\nНажмите {playerInputsList.GetNameOfKeyInteract()}";

				// Проверка на нажатие кнопки
				if (playerInputsList.GetKeyInteract())
				{
					interactableObj.Interact(); // Обработка события взаимодействия
				}
			}
			else
			{
				Debug.LogWarning("Объект с тегом \"Interactable\" не содержит интерфейс IInteractable.");
			}
		}
		else
		{
			// Если объект вышел из зоны взаимодействия
			if (currentInteractableItem != null)
			{
				currentInteractableItem.layer = LayerMask.NameToLayer("Default");
			}

			// Очищаем текущий объект
			currentInteractableItem = null;
		}

		// Запоминаем текущий объект как предыдущий для следующего кадра
		previousInteractableItem = currentInteractableItem;
	}
}