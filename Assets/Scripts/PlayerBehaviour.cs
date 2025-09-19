using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
	PlayerInputsList playerInputsList;

	private bool _isPlayerArmed;

	void Start()
	{
		playerInputsList = GetComponent<PlayerInputsList>();

		_isPlayerArmed = false;
	}

	void Update()
	{
		if (playerInputsList.GetKeyShowWeapons())
		{
			_isPlayerArmed = !_isPlayerArmed;
		}

		//Debug.Log(GetPlayerBehaviour());
		//Debug.Log("Is player armed " + _isPlayerArmed);
	}
	public int GetPlayerBehaviour()
	{
		if (_isPlayerArmed == true)
		{
			return 1;
		}
		else return 0;
	}

	// normal 0
	// armed 1
	// wanted 2
	// hostile 3

}
