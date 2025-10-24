using TMPro;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	private float interactionRange = 50f; // Диапазон взаимодействия
	public TextMeshProUGUI interactionText; // Подсказка (назначается вручную через Inspector)
	public PlayerCamera playerCamera;
	public GameObject PlayerCameraObject;
	private PlayerInputsList playerInputsList; // Список кнопок ввода
	private Material outlineMaterial; // Контурный материал
	private Renderer currentRenderer; // Переменная для хранения текущего Renderer
	private Material originalMaterial; // Переменная для хранения оригинального материала
	private bool IsAbleCheckForOldMaterial;

	private RaycastHit hitInfo;
	private bool isHit;

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>(); // Получаем список вводимых команд
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();
		outlineMaterial = Resources.Load<Material>("WhiteOutline"); // Загружаем контурный материал
		IsAbleCheckForOldMaterial = true;
		interactionRange = 1.5f; // Диапазон взаимодействия

	}

	/*
private void OnDrawGizmos()
{
	if (playerCamera != null)
	{
		Gizmos.color = Color.red; // Цвет луча
		Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionRange);
	}
}
	*/

void Update()
	{
		//Debug.Log("Renderer object: " + currentRenderer);

	//	Debug.Log("Original material: " + originalMaterial);


		if (interactionText != null)
			interactionText.text = "";


		//RaycastHit hitInfo;
		if (playerCamera != null)
		{
			isHit = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionRange);
		}


		if (isHit && hitInfo.collider != null && hitInfo.collider.tag == "Interactable") // Проверка по тегу и на null
		{
			var interactableObj = hitInfo.collider.GetComponent<IInteractable>();

			if (interactableObj != null) // Проверка на наличие интерфейса
			{
				Renderer renderer = hitInfo.collider.GetComponent<Renderer>();
				if (renderer != null)
				{
					currentRenderer = renderer;

					if (IsAbleCheckForOldMaterial == true)
					{ 
						originalMaterial = renderer.material;
						IsAbleCheckForOldMaterial = false;

					}

					// Сохраняем оригинальный материал в компоненте Renderer
					if (renderer.material != outlineMaterial)
					{
					
						renderer.material = outlineMaterial;
					}

					
				}

				// Устанавливаем подсказку с нужной кнопкой
				interactionText.text = $"{interactableObj.InteractionHint}\nНажмите {playerInputsList.GetNameOfKeyInteract()}";

				// Проверка на нажатие кнопки
				if (playerInputsList.GetKeyInteract())
				{
					interactableObj.Interact(); // Обработка события взаимодействия
				}

				if (interactableObj == null)
				{
					//originalMaterial = null;
					//currentRenderer = null;
					
				}
			}
			else 
			{
				Debug.LogWarning("Объект с тегом \"Interactable\" не содержит интерфейс IInteractable.");
			}

			if (interactableObj == null)
			{
				//originalMaterial = null;
				//currentRenderer = null;

			}
		}
		else 
		{
			// Если объект вышел из зоны взаимодействия, возвращаем исходный материал
			if (currentRenderer != null)
			{
			//	Debug.Log("BRUH!");

				currentRenderer.material = originalMaterial;
			
			}
				currentRenderer = null;
				originalMaterial = null;
				IsAbleCheckForOldMaterial = true;
		}

		
	}

	// Функция для смены материалов (для выделения контура)
	private void ChangeMaterialToOutline(GameObject obj)
	{
		Renderer renderer = obj.GetComponent<Renderer>();
		if (renderer != null && outlineMaterial != null)
		{
			renderer.material = outlineMaterial;
		}
		else
		{
			Debug.LogWarning("Материалов или Render-компонентов нет.");
		}
	}
}