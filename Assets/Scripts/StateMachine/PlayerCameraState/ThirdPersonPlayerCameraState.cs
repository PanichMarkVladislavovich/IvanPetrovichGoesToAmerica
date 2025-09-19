using UnityEngine;
public class ThirdPersonPlayerCameraState : PlayerCameraState
{
	public ThirdPersonPlayerCameraState(PlayerCamera playerCam)
	{
		playerCamera = playerCam;
		Debug.Log("Entered 3rd Person Camera");
		playerCamera.SetPlayerCameraType(PlayerCameraStateType.ThirdPerson);
	}
	public override void ChangePlayerCameraView()
	{
		playerCamera.SetPlayerCameraState(PlayerCameraStateType.FirstPerson);
	}
	public override void PlayerCameraPosition()
	{
		playerCamera.ThirdPersonCameraTransform();
	}




	public override void EnterCutscene()
	{
		playerCamera.SetPlayerCameraState(PlayerCameraStateType.Cutscene);
		playerCamera.SetPlayerCameraType(PlayerCameraStateType.Cutscene);
	}
}
