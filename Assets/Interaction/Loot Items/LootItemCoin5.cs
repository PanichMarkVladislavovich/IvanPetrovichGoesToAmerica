using UnityEngine;

public class LootItemCoin5 : LootItem
{
	void Start()
	{
		ItemName = "Монета 5 Рублей";
		MoneyValue = 5;
}

	public override void Interact()
	{
		Debug.Log($"Вы подняли {ItemName}, получаете ${MoneyValue}");
		Destroy(gameObject);           // Удаляем объект
		PlayerMoneyManager.Instance.AddMoney(MoneyValue); // Добавляем деньги игроку
	}
}
