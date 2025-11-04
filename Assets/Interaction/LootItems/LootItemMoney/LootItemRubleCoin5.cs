using UnityEngine;

public class LootItemRubleCoin5 : LootItemAbstract
{
	public override int MoneyValue { get; protected set; } = 5;

	public override string InteractionItemName => "Монета 5 рублей";


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