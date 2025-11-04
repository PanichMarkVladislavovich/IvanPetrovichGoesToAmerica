using UnityEngine;

public class LootItemHealingItem : LootItemAbstract
{

	public override int MoneyValue => 0;

	public override string InteractionItemNameSystem => "HealingItem";
	public override string InteractionItemNameUI => "Лечащий предмет";


	public override void Interact()
	{
		if (PlayerHealthManager.Instance.CurrentHealingItemsNumber < 9)
		{
			Debug.Log($"Вы подняли {InteractionItemNameUI}");
			Destroy(gameObject);
			PlayerHealthManager.Instance.AddHealingItem();
			WasLootItemCollected = true;
		}
		else Debug.Log("Can't pick up more Healing Items");

	}

	
}