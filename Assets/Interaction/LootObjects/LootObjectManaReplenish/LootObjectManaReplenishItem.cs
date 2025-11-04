using UnityEngine;

public class LootObjectManaReplenishItem : LootObjectAbstract
{

	public override int MoneyValue => 0;

	public override string InteractionObjectNameSystem => "ManaReplenishItem";
	public override string InteractionObjectNameUI => "Предмет восстаналивает ману";


	public override void Interact()
	{
		if (PlayerHealthManager.Instance.CurrentHealingItemsNumber < 9)
		{
			Debug.Log($"Вы подняли {InteractionObjectNameUI}");
			Destroy(gameObject);
			PlayerManaManager.Instance.AddManaReplenishItem();
			WasLootItemCollected = true;
		}
		else Debug.Log("Can't pick up more ManaReplenish Items");

	}

	
}