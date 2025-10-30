public interface IInteractable
{
	string InteractionItemName { get; }
	string InteractionHint { get; }
	void Interact();
}