using UnityEngine;
using UnityEngine.UI;

public class LoadSubMenuController : MonoBehaviour
{
	PauseMenuController pauseMenuController;

	public Canvas LoadSubMenuCanvas;

	public Button CloseLoadSubMenuButton;

	public Button LoadGame1Button;
	public Button LoadGame2Button;
	public Button LoadGame3Button;
	public Button LoadGame4Button;
	public Button LoadGame5Button;

	// Массив компонентов текста кнопок
	private Text[] loadButtonTexts; // Добавил недостающее объявление
									// Перехват события старта сцены для установки начальных значений
	void Start()
	{
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseLoadSubMenuButton.onClick.AddListener(CloseLoadSubMenu);

		LoadGame1Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(1));
		LoadGame2Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(2));
		LoadGame3Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(3));
		LoadGame4Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(4));
		LoadGame5Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(5));

		// Получаем ссылки на текстовые компоненты кнопок
		loadButtonTexts = new Text[]
		{
		LoadGame1Button.GetComponentInChildren<Text>(),
		LoadGame2Button.GetComponentInChildren<Text>(),
		LoadGame3Button.GetComponentInChildren<Text>(),
		LoadGame4Button.GetComponentInChildren<Text>(),
		LoadGame5Button.GetComponentInChildren<Text>()
		};

		// Сразу обновляем надписи кнопок
		RefreshLoadButtonLabels();
	}

	private void Update()
	{
		if (InputManager.Instance.GetKeyPauseMenu() && LoadSubMenuCanvas.gameObject.activeInHierarchy)
		{
			CloseLoadSubMenu();
		}
	}

	public void CloseLoadSubMenu()
	{
		LoadSubMenuCanvas.gameObject.SetActive(false);

		pauseMenuController.PauseMenuCanvas.gameObject.SetActive(true);

		Debug.Log("LoadSubMenu closed");
	}

	// Метод обновления подписей кнопок загрузки
	public void RefreshLoadButtonLabels()
	{
		string[] saveInfos = DataPersistenceManager.Instance.GetSaveInfo();

		for (int i = 0; i < loadButtonTexts.Length; i++)
		{
			loadButtonTexts[i].text = $"Слот {i + 1}: {saveInfos[i]}";
		}
	}
}
