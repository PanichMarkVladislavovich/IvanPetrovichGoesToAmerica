using UnityEngine;

public abstract class LootItem : MonoBehaviour, IInteractable
{
	public string ItemName;              // Название предмета
	public int MoneyValue;               // Денежная стоимость
	public string InteractionHint => $"Поднять {ItemName}?";

	public abstract void Interact();     // Переопределён метод взаимодействия
}
