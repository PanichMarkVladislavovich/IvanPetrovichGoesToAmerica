using UnityEngine;

public class VendingMachineAmmo : LootItem
{
	void Start()
	{
		ItemName = "Автомат с патронами";
		MoneyValue = 2;
	}

	public override void Interact()
	{
		Debug.Log($"Вы подняли {ItemName}, получаете ${MoneyValue}");
		//Destroy(gameObject);           // Удаляем объект
		//PlayerMoneyManager.Instance.AddMoney(MoneyValue); // Добавляем деньги игроку
	}
}
