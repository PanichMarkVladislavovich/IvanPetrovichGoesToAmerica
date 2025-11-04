public interface IInteractable
{
	string InteractionItemNameUI { get; }
	string InteractionHint { get; }
	void Interact();
}