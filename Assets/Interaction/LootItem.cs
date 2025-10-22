using UnityEngine;

public abstract class LootItem : InteractableItem
{
	public abstract int MoneyValue { get; protected set; }

	public override string InteractionHint => $"Поднять {ItemName}";

	public sealed override void Interact()
	{
		Debug.Log($"Вы подняли {ItemName}, получаете ${MoneyValue}");
		Destroy(gameObject);
		PlayerMoneyManager.Instance.AddMoney(MoneyValue);
	}
}