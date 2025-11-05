using UnityEngine;

public class PickableObjectThrowable : PickableObjectAbstract, IThrowable
{
	public float ObjectThrowPower => 10f;

	public void ThrowObject()
	{
		Debug.Log($"Throwed {InteractionObjectNameSystem}");
		gameObject.tag = "Interactable";
		BoxCollider.enabled = true;
		RigidBody.isKinematic = false;
		IsObjectPickedUp = false;

		// Отцепляем объект от игрока
		transform.parent = null;

		RigidBody.AddForce(CachedPlayer.transform.forward * ObjectThrowPower, ForceMode.Impulse);
	}
}
