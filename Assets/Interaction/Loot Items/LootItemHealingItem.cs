using UnityEngine;

public class LootItemHealingItem : LootItemAbstract
{

	public override int MoneyValue { get; protected set; } = 0;

	public override string ItemName => "Лечащий предмет";


	public override void Interact()
	{
		if (PlayerHealthManager.Instance.CurrentHealingItemsNumber < 9)
		{
			Debug.Log($"Вы подняли {ItemName}");
			Destroy(gameObject);
			PlayerHealthManager.Instance.AddHealingItem();
		}
		else Debug.Log("Can't pick up more Healing Items");

	}

	public override void LoadData(GameData data)
	{
		
	}

	public override void SaveData(ref GameData data)
	{
		
	}
}