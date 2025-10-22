using UnityEngine;

public class VendingMachineAmmo : VendingMachineItem
{
	//public override int MoneyValue { get; protected set; } = 5;

	public override string ItemName => "Автомате по продаже патронов";

	public override string GoodsName => "Патроны";

	
	public override void Interact()
	{
		Debug.Log($"Вы купили {GoodsName} в {ItemName}");
		//	Destroy(gameObject);
		//PlayerMoneyManager.Instance.AddMoney(MoneyValue);
	}
}