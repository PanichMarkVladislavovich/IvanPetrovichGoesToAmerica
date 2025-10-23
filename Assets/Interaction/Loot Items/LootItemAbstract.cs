using UnityEngine;

public abstract class LootItemAbstract : MonoBehaviour, IInteractable
{
	public virtual string ItemName => gameObject.name;

	public virtual string InteractionHint => $"Поднять {ItemName}";

	public virtual int MoneyValue { get; protected set; }
	public abstract void Interact();
}