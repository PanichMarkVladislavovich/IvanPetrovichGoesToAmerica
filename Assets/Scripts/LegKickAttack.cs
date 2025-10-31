using UnityEngine;

public class LegKickAttack : MonoBehaviour
{
    PlayerInputsList playerInputsList;
    PlayerMovementController playerMovementController;

    public bool IsPlayerLegKicking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInputsList = GetComponent<PlayerInputsList>();
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputsList.GetKeyLegKick() && IsPlayerLegKicking == false)
        {
            LegKick();
        }

    }


    public void LegKick()
    {
        IsPlayerLegKicking = true;

        playerMovementController.SetPlayerMovementState(PlayerMovementStateType.PlayerIdle);

		IsPlayerLegKicking = false;
	}
}
