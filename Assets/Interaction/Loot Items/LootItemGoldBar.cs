using UnityEngine;

public class LootItemGoldBar : LootItemAbstract
{
	public override int MoneyValue { get; protected set; } = 20;

	public override string ItemName => "Золотой слиток";


	public override void Interact()
	{
		Debug.Log($"Вы подняли {ItemName}, получаете {MoneyValue} рублей");
		Destroy(gameObject);
		PlayerMoneyManager.Instance.AddMoney(MoneyValue);
	}
}