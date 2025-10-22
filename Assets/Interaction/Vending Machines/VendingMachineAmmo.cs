using UnityEngine;

public class VendingMachineAmmo : VendingMachineItem
{
	//public override int MoneyValue { get; protected set; } = 5;

	public override string ItemName => "Автомате по продаже патронов";

	public override string GoodsName { get; protected set; }  = "Патроны";

	//public override int MoneyValue { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

	// Этот класс не нуждается в переопределении InteractionHint,
	// так как базовая реализация вполне подходит
}