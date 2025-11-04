using UnityEngine;

public class LootItemValuable : LootItemAbstract
{
	public override void Interact()
	{
		Debug.Log($"Вы подняли {InteractionItemName}, получаете {MoneyValue} рублей");
		Destroy(gameObject);
		PlayerMoneyManager.Instance.AddMoney(MoneyValue);
		WasLootItemCollected = true;
	}

	public override void SaveData(ref GameData data)
	{


		if (GameSceneManager.Instance.CurrentSceneSystemName == "SceneTEST")
		{
			data.LootItemSceneTEST[LootItemIndex].LootItemIndex = LootItemIndex;

			if (WasLootItemCollected == true)
			{
				data.LootItemSceneTEST[LootItemIndex].WasLootItemCollected = true;
			}
			else data.LootItemSceneTEST[LootItemIndex].WasLootItemCollected = false;

		}

		if (GameSceneManager.Instance.CurrentSceneSystemName == "Scene1")
		{
			data.LootItemScene1[LootItemIndex].LootItemIndex = LootItemIndex;

			if (WasLootItemCollected == true)
			{
				data.LootItemScene1[LootItemIndex].WasLootItemCollected = true;
			}
			else data.LootItemScene1[LootItemIndex].WasLootItemCollected = false;

		}


	}

	public override void LoadData(GameData data)
	{
		if (GameSceneManager.Instance.CurrentSceneSystemName == "SceneTEST")
		{
			if (data.LootItemSceneTEST[LootItemIndex].WasLootItemCollected == true)
			{
				WasLootItemCollected = true;
				Destroy(gameObject);
			}
		}

		if (GameSceneManager.Instance.CurrentSceneSystemName == "Scene1")
		{
			if (data.LootItemScene1[LootItemIndex].WasLootItemCollected == true)
			{
				WasLootItemCollected = true;
				Destroy(gameObject);
			}
		}

	}
}