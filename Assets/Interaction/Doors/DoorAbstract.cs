using UnityEngine;
using System;
using Unity.IO.LowLevel.Unsafe;

public abstract class DoorAbstract : MonoBehaviour, IInteractable
{
	//public virtual Vector3 LootItemPosition { get; protected set; }
	//public virtual Quaternion LootItemRotation { get; protected set; }
	public virtual string InteractionItemName => gameObject.name;

	// Свойство подсказки теперь учитывает состояние двери
	public virtual string InteractionHint => !IsDoorOpened ? $"Открыть {InteractionItemName}" : $"Закрыть {InteractionItemName}";

	public virtual bool IsDoorOpened { get; protected set; }


	public int DoorIndex { get; protected set; }
	
	/*
	internal void AssignLootItemIndex(int index)
	{
		Door = index;
	}
*/

	public abstract void Interact();


	/*
	public abstract void LoadData(GameData data);


	public abstract void SaveData(ref GameData data);
	*/

}