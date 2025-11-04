using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public abstract class PickableObjectAbstract : MonoBehaviour, IInteractable, IDataPersistence
{
	GameObject _cachedPlayer;

	private BoxCollider boxCollider;
	private Rigidbody rigidBody;

	[SerializeField]
	private string _interactionItemNameSystem;
	public virtual string InteractionObjectNameSystem => _interactionItemNameSystem;



	// Приватное поле, видимое в инспекторе
	[SerializeField]
	private string _interactionItemNameUI;
	public virtual string InteractionObjectNameUI => _interactionItemNameUI;

	public string InteractionHint => $"Поднять {InteractionObjectNameUI}?";

	private bool isObjectPickedUp;

	void Start()
	{
		boxCollider = GetComponent<BoxCollider>();
		rigidBody = GetComponent<Rigidbody>();
		_cachedPlayer = GameObject.Find("Player");
	}

	private void Update()
	{
		if (isObjectPickedUp && InputManager.Instance.GetKeyInteract())
		{
			gameObject.tag = "Interactable";
			boxCollider.enabled = true;
			rigidBody.isKinematic = false;
			isObjectPickedUp = false;

			// Отцепляем объект от игрока
			transform.parent = null;
		
		}
	}

	IEnumerator MoveTowardsTarget()
	{

		while (true)
		{
			// Рассчитываем новую целевую позицию каждый кадр
			Vector3 targetPosition = _cachedPlayer.transform.position + _cachedPlayer.transform.forward * 1f + Vector3.up * 1f;

			// Перемещаем объект к новой позиции
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);

			// Выход из цикла, если объект вплотную приблизился к игроку
			if ((transform.position - targetPosition).sqrMagnitude < 0.001f)
			{
				break;
			}

			yield return null;
		}

		// Установим последнюю позицию на случай погрешности
		transform.position = _cachedPlayer.transform.position + _cachedPlayer.transform.forward * 1f + Vector3.up * 1f;
	}

	public void Interact()
	{
		if (!isObjectPickedUp)
		{
			if (_cachedPlayer != null)
			{
				gameObject.tag = "Untagged";
				boxCollider.enabled = false;
				rigidBody.isKinematic = true;

				// Начинаем плавное перемещение
				StartCoroutine(MoveTowardsTarget());

				// Другие настройки остаются такими же
				transform.parent = _cachedPlayer.transform;
				transform.rotation = Quaternion.Euler(0, _cachedPlayer.transform.localEulerAngles.y, 0);
				isObjectPickedUp = true;
			}
			else
			{
				Debug.LogError("Player not found!");
			}
		}
	}





	/*
	public void Interact()
	{
		if (!isObjectPickedUp)
		{
			if (_cachedPlayer != null)
			{
				gameObject.tag = "Untagged";

				rigidBody.isKinematic = true;

				// Перемещаем объект к позиции игрока
				transform.position = _cachedPlayer.transform.position + _cachedPlayer.transform.forward * 1f + Vector3.up * 1f;

				// Устанавливаем объект как дочерний элемент игрока
				transform.parent = _cachedPlayer.transform;

				// Выравниваем вращение объекта (устанавливаем в ноль по всем осям)
				transform.rotation = Quaternion.Euler(0, _cachedPlayer.transform.localEulerAngles.y, 0);

				isObjectPickedUp = true;
			}
			else
			{
				Debug.LogError("Player not found!");
			}
		}
	}
	*/

	public void SaveData(ref GameData data)
	{
		
	}

	public void LoadData(GameData data)
	{
		
	}

	
}
