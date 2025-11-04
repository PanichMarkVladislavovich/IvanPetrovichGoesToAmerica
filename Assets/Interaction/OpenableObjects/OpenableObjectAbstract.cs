using UnityEngine;
using System;
using Unity.IO.LowLevel.Unsafe;

public abstract class OpenableObjectAbstract : MonoBehaviour, IInteractable
{
	//public virtual Vector3 LootItemPosition { get; protected set; }
	//public virtual Quaternion LootItemRotation { get; protected set; }

	// Приватное поле, видимое в инспекторе
	[SerializeField]
	private string _interactionItemName;
	public virtual string InteractionItemNameUI => _interactionItemName;

	// Свойство подсказки теперь учитывает состояние двери
	public virtual string InteractionHint => !IsDoorOpened ? $"Открыть {InteractionItemNameUI}" : $"Закрыть {InteractionItemNameUI}";

	public virtual bool IsDoorOpened { get; protected set; }


	public int DoorIndex { get; protected set; }
	


	public abstract void Interact();


}