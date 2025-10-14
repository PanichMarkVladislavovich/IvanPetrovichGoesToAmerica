using UnityEngine;

public abstract class WeaponClass : MonoBehaviour
{
	public string WeaponNameSystem;
	public string WeaponNameUI;
	public float WeaponDamage;
	public GameObject weaponModel; // Ссылка на 3D модель оружия
	public GameObject currentModelInstance; // Ссылка на инстанцированную модель
	public MeshRenderer weaponMeshRenderer;

	// Теперь слот для рук задаётся через инспектор
	public GameObject LeftHandWeaponSlot; // Левый слот (кость руки)
	public GameObject RightHandWeaponSlot; // Правый слот (кость руки)


	public Transform LeftHandWeaponSlotTransform; // Левый слот (кость руки)
	public Transform RightHandWeaponSlotTransform; // Правый слот (кость руки)

	public virtual void WeaponAttack()
	{
		// 4 weapon classes override this method
	}

	public void Start()
	{
		/*
		Debug.Log(LeftHandWeaponSlot);
		LeftHandWeaponSlot = GameObject.Find("Slot.L");
		LeftHandWeaponSlotTransform = LeftHandWeaponSlot.transform;
		Debug.Log(LeftHandWeaponSlot);
		*/

		//LeftHandWeaponSlotTransform = GameObject.Find("Slot.L").transform;
	}

	public void InstantiateWeaponModel(string handType)
	{
		if (weaponModel != null)
		{
			currentModelInstance = Instantiate(weaponModel);
			weaponMeshRenderer = currentModelInstance.GetComponent<MeshRenderer>();
			//currentModelInstance.transform.parent = transform;
			if (handType == "left")
			{
				LeftHandWeaponSlotTransform = GameObject.Find("Slot.L").transform;
				currentModelInstance.transform.SetParent(LeftHandWeaponSlotTransform, true);
				
				//currentModelInstance.transform.localPosition = new Vector3(-0.35f, 1.75f, 0.5f); // Локальная позиция для левой руки
			}
			else if(handType == "right")
			{

				LeftHandWeaponSlotTransform = GameObject.Find("Slot.R").transform;
				currentModelInstance.transform.SetParent(LeftHandWeaponSlotTransform, true);
				//currentModelInstance.transform.localPosition = new Vector3(0.35f, 1.75f, 0.5f); // Локальная позиция для правой руки
			}
			// Обнуляем локальную позицию и ориентацию
			currentModelInstance.transform.localPosition = Vector3.zero;
			currentModelInstance.transform.localRotation = Quaternion.identity;
		}
	}

	public void DestroyWeaponModel()
	{
		if (currentModelInstance != null)
		{
			Destroy(currentModelInstance);
			currentModelInstance = null;
		}
	}
}