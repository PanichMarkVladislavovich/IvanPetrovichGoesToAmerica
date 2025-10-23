using UnityEngine;


[System.Serializable]



public class GameData
{
	
	//Player Movement
	public string CurrentPlayerMovementStateType;
	public Vector3 PlayerPosition;
	public Quaternion PlayerRotation;


	//Camera
	public string CurrentPlayerCameraStateType;
	public float PlayerCameraDistanceY;
	public float PlayerCameraDistanceZ;
	public Quaternion CameraRotation;
	public bool IsCameraShoulderRight;

	//PlayerMoney
	public int PlayerMoney;

	//Collectables
	public bool[] WasLootItemCollectedCoin5;       // Массив для золотых слитков
	public bool[] WasLootItemCollectedGoldBar;// Массив для пятирублёвых монет
	public bool[] WasLootItemCollectedHealingItem;   // Массив для лечебных предметов

	public GameData()
	{
		CurrentPlayerMovementStateType = "PlayerIdle";
		PlayerPosition = new Vector3(2, 0, 4);
		PlayerRotation = new Quaternion(0, 0, 0, 0);

		CurrentPlayerCameraStateType = "ThirdPerson";
		PlayerCameraDistanceY = -1.75f;
		PlayerCameraDistanceZ = 3.25f;
		CameraRotation = new Quaternion(0, 0, 0, 0);
		IsCameraShoulderRight = true;

		PlayerMoney = 0;

		WasLootItemCollectedCoin5 = new bool[10];
		WasLootItemCollectedGoldBar = new bool[10];
		WasLootItemCollectedHealingItem = new bool[10];
	}
}

