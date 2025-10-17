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
	public GameObject ThirdPersonLeftHandWeaponSlot; // Левый слот (кость руки)
	public GameObject ThirdRightHandWeaponSlot; // Правый слот (кость руки)
	public Transform ThirdLeftHandWeaponSlotTransform; // Левый слот (кость руки)
	public Transform ThirdRightHandWeaponSlotTransform; // Правый слот (кость руки)

	// Теперь слот для рук задаётся через инспектор
	public GameObject FirstPersonLeftHandWeaponSlot; // Левый слот (кость руки)
	public GameObject FirstRightHandWeaponSlot; // Правый слот (кость руки)


	public Transform FirstLeftHandWeaponSlotTransform; // Левый слот (кость руки)
	public Transform FirstRightHandWeaponSlotTransform; // Правый слот (кость руки)

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
				ThirdLeftHandWeaponSlotTransform = GameObject.Find("Slot.L").transform;
				ThirdPersonWeaponModelInstance.transform.SetParent(ThirdLeftHandWeaponSlotTransform, true);

				FirstLeftHandWeaponSlotTransform = GameObject.Find("Slot1.L").transform;
				FirstPersonWeaponModelInstance.transform.SetParent(FirstLeftHandWeaponSlotTransform, true);
				//FirstPersonWeaponModelInstance.transform.localPosition = new Vector3(-0.35f, 1.25f, 0.5f);
				//FirstPersonlModelInstance.transform.SetParent
				//currentModelInstance.transform.localPosition = new Vector3(-0.35f, 1.75f, 0.5f); // Локальная позиция для левой руки
			}
			else if(handType == "right")
			{

				ThirdLeftHandWeaponSlotTransform = GameObject.Find("Slot.R").transform;
				ThirdPersonWeaponModelInstance.transform.SetParent(ThirdLeftHandWeaponSlotTransform, true);

				FirstLeftHandWeaponSlotTransform = GameObject.Find("Slot1.R").transform;
				FirstPersonWeaponModelInstance.transform.SetParent(FirstLeftHandWeaponSlotTransform, true);
				//FirstPersonWeaponModelInstance.transform.localPosition = new Vector3(0.35f, 1.25f, 0.5f);
				//currentModelInstance.transform.localPosition = new Vector3(0.35f, 1.75f, 0.5f); // Локальная позиция для правой руки
			}
			// Обнуляем локальную позицию и ориентацию
			FirstPersonWeaponModelInstance.transform.localPosition = Vector3.zero;
			FirstPersonWeaponModelInstance.transform.localRotation = Quaternion.identity;
			//FirstPersonWeaponModelInstance.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);

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