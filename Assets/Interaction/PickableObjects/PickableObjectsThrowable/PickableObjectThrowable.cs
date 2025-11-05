using UnityEngine;

public class PickableObjectThrowable : PickableObjectAbstract, IThrowable
{
	//private bool _wasObjectDestroyed;

	private bool _canObjectBeDestroyedOnImpact;

	public float ObjectThrowPower => 10f;

	private void OnCollisionEnter(Collision collision)
	{
		if (_canObjectBeDestroyedOnImpact)
		{
			RigidBody.isKinematic = true;

			//_wasObjectDestroyed = true;
			Destroy(gameObject);
		}
	}

	public void ThrowObject()
	{
		Debug.Log($"Throwed {InteractionObjectNameSystem}");
		gameObject.tag = "Interactable";
		BoxCollider.enabled = true;
		RigidBody.isKinematic = false;
		IsObjectPickedUp = false;

		

		_canObjectBeDestroyedOnImpact = true;
		// Отцепляем объект от игрока
		transform.parent = null;

		RigidBody.AddForce(CachedPlayer.transform.forward * ObjectThrowPower, ForceMode.Impulse);
	}
}
