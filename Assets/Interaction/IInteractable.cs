public interface IInteractable
{
	string ItemName { get; }
	string InteractionHint { get; }
	void Interact();
}