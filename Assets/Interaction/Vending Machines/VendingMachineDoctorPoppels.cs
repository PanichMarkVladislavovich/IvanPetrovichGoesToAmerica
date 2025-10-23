using UnityEngine;

public class VendingMachineDoctorPoppels : VendingMachineAbstract
{
	//public override int MoneyValue { get; protected set; } = 5;

	public override string ItemName => "Автомате по продаже Сиропа";

	public override string GoodsName => "Сироп";


	public override void Interact()
	{
		Debug.Log($"Вы купили {GoodsName} в {ItemName}");
		//	Destroy(gameObject);
		//PlayerMoneyManager.Instance.AddMoney(MoneyValue);
	}
}