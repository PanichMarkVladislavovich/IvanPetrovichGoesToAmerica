using UnityEngine;
public class PlayerCameraFirstPersonRender : MonoBehaviour
{
	PlayerCamera playerCamera;
	public PlayerCameraStateType playerCameraStateType;

	public GameObject PlayerCameraObject;

	public GameObject PlayerHeadParent;
	public GameObject[] PlayerHeadChildren;
	void Start()
	{
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();

		// Get all PlayerHead children
		PlayerHeadChildren = new GameObject[PlayerHeadParent.transform.childCount];

		for (int i = 0; i < PlayerHeadChildren.Length; i++)
		{
			PlayerHeadChildren[i] = PlayerHeadParent.transform.GetChild(i).gameObject;
		}
	}

	void FixedUpdate()
	{
		if (playerCamera.IsPlayerCameraFirstPerson == true) 
		{
			// In 1st person DO NOT show Head render and its children
			PlayerHeadParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			for (int i = 0; i < PlayerHeadChildren.Length; i++)
			{
				PlayerHeadChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
		}
		else 
		{
			// In 3rd person show Head render and its children
			PlayerHeadParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			for (int i = 0; i < PlayerHeadChildren.Length; i++)
			{
				PlayerHeadChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}
		}
	}
}
