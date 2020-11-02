using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlacement : MonoBehaviour
{
	GameMaster gameMaster;

	bool inputMode;

	public float DistanceTop;
	public float DistanceLaunch;

	private void Start()
	{
		gameMaster = transform.parent.gameObject.GetComponent<BallScript>().gameMaster;
	}

	private void Update()
	{
		if (gameMaster._inputMode)
			inputMode = true;
		else
			inputMode = false;

		if (inputMode)
			PlacementInputMode();
		else
		{
			PlacementLaunchMode();
		}

		transform.LookAt(transform.parent);
	}

	void PlacementInputMode()
	{
		transform.position = transform.parent.position + (gameMaster._gravityDirection * -DistanceTop);
	}

	void PlacementLaunchMode()
	{
		transform.position = transform.parent.position + (gameMaster._gravityPerpendicular * -DistanceLaunch);
	}
}
