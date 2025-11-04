using UnityEngine;

public abstract class VendingMachineAbstract : MonoBehaviour, IInteractable
{

	public virtual string InteractionItemNameUI => gameObject.name;
	public virtual string InteractionHint => $"Купить {GoodsName} в {InteractionItemNameUI}?";
	public virtual string GoodsName => gameObject.name;



	public abstract void Interact();
}
	