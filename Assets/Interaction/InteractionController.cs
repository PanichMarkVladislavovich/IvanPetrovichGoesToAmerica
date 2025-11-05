using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	private float interactionRange = 50f; // Диапазон взаимодействия
	public TextMeshProUGUI interactionText; // Подсказка (назначается вручную через Inspector)
	public PlayerCameraController playerCamera;
	public GameObject PlayerCameraObject;

	public PlayerBehaviour playerBehaviour;

	private RaycastHit hitInfo;
	private bool isHit;

	private GameObject previousInteractableObject; // Переменная для хранения предыдущего объекта
	private GameObject currentInteractableObject; // Текущий объект взаимодействия
	public GameObject CurrentPickableObject { get; private set; }

	void Start()
	{
		playerCamera = PlayerCameraObject.GetComponent<PlayerCameraController>();
		playerBehaviour = GetComponent<PlayerBehaviour>();
	}

	void Update()
	{
		//Debug.Log(CurrentPickableObject);



		if (playerCamera.CurrentPlayerCameraStateType == "FirstPerson")
			interactionRange = 2.5f;
		else if (playerCamera.CurrentPlayerCameraStateType == "ThirdPerson")
			interactionRange = 2f + playerCamera.PlayerCameraDistanceZ;

		if (interactionText != null)
			interactionText.text = "";

		if (playerCamera != null)
		{
			isHit = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionRange);
		}

		// Если у нас есть захваченный объект, запрещаем любое другое взаимодействие
		if (CurrentPickableObject != null)
		{
			var pickableObj = CurrentPickableObject.GetComponent<IPickable>();
			var throwableObj = CurrentPickableObject.GetComponent<IThrowable>();
			if (pickableObj != null && pickableObj.IsObjectPickedUp)
			{
				// Сообщаем, что игрок держит объект

				if (throwableObj != null)
				{
					interactionText.text = $"Отпустить {InputManager.Instance.GetNameOfKeyInteract()}\nБросить {InputManager.Instance.GetNameOfKeyLeftHandWeaponAttack()}";
				}
				else
				{
					interactionText.text = $"Отпустить на {InputManager.Instance.GetNameOfKeyInteract()}";
				}



				// При нажатии кнопки освобождаем объект
				if (InputManager.Instance.GetKeyInteract())
				{
					pickableObj.DropOffObject();
					CurrentPickableObject = null;
					if (playerBehaviour.WasPlayerArmed == true)
					{
						playerBehaviour.ArmPlayer();
					}
				}


				if (throwableObj != null && InputManager.Instance.GetKeyLeftHandWeaponAttack())
				{
					throwableObj.ThrowObject();
					CurrentPickableObject = null;
					if (playerBehaviour.WasPlayerArmed == true)
					{
						playerBehaviour.ArmPlayer();
					}
				}

				return; // Завершаем цикл обработки, не реагируя на другие объекты
			}
		}

		// Нормальная обработка объектов
		if (isHit && hitInfo.collider != null && hitInfo.collider.tag == "Interactable")
		{
			var interactableObj = hitInfo.collider.GetComponent<IInteractable>();
			var pickableObj = hitInfo.collider.GetComponent<IPickable>();

			//Debug.Log(pickableObj != null ? pickableObj.IsObjectPickedUp.ToString() : "Нет компонента PickableObjectAbstract");

			if (interactableObj != null)
			{
				GameObject renderer = hitInfo.collider.gameObject;

				if (renderer != null)
				{
					// Подсветка текущего объекта
					currentInteractableObject = renderer;

					// Если сменился объект, меняем слои для правильного рендеринга
					if (previousInteractableObject != null && previousInteractableObject != currentInteractableObject)
					{
						previousInteractableObject.layer = LayerMask.NameToLayer("Default");
					}

					// Применяем новый слой Outline
					currentInteractableObject.layer = LayerMask.NameToLayer("Outline");
				}

				if (currentInteractableObject != null)
				{
					// Подсказка для взаимодействия
					interactionText.text = $"{interactableObj.InteractionHint}\nНажмите {InputManager.Instance.GetNameOfKeyInteract()}";
				}

				// Если это стандартный объект IInteractable, обрабатываем нажатие
				if (InputManager.Instance.GetKeyInteract())
				{
					interactableObj.Interact();
				}

				// Если это захватываемый объект, добавляем его в кэш
				if (pickableObj != null && pickableObj.IsObjectPickedUp)
				{
					CurrentPickableObject = renderer;
				}
			}
			else
			{
				Debug.LogWarning("Объект с тегом 'Interactable' не содержит интерфейс IInteractable.");
			}
		}
		else
		{
			// Очистка текущих объектов
			if (currentInteractableObject != null)
			{
				currentInteractableObject.layer = LayerMask.NameToLayer("Default");
			}

			currentInteractableObject = null;
		}

		// Помечаем текущий объект как предыдущий
		previousInteractableObject = currentInteractableObject;
	}
}