using UnityEngine;

public class VendingMachineDoctorPoppels : VendingMachineAbstract
{

	public GameObject ManaReplenishItemModel;


	public override string InteractionObjectNameUI => "Автомате по продаже Сиропа";

	public override string GoodsName => "Сироп";

	private int goodsPrice = 15;

	private void Awake()
	{
		ManaReplenishItemModel = Resources.Load<GameObject>("ManaReplenishItem"); // Загружаем префаб револьвера

	}
	public override void Interact()
	{
		if (PlayerMoneyManager.Instance.PlayerMoney >= goodsPrice)
		{
			Vector3 spawnPosition = transform.position + new Vector3(-1f, 0.5f, 0f); // Сместили объект вверх на единицу

			Debug.Log($"Вы купили {GoodsName} в {InteractionObjectNameUI}");
			Instantiate(ManaReplenishItemModel, spawnPosition, Quaternion.identity);
			PlayerMoneyManager.Instance.DeductMoney(-goodsPrice);
		}
		else Debug.Log("Not enought Money");
	}
}