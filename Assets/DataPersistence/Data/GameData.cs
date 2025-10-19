using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]



public class GameData
{
	
	//Player Movement
	public string CurrentPlayerMovementStateType;
	public Vector3 PlayerPosition;
	public Quaternion PlayerRotation;


	//Camera
	public string CurrentPlayerCameraStateType;
	public Quaternion CameraRotation;
	public bool IsCameraShoulderRight;


	public GameData()
	{
		CurrentPlayerMovementStateType = "PlayerIdle";
		PlayerPosition = new Vector3(2, 0, 4);
		PlayerRotation = new Quaternion(0, 0, 0, 0);

		CurrentPlayerCameraStateType = "ThirdPerson";
		CameraRotation = new Quaternion(0, 0, 0, 0);
		IsCameraShoulderRight = true;
		
	}
}

