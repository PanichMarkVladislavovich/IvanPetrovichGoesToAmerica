public interface IInteractable
{
	string InteractionHint { get; }     // Подсказка, появляющаяся на экране
	void Interact();                     // Метод обработки взаимодействия
}