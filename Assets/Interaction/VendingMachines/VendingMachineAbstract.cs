using UnityEngine;

public abstract class VendingMachineAbstract : MonoBehaviour, IInteractable
{

	public virtual string InteractionObjectNameUI => gameObject.name;
	public virtual string InteractionHint => $"Купить {GoodsName} в {InteractionObjectNameUI}?";
	public virtual string GoodsName => gameObject.name;

	public string InteractionObjectNameSystem => null;

	public abstract void Interact();
}
	