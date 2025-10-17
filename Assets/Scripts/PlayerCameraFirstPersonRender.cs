using UnityEngine;
public class PlayerCameraFirstPersonRender : MonoBehaviour
{
	PlayerCamera playerCamera;
	WeaponController weaponController;
	public PlayerCameraStateType playerCameraStateType;

	public GameObject PlayerCameraObject;

	public GameObject PlayerHeadParent;
	public GameObject PlayerHandRightParent;
	public GameObject PlayerHandLeftParent;

	void Start()
	{
		playerCamera = PlayerCameraObject.GetComponent<PlayerCamera>();
		weaponController = GetComponent<WeaponController>();
	}

	void FixedUpdate()
	{
		if (playerCamera.IsPlayerCameraFirstPerson == true) 
		{
			HideMeshes(PlayerHeadParent);

			if (weaponController.RightHandWeapon != null)
			{
				HideMeshes(PlayerHandRightParent);
			}
			else
			{
				ShowMeshes(PlayerHandRightParent);
			}

			if (weaponController.LeftHandWeapon != null)
			{
				HideMeshes(PlayerHandLeftParent);
			}
			else
			{
				ShowMeshes(PlayerHandLeftParent);
			}

			if (weaponController.RightHandWeapon != null)
			{
				ShowMeshes(weaponController.RightHandWeapon.FirstPersonWeaponModelInstance);
				HideMeshes(weaponController.RightHandWeapon.ThirdPersonWeaponModelInstance);
			}

			if (weaponController.LeftHandWeapon != null)
			{
				ShowMeshes(weaponController.LeftHandWeapon.FirstPersonWeaponModelInstance);
				HideMeshes(weaponController.LeftHandWeapon.ThirdPersonWeaponModelInstance);
			}
		}

		else 
		{
			ShowMeshes(PlayerHeadParent);

			ShowMeshes(PlayerHandRightParent);
			ShowMeshes(PlayerHandLeftParent);

			if (weaponController.RightHandWeapon != null)
			{
				HideMeshes(weaponController.RightHandWeapon.FirstPersonWeaponModelInstance);
				ShowMeshes(weaponController.RightHandWeapon.ThirdPersonWeaponModelInstance);
			}

			if (weaponController.LeftHandWeapon != null)
			{
				HideMeshes(weaponController.LeftHandWeapon.FirstPersonWeaponModelInstance);
				ShowMeshes(weaponController.LeftHandWeapon.ThirdPersonWeaponModelInstance);
			}
		}
	}

	public void ShowMeshes(GameObject rootObj)
	{
		// Получаем все рендеры (включая дочерние объекты)
		Renderer[] renderers = rootObj.GetComponentsInChildren<Renderer>(true);

		// Перебираем все рендеры и включаем отбрасывание теней
		foreach (Renderer renderer in renderers)
		{
			if (renderer is MeshRenderer || renderer is SkinnedMeshRenderer)
			{
				renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}
		}
	}

	public void HideMeshes(GameObject rootObj)
	{
		// Получаем все рендеры (включая дочерние объекты)
		Renderer[] renderers = rootObj.GetComponentsInChildren<Renderer>(true);

		// Перебираем все рендеры и включаем отбрасывание теней
		foreach (Renderer renderer in renderers)
		{
			if (renderer is MeshRenderer || renderer is SkinnedMeshRenderer)
			{
				renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
		}
	}
}
