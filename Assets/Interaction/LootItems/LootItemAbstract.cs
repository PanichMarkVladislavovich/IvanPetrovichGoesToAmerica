using UnityEngine;
using System;
using Unity.IO.LowLevel.Unsafe;

public abstract class LootItemAbstract : MonoBehaviour, IInteractable, IDataPersistence
{

	[SerializeField]
	private string _interactionItemName;
	public virtual string InteractionItemName => _interactionItemName;

	public virtual string InteractionHint => $"Поднять {InteractionItemName}";


	[SerializeField]
	private int _moneyValue;

	public virtual int MoneyValue => _moneyValue;

	public virtual bool WasLootItemCollected { get; protected set; }
	

	// Поле для внутреннего индекса и хранения типа предмета
	public int LootItemIndex { get; protected set; }
	
	internal void AssignLootItemIndex(int index)
	{
		LootItemIndex = index;
	}

	public abstract void Interact();

	public abstract void LoadData(GameData data);


	public abstract void SaveData(ref GameData data);

}