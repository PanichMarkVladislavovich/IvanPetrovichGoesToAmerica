using UnityEngine;

public class LootItemBanknote100 : LootItemAbstract
{
	public override int MoneyValue { get; protected set; } = 100;

	public override string InteractionItemName => "Банкнота 100 рублей";


	public override void Interact()
	{
		Debug.Log($"Вы подняли {InteractionItemName}, получаете {MoneyValue} рублей");
		Destroy(gameObject);
		PlayerMoneyManager.Instance.AddMoney(MoneyValue);
	}

	public override void LoadData(GameData data)
	{

	}

	public override void SaveData(ref GameData data)
	{

	}
}