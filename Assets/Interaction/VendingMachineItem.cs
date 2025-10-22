using UnityEngine;

public abstract class VendingMachineItem : MonoBehaviour, IInteractable
{

	public virtual string ItemName => gameObject.name;
	public virtual string InteractionHint => $" упить {GoodsName} в {ItemName}?";
	public virtual string GoodsName => gameObject.name;



	public abstract void Interact();
}
	