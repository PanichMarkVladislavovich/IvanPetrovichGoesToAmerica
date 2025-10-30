using UnityEngine;

public class VendingMachineAmmo : VendingMachineAbstract
{

	public GameObject HealingItemModel;


	public override string InteractionItemName => "Автомате по продаже патронов";

	public override string GoodsName => "Патроны";

	private int goodsPrice = 30;

	private void Awake()
	{
		HealingItemModel = Resources.Load<GameObject>("HealingItem"); // Загружаем префаб револьвера

	}
	public override void Interact()
	{
		if (PlayerMoneyManager.Instance.PlayerMoney >= goodsPrice)
		{
			Vector3 spawnPosition = transform.position + new Vector3(-1f, 0.5f, 0f); // Сместили объект вверх на единицу

			Debug.Log($"Вы купили {GoodsName} в {InteractionItemName}");
			Instantiate(HealingItemModel, spawnPosition, Quaternion.identity); 
			PlayerMoneyManager.Instance.DeductMoney(-goodsPrice);
		}
		else Debug.Log("Not enought Money");
	}
}