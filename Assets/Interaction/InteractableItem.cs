using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{
	public virtual string ItemName => gameObject.name;

	public virtual string InteractionHint => $"{ItemName}: неопределённая подсказка"; // Дефолтная версия

	public abstract void Interact();
}