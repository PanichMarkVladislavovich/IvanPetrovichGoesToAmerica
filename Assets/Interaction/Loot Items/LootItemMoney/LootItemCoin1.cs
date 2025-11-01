using UnityEngine;

public class LootItemCoin1 : LootItemAbstract
{
	public override int MoneyValue { get; protected set; } = 1;

	public override string InteractionItemName => "Монета 1 рубль";


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