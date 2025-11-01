using UnityEngine;

public class LootItemBanknote25: LootItemAbstract
{
	public override int MoneyValue { get; protected set; } = 25;

	public override string InteractionItemName => "Банкнота 25 рублей";


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