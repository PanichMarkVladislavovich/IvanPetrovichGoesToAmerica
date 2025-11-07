using UnityEngine;

public class VendingMachineAmmo : VendingMachineAbstract
{

	public GameObject AmmoItemModel;


	public override string InteractionObjectNameUI => "Автомате по продаже патронов";

	public override string GoodsName => "Патроны";

	private int goodsPrice = 30;

	private void Awake()
	{
		//AmmoItemModel = Resources.Load<GameObject>("HealingItem"); // Загружаем префаб револьвера

	}
	public override void Interact()
	{
		
		/*
		if (PlayerMoneyManager.Instance.PlayerMoney >= goodsPrice)
		{
			Vector3 spawnPosition = transform.position + new Vector3(-1f, 0.5f, 0f); // Сместили объект вверх на единицу

			Debug.Log($"Вы купили {GoodsName} в {InteractionObjectNameUI}");
			Instantiate(AmmoItemModel, spawnPosition, Quaternion.identity);
			PlayerMoneyManager.Instance.DeductMoney(-goodsPrice);
		}
		else Debug.Log("Not enought Money");
		*/

		Debug.Log("Not implemented yet!");
	}
}