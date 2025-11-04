using UnityEngine;

public class LootItemManaReplenishItem : LootItemAbstract
{

	public override int MoneyValue => 0;

	public override string InteractionItemNameSystem => "ManaReplenishItem";
	public override string InteractionItemNameUI => "Предмет восстаналивает ману";


	public override void Interact()
	{
		if (PlayerHealthManager.Instance.CurrentHealingItemsNumber < 9)
		{
			Debug.Log($"Вы подняли {InteractionItemNameUI}");
			Destroy(gameObject);
			PlayerManaManager.Instance.AddManaReplenishItem();
			WasLootItemCollected = true;
		}
		else Debug.Log("Can't pick up more ManaReplenish Items");

	}

	
}