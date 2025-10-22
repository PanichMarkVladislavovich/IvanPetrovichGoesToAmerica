using UnityEngine;

public class LootItemCoin5 : LootItem
{
	public override int MoneyValue { get; protected set; } = 5;

	public override string ItemName => "Монета 5 рублей";

	// Этот класс не нуждается в переопределении InteractionHint,
	// так как базовая реализация вполне подходит
}