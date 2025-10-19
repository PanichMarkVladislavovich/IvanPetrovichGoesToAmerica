using UnityEngine;

[System.Serializable]



public class GameData
{
	public bool IsCameraShoulderRight;
	public string CurrentPlayerMovementStateType;
	public Vector3 PlayerTransform;

	public GameData()
	{
		CurrentPlayerMovementStateType = "PlayerIdle";

		IsCameraShoulderRight = true;

		PlayerTransform = new Vector3(2, 0, 4);
	}


	
}

