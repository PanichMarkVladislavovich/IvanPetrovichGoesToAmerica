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
		

			if (WasLootItemCollected == true)
			{
				data.LootItemSceneTESTDataGoldBar[LootItemIndex].WasLootItemCollected = true;
			}
			else data.LootItemSceneTESTDataGoldBar[LootItemIndex].WasLootItemCollected = false;


			data.LootItemSceneTESTDataGoldBar[LootItemIndex].LootItemIndex = LootItemIndex;
		



		

			//data.LootItemSCENE2DataGoldBar[LootItemIndex].LootItemIndex = data.LootItemSCENE2DataGoldBar[LootItemIndex].LootItemIndex;

			//data.LootItemSCENE2DataGoldBar[LootItemIndex].WasLootItemCollected = data.LootItemSCENE2DataGoldBar[LootItemIndex].WasLootItemCollected;

		
	}

	public override void LoadData(GameData data)
	{
		if (data.LootItemSceneTESTDataGoldBar[LootItemIndex].WasLootItemCollected == true)
		{
			WasLootItemCollected = true;
			Destroy(gameObject);
		}

		//data.LootItemSCENE2DataGoldBar[LootItemIndex].LootItemIndex = data.LootItemSCENE2DataGoldBar[LootItemIndex].LootItemIndex;

		//data.LootItemSCENE2DataGoldBar[LootItemIndex].WasLootItemCollected = data.LootItemSCENE2DataGoldBar[LootItemIndex].WasLootItemCollected;


	}


}