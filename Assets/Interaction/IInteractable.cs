public interface IInteractable
{
	string InteractionObjectNameSystem { get; }
	string InteractionObjectNameUI { get; }
	string InteractionHint { get; }
	void Interact();
}