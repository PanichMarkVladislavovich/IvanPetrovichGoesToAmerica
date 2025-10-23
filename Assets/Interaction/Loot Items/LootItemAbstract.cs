using UnityEngine;
using System;
using Unity.IO.LowLevel.Unsafe;

public abstract class LootItemAbstract : MonoBehaviour, IInteractable, IDataPersistence
{
	public virtual Vector3 LootItemPosition { get; protected set; }
	public virtual Quaternion LootItemRotation { get; protected set; }
	public virtual string ItemName => gameObject.name;

	public virtual string InteractionHint => $"Поднять {ItemName}";

	public virtual int MoneyValue { get; protected set; }

	public virtual bool WasLootItemCollected { get; protected set; }
	

	// Поле для внутреннего индекса и хранения типа предмета
	public int CollectableIndex { get; private set; }
	public Type CollectableType { get; private set; }

	// Универсальная процедура назначения индекса
	internal void AssignLootItemIndex(int index, Type type)
	{
		CollectableIndex = index;
		CollectableType = type;
	}

	public abstract void Interact();

	public abstract void LoadData(GameData data);


	public abstract void SaveData(ref GameData data);

}