using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFacingPlanet : MonoBehaviour
{
	GameMaster _gameMaster;
    void Start()
    {
		_gameMaster = transform.parent.gameObject.GetComponent<BallScript>().gameMaster;
    }

    // Update is called once per frame
    void Update()
    {
		transform.LookAt(_gameMaster.Levels[_gameMaster.CurrentLevel].position);
    }
}
