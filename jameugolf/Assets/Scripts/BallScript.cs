using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	public GameMaster gameMaster;
	Rigidbody _rb;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	public void BallLaunch(Vector3 direction, float power)
	{
		_rb.AddForce(direction.normalized * power);
	}
}
