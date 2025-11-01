using UnityEngine;

public class LootItemGoldBar : LootItemAbstract
{
	public override int MoneyValue { get; protected set; } = 20;

	public override string InteractionItemName => "Золотой слиток";

	

	public override void Interact()
	{
		

		Debug.Log($"Вы подняли {InteractionItemName}, получаете {MoneyValue} рублей");
		Destroy(gameObject);
		PlayerMoneyManager.Instance.AddMoney(MoneyValue);
		WasLootItemCollected = true;
		
	}



	public override void SaveData(ref GameData data)
	{
		

		if (GameSceneManager.Instance.CurrentSceneName == "SceneTEST")
		{
			data.LootItemSceneTESTDataGoldBar[LootItemIndex].LootItemIndex = LootItemIndex;

			if (WasLootItemCollected == true)
			{
				data.LootItemSceneTESTDataGoldBar[LootItemIndex].WasLootItemCollected = true;
			}
			else data.LootItemSceneTESTDataGoldBar[LootItemIndex].WasLootItemCollected = false;
			//Debug.Log("IT IS SAVED");

		}

		if (GameSceneManager.Instance.CurrentSceneName == "Scene1")
		{
			data.LootItemScene1DataGoldBar[LootItemIndex].LootItemIndex = LootItemIndex;

			if (WasLootItemCollected == true)
			{
				data.LootItemScene1DataGoldBar[LootItemIndex].WasLootItemCollected = true;
			}
			else data.LootItemScene1DataGoldBar[LootItemIndex].WasLootItemCollected = false;
			//Debug.Log("IT IS SAVED");

		}

		
	}

	public override void LoadData(GameData data)
	{
		if (GameSceneManager.Instance.CurrentSceneName == "SceneTEST")
		{
			if (data.LootItemSceneTESTDataGoldBar[LootItemIndex].WasLootItemCollected == true)
			{
				WasLootItemCollected = true;
				Destroy(gameObject);
			}
		}

		if (GameSceneManager.Instance.CurrentSceneName == "Scene1")
		{
			if (data.LootItemScene1DataGoldBar[LootItemIndex].WasLootItemCollected == true)
			{
				WasLootItemCollected = true;
				Destroy(gameObject);
			}
		}

	}


}