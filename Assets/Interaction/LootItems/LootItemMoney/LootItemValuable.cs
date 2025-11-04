using UnityEngine;

public class LootItemValuable : LootItemAbstract
{
	public override void Interact()
	{
		Debug.Log($"Вы подняли {InteractionItemNameUI}, получаете {MoneyValue} рублей");
		Destroy(gameObject);
		PlayerMoneyManager.Instance.AddMoney(MoneyValue);
		WasLootItemCollected = true;
	}

	
}