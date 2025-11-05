using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public abstract class PickableObjectAbstract : MonoBehaviour, IInteractable, IDataPersistence, IPickable
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

	public bool IsObjectPickedUp { get; private set; }

	void Start()
	{
		boxCollider = GetComponent<BoxCollider>();
		rigidBody = GetComponent<Rigidbody>();
		_cachedPlayer = GameObject.Find("Player");
	}

	
	private void Update()
	{
		if (IsObjectPickedUp && InputManager.Instance.GetKeyInteract())
		{
			DropOffObject();
		}
	}
	

	public void Interact()
	{
		PickUpObject();
	}

	public void PickUpObject()
	{
		if (!IsObjectPickedUp)
		{
			if (_cachedPlayer != null)
			{
				Debug.Log("Interact");

				gameObject.tag = "Untagged";
				boxCollider.enabled = false;
				rigidBody.isKinematic = true;

				// Начинаем плавное перемещение
				StartCoroutine(MoveTowardsTarget());

				// Другие настройки остаются такими же
				transform.parent = _cachedPlayer.transform;
				transform.rotation = Quaternion.Euler(0, _cachedPlayer.transform.localEulerAngles.y, 0);
				IsObjectPickedUp = true;
			}
			else
			{
				Debug.Log("Player not found!");
			}
		}
	}


	public virtual void DropOffObject()
	{
			Debug.Log("DropOff");
			gameObject.tag = "Interactable";
			boxCollider.enabled = true;
			rigidBody.isKinematic = false;
			IsObjectPickedUp = false;

			// Отцепляем объект от игрока
			transform.parent = null;

		
	}

	IEnumerator MoveTowardsTarget()
	{

		while (true)
		{
			// Рассчитываем новую целевую позицию каждый кадр
			Vector3 targetPosition = _cachedPlayer.transform.position + _cachedPlayer.transform.forward * 0.5f + Vector3.up * 1f;

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
		transform.position = _cachedPlayer.transform.position + _cachedPlayer.transform.forward * 0.5f + Vector3.up * 1f;
	}


	public void SaveData(ref GameData data)
	{
		
	}

	public void LoadData(GameData data)
	{
		
	}

	
}
