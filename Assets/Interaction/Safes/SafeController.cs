using UnityEngine;
using System.Collections;

public class SafeController : MonoBehaviour, IInteractable
{
	public GameObject SafeDoor;


	public GameObject SafeRotatorySection1;
	public GameObject SafeRotatorySection2;
	public GameObject SafeRotatorySection3;

	private float safeDoorOpeningSpeed = 100f;

	private bool wasSafeOpened;

	private Quaternion safeDoorOpenedRotation;       // Угловое положение открытой двери


	public string InteractionItemName => null;

	public string InteractionHint => "Открыть сейф";


	void Start()
	{
		// Настройка состояний вращения
		Vector3 openedEulerAngles = new Vector3(0, -90, 0);
		safeDoorOpenedRotation = Quaternion.Euler(openedEulerAngles);


	}
	
	public void Interact()
	{
		var section1 = SafeRotatorySection1.GetComponent<SafeRotatorySection>();
		var section2 = SafeRotatorySection2.GetComponent<SafeRotatorySection>();
		var section3 = SafeRotatorySection3.GetComponent<SafeRotatorySection>();

		//var safeDoor = SafeDoor.GetComponent<Transform>();

		if (wasSafeOpened == false)
		{
			if (section1 != null && section2 != null && section3 != null)
			{
				if (section1.IsSafeRotatorySectionPositionCorrect && section2.IsSafeRotatorySectionPositionCorrect
					&& section3.IsSafeRotatorySectionPositionCorrect)
				{
					Debug.Log("SAFE CORRECT");

					wasSafeOpened = true;

					if (SafeDoor != null)
					{
						StartCoroutine(OpenSafeDoor());
					}
				}
				else
				{
					Debug.Log("SAFE FAILED");
				}
			}
		}
	}

	IEnumerator OpenSafeDoor()
	{
		var safeDoor = SafeDoor.GetComponent<Transform>();

		while (Quaternion.Angle(safeDoor.transform.localRotation, safeDoorOpenedRotation) > 0.1f)
		{
			safeDoor.transform.localRotation = Quaternion.RotateTowards(safeDoor.transform.localRotation, safeDoorOpenedRotation, Time.deltaTime * safeDoorOpeningSpeed);
			yield return null;
		}

	}
}
