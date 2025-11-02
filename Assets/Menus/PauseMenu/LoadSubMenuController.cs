using System;
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


	private Text[] currentDateAndTimeTexts;

	private Text[] currentSceneNameUITexts;
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
		currentDateAndTimeTexts = new Text[]
        {
            LoadGame1Button.transform.Find("Text_CurrentDateAndTime")?.GetComponent<Text>(),
            LoadGame2Button.transform.Find("Text_CurrentDateAndTime")?.GetComponent<Text>(),
            LoadGame3Button.transform.Find("Text_CurrentDateAndTime")?.GetComponent<Text>(),
            LoadGame4Button.transform.Find("Text_CurrentDateAndTime")?.GetComponent<Text>(),
            LoadGame5Button.transform.Find("Text_CurrentDateAndTime")?.GetComponent<Text>()
        };

        currentSceneNameUITexts = new Text[]
        {
            LoadGame1Button.transform.Find("Text_CurrentSceneNameUI")?.GetComponent<Text>(),
            LoadGame2Button.transform.Find("Text_CurrentSceneNameUI")?.GetComponent<Text>(),
            LoadGame3Button.transform.Find("Text_CurrentSceneNameUI")?.GetComponent<Text>(),
            LoadGame4Button.transform.Find("Text_CurrentSceneNameUI")?.GetComponent<Text>(),
            LoadGame5Button.transform.Find("Text_CurrentSceneNameUI")?.GetComponent<Text>()
        };

        emptySlotTexts = new Text[]
        {
            LoadGame1Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame2Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame3Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame4Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>(),
            LoadGame5Button.transform.Find("Text_EmptySlot")?.GetComponent<Text>()
        };
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

	public void RefreshLoadButtonLabels()
	{
		var extendedSaveInfos = DataPersistenceManager.Instance.GetExtendedSaveInfo();

		for (int i = 0; i < extendedSaveInfos.Length; i++)
		{
			var (currentDataAndTime, currentSceneNameUI, currentSceneNameSystem) = extendedSaveInfos[i];

			if (!string.IsNullOrEmpty(currentSceneNameSystem)) // Проверяем наличие сцены
			{
				// Обновляем текстовую информацию
				currentSceneNameUITexts[i].text = currentDataAndTime;                
				currentDateAndTimeTexts[i].text = currentSceneNameUI;        

				// Включаем компоненты
				currentSceneNameUITexts[i].gameObject.SetActive(true);
				currentDateAndTimeTexts[i].gameObject.SetActive(true);
				emptySlotTexts[i].gameObject.SetActive(false);

				// Формирование имени файла иконки
				string currentSceneBackgroundImage = $"{currentSceneNameSystem}";

				// Загрузка спрайта иконки
				Sprite sprite = Resources.Load<Sprite>($"Sprites/{currentSceneBackgroundImage}");

				if (sprite != null)
				{
					// Определяем нужную кнопку через if-else
					if (i == 0)
					{
						LoadGame1Button.transform.Find("Level_Image").gameObject.SetActive(true);
						LoadGame1Button.transform.Find("Level_Image").GetComponent<Image>().sprite = sprite;
					}
					else if (i == 1)
					{
						LoadGame2Button.transform.Find("Level_Image").gameObject.SetActive(true);
						LoadGame2Button.transform.Find("Level_Image").GetComponent<Image>().sprite = sprite;
					}
					else if (i == 2)
					{
						LoadGame3Button.transform.Find("Level_Image").gameObject.SetActive(true);
						LoadGame3Button.transform.Find("Level_Image").GetComponent<Image>().sprite = sprite;
					}
					else if (i == 3)
					{
						LoadGame4Button.transform.Find("Level_Image").gameObject.SetActive(true);
						LoadGame4Button.transform.Find("Level_Image").GetComponent<Image>().sprite = sprite;
					}
					else if (i == 4)
					{
						LoadGame5Button.transform.Find("Level_Image").gameObject.SetActive(true);
						LoadGame5Button.transform.Find("Level_Image").GetComponent<Image>().sprite = sprite;
					}
				}
				else
				{
					Debug.LogError("Failed to load Scene Backgound Image");
				}
			}
			else
			{
				// Данные не найдены, показываем текст "Слот пуст"
				currentSceneNameUITexts[i].gameObject.SetActive(false);
				currentDateAndTimeTexts[i].gameObject.SetActive(false);
				emptySlotTexts[i].text = $"Слот {i + 1} пуст";
				emptySlotTexts[i].gameObject.SetActive(true);

				if (i == 0)
				{
					LoadGame1Button.transform.Find("Level_Image").gameObject.SetActive(false);
				}
				else if (i == 1)
				{
					LoadGame2Button.transform.Find("Level_Image").gameObject.SetActive(false);
				}
				else if (i == 2)
				{
					LoadGame3Button.transform.Find("Level_Image").gameObject.SetActive(false);
				}
				else if (i == 3)
				{
					LoadGame4Button.transform.Find("Level_Image").gameObject.SetActive(false);
				}
				else if (i == 4)
				{
					LoadGame5Button.transform.Find("Level_Image").gameObject.SetActive(false);
				}
			}
		}
	}
}
