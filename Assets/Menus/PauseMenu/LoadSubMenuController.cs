using System;
using TMPro;
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
	private Text[] levelNameTexts;
	private Text[] moneyTexts;
	private Text[] emptySlotTexts;
	void Start()
	{
		pauseMenuController = GetComponent<PauseMenuController>();

		CloseLoadSubMenuButton.onClick.AddListener(CloseLoadSubMenu);

		LoadGame1Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(1));
		LoadGame2Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(2));
		LoadGame3Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(3));
		LoadGame4Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(4));
		LoadGame5Button.onClick.AddListener(() => DataPersistenceManager.Instance.LoadGame(5));
		// Формируем массивы текстовых компонентов
        levelNameTexts = new Text[]
        {
            LoadGame1Button.transform.Find("Text_CurrentLevelNameUI")?.GetComponent<Text>(), // Название уровня
            LoadGame2Button.transform.Find("Text_CurrentLevelNameUI")?.GetComponent<Text>(),
            LoadGame3Button.transform.Find("Text_CurrentLevelNameUI")?.GetComponent<Text>(),
            LoadGame4Button.transform.Find("Text_CurrentLevelNameUI")?.GetComponent<Text>(),
            LoadGame5Button.transform.Find("Text_CurrentLevelNameUI")?.GetComponent<Text>()
        };

        moneyTexts = new Text[]
        {
            LoadGame1Button.transform.Find("Text_PlayerMoney")?.GetComponent<Text>(), // Деньги игрока
            LoadGame2Button.transform.Find("Text_PlayerMoney")?.GetComponent<Text>(),
            LoadGame3Button.transform.Find("Text_PlayerMoney")?.GetComponent<Text>(),
            LoadGame4Button.transform.Find("Text_PlayerMoney")?.GetComponent<Text>(),
            LoadGame5Button.transform.Find("Text_PlayerMoney")?.GetComponent<Text>()
        };

        emptySlotTexts = new Text[]
        {
            LoadGame1Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(), // Слот пуст
            LoadGame2Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame3Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame4Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame5Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>()
        };
		// Сразу обновляем надписи кнопок
		//RefreshLoadButtonLabels();
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
	// Метод для обновления информации на кнопках
	// Метод для обновления информации на кнопках
	public void RefreshLoadButtonLabels()
	{
		var extendedSaveInfos = DataPersistenceManager.Instance.GetExtendedSaveInfo();

		for (int i = 0; i < extendedSaveInfos.Length; i++)
		{
			var (levelName, playerMoney) = extendedSaveInfos[i];

			if (!string.IsNullOrEmpty(levelName)) // Если данные есть
			{
				// Формирование имени файла иконки
				string iconName = $"{levelName}";

				Debug.Log(iconName);
				// Загрузка спрайта иконки
				//Sprite sprite = Resources.Load<Sprite>($"Sprites/{iconName}");
				Sprite sprite = Resources.Load<Sprite>("Sprites/Scene1");


				if (sprite != null)
				{
					// Устанавливаем иконку на компонент Image в Level_Image
					LoadGame4Button.transform.Find("Level_Image").GetComponent<Image>().sprite = sprite;
				}
				else Debug.Log("Sprite EMPTY");

				// Обновляем текстовую информацию
				levelNameTexts[i].text = levelName;                 // Название уровня
				moneyTexts[i].text = playerMoney.ToString();        // Деньги

				// Включаем компоненты
				levelNameTexts[i].gameObject.SetActive(true);
				moneyTexts[i].gameObject.SetActive(true);
				emptySlotTexts[i].gameObject.SetActive(false);
			}
			else
			{
				// Данные не найдены, показываем текст "Слот пуст"
				levelNameTexts[i].gameObject.SetActive(false);
				moneyTexts[i].gameObject.SetActive(false);
				emptySlotTexts[i].text = $"Слот {i + 1} пуст";
				emptySlotTexts[i].gameObject.SetActive(true);
			}
		}
	}
}
