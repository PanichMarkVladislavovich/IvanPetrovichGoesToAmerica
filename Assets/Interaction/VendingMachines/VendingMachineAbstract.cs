using UnityEngine;

public abstract class VendingMachineAbstract : MonoBehaviour, IInteractable
{

	public virtual string InteractionItemName => gameObject.name;
	public virtual string InteractionHint => $"Купить {GoodsName} в {InteractionItemName}?";
	public virtual string GoodsName => gameObject.name;



	public abstract void Interact();
}
	