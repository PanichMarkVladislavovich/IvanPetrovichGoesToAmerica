using UnityEngine;
public class PlayerCameraFirstPersonRender : MonoBehaviour
{
	PlayerCamera playerCamera;
	WeaponController weaponController;
	public PlayerCameraStateType playerCameraStateType;

	public GameObject PlayerCameraObject;

	public GameObject PlayerHeadParent;
	public GameObject[] PlayerHeadChildren;

	public GameObject PlayerHandRightParent;
	public GameObject[] PlayerHandRightChildren;

	public GameObject PlayerHandLeftParent;
	public GameObject[] PlayerHandLeftChildren;

	void Start()
	{
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();
		weaponController = GetComponent<WeaponController>();

		// Get all PlayerHead children
		PlayerHeadChildren = new GameObject[PlayerHeadParent.transform.childCount];

		for (int i = 0; i < PlayerHeadChildren.Length; i++)
		{
			PlayerHeadChildren[i] = PlayerHeadParent.transform.GetChild(i).gameObject;
		}

		// Get all PlayerHandRight children
		PlayerHandRightChildren = new GameObject[PlayerHandRightParent.transform.childCount];

		for (int i = 0; i < PlayerHandRightChildren.Length; i++)
		{
			PlayerHandRightChildren[i] = PlayerHandRightParent.transform.GetChild(i).gameObject;
		}

		// Get all PlayerHandLeft children
		PlayerHandLeftChildren = new GameObject[PlayerHandLeftParent.transform.childCount];

		for (int i = 0; i < PlayerHandLeftChildren.Length; i++)
		{
			PlayerHandLeftChildren[i] = PlayerHandLeftParent.transform.GetChild(i).gameObject;
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

			if (weaponController.RightHandWeapon != null)
			{
				// In 1st person DO NOT show HandRight render and its children
				PlayerHandRightParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
				for (int i = 0; i < PlayerHandRightChildren.Length; i++)
				{
					PlayerHandRightChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
				}
			}
			else
			{
				PlayerHandRightParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				for (int i = 0; i < PlayerHandRightChildren.Length; i++)
				{
					PlayerHandRightChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				}
			}

			if (weaponController.LeftHandWeapon != null)
			{
				// In 1st person DO NOT show HandLeft render and its children
				PlayerHandLeftParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
				for (int i = 0; i < PlayerHandLeftChildren.Length; i++)
				{
					PlayerHandLeftChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
				}
			}
			else
			{
				PlayerHandLeftParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				for (int i = 0; i < PlayerHandLeftChildren.Length; i++)
				{
					PlayerHandLeftChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				}
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

			// In 3rd person show HandRight render and its children
			PlayerHandRightParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			for (int i = 0; i < PlayerHandRightChildren.Length; i++)
			{
				PlayerHandRightChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}

			// In 3rd person show HandLeft render and its children
			PlayerHandLeftParent.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			for (int i = 0; i < PlayerHandLeftChildren.Length; i++)
			{
				PlayerHandLeftChildren[i].GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}
		}
	}
}
