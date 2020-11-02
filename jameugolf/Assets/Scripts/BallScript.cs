using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
	public GameMaster gameMaster;
	Rigidbody _rb;
	[SerializeField] LayerMask _holeMask;
	[Range(.5f,1)] public float LimitRange;

	bool _cooldown;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	public void BallLaunch(Vector3 direction, float power)
	{
		StartCoroutine(Cooldown());
		_rb.AddForce(direction * power);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9)
		{
			gameMaster.Win();
		}
	}

	private void Update()
	{
		float speed = _rb.velocity.magnitude;
		if(speed <= LimitRange && !gameMaster._inputMode && !_cooldown)
		{
			_rb.velocity = Vector3.zero;
			gameMaster.ResetInputs();
		}
	}

	IEnumerator Cooldown()
	{
		_cooldown = true;
		yield return new WaitForSeconds(.5f);
		_cooldown = false;
	}
}
