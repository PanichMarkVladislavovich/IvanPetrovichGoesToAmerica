using UnityEngine;

public abstract class VendingMachineItem : InteractableItem
{
	public abstract string GoodsName { get; protected set; }

	public override string InteractionHint => $"Купить {GoodsName} в {ItemName}?";

	public sealed override void Interact()
	{
		Debug.Log($"Вы купили {GoodsName} в {ItemName}");
	//	Destroy(gameObject);
		//PlayerMoneyManager.Instance.AddMoney(MoneyValue);
	}
}