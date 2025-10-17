using UnityEngine;

public abstract class WeaponClass : MonoBehaviour
{
	public string WeaponNameSystem;
	public string WeaponNameUI;
	public float WeaponDamage;

	public GameObject weaponModel; // Ссылка на 3D модель оружия
	public GameObject FirstPersonWeaponModelInstance; // Ссылка на инстанцированную модель
	public GameObject ThirdPersonWeaponModelInstance; // Ссылка на инстанцированную модель

	public MeshRenderer FirstPersonWeaponMeshRenderer;
	public MeshRenderer ThirdPersonWeaponMeshRenderer;

	// Теперь слот для рук задаётся через инспектор
	public GameObject LeftHandWeaponSlot; // Левый слот (кость руки)
	public GameObject RightHandWeaponSlot; // Правый слот (кость руки)


	public Transform LeftHandWeaponSlotTransform; // Левый слот (кость руки)
	public Transform RightHandWeaponSlotTransform; // Правый слот (кость руки)

	public virtual void WeaponAttack()
	{
		// 4 weapon classes override this method
	}

	

	public void InstantiateWeaponModel(string handType)
	{
		if (weaponModel != null)
		{
			FirstPersonWeaponModelInstance = Instantiate(weaponModel);
			ThirdPersonWeaponModelInstance = Instantiate(weaponModel);
			FirstPersonWeaponMeshRenderer = FirstPersonWeaponModelInstance.GetComponent<MeshRenderer>();
			ThirdPersonWeaponMeshRenderer = ThirdPersonWeaponModelInstance.GetComponent<MeshRenderer>();
			FirstPersonWeaponModelInstance.transform.parent = transform;
			//currentModelInstance.transform.parent = transform;
			if (handType == "left")
			{
				LeftHandWeaponSlotTransform = GameObject.Find("Slot.L").transform;
				ThirdPersonWeaponModelInstance.transform.SetParent(LeftHandWeaponSlotTransform, true);
				FirstPersonWeaponModelInstance.transform.localPosition = new Vector3(-0.35f, 1.25f, 0.5f);
				//FirstPersonlModelInstance.transform.SetParent
				//currentModelInstance.transform.localPosition = new Vector3(-0.35f, 1.75f, 0.5f); // Локальная позиция для левой руки
			}
			else if(handType == "right")
			{

				LeftHandWeaponSlotTransform = GameObject.Find("Slot.R").transform;
				ThirdPersonWeaponModelInstance.transform.SetParent(LeftHandWeaponSlotTransform, true);
				FirstPersonWeaponModelInstance.transform.localPosition = new Vector3(0.35f, 1.25f, 0.5f);
				//currentModelInstance.transform.localPosition = new Vector3(0.35f, 1.75f, 0.5f); // Локальная позиция для правой руки
			}
			// Обнуляем локальную позицию и ориентацию
			ThirdPersonWeaponModelInstance.transform.localPosition = Vector3.zero;
			ThirdPersonWeaponModelInstance.transform.localRotation = Quaternion.identity;
		}
	}

	public void DestroyWeaponModel()
	{
		if (ThirdPersonWeaponModelInstance != null)
		{
			Destroy(ThirdPersonWeaponModelInstance);
			ThirdPersonWeaponModelInstance = null;
		}
		if (FirstPersonWeaponModelInstance != null)
		{
			Destroy(FirstPersonWeaponModelInstance);
			FirstPersonWeaponModelInstance = null;
		}
	}
}