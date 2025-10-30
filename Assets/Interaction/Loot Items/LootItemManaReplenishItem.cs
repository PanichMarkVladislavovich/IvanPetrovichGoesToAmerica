using UnityEngine;

public class LootItemManaReplenishItem : LootItemAbstract
{

	public override int MoneyValue { get; protected set; } = 0;

	public override string InteractionItemName => "Предмет восстаналивает ману";


	public override void Interact()
	{
		if (PlayerHealthManager.Instance.CurrentHealingItemsNumber < 9)
		{
			Debug.Log($"Вы подняли {InteractionItemName}");
			Destroy(gameObject);
			PlayerManaManager.Instance.AddManaReplenishItem();
		}
		else Debug.Log("Can't pick up more ManaReplenish Items");

	}

	public override void LoadData(GameData data)
	{

	}

	public override void SaveData(ref GameData data)
	{

	}
}