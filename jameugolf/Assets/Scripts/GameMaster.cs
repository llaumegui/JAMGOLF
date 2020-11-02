using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{


	public Transform Level;

	public Transform Ball;
	public Transform BallEmptyLookat;
	BallScript _ballScript;

	[SerializeField] Transform _powerJauge;
	Vector3 _baseJaugePosition;
	public AnimationCurve Curve;

	public Vector3 _gravityDirection;
	public Vector3 _gravityPerpendicular;
	float _gravityIntensity;
	bool _canUpdateGravity;

	public float Power;
	public Vector2 DirectionAim;
	Vector3 Direction;

	public bool _inputMode;
	bool _buttonPressed;
	bool _ballPowerLaunched;
	bool _shooted;

	public KeyCode[] Numpads;

	[Header("BallPower")]
	public float TimeLerp;
	float _interpolator;
	float _currentValue;
	bool _positiveCharge = true;
	[Range(0,1)] public float Height;
	[Range(100,1000)] public float PowerValue;

	private void Start()
	{
		_baseJaugePosition = _powerJauge.position;
		BallEmptyLookat = Ball.transform.GetChild(0);

		ResetInputs();
		SetLevel();
	}

	private void Update()
	{
		if (_inputMode)
			Inputs();
	}

	private void FixedUpdate()
	{
		if (_canUpdateGravity)
			UpdateGravity();
	}

	void Inputs()
	{
		bool pressed = false;
		foreach(KeyCode key in Numpads)
		{
			if(Input.GetKey(key))
			{
				pressed = true;
				DirectionAim = SetDirection(key);
			}
		}
		if (!pressed)
			_buttonPressed = false;
		else
			_buttonPressed = true;


		if (_buttonPressed)
			BallPower();
		if (!_buttonPressed && _ballPowerLaunched)
			Launch();

	}

	void BallPower()
	{
		_ballPowerLaunched = true;

		if (_positiveCharge)
			_currentValue = Mathf.Lerp(0, 1, _interpolator);
		else
			_currentValue = Mathf.Lerp(1, 0, _interpolator);


		_interpolator += Time.deltaTime / TimeLerp;
		if(_interpolator >= 1)
		{
			_interpolator = 0;
			_positiveCharge = !_positiveCharge;
		}

		_powerJauge.position = _baseJaugePosition + (Vector3.up * _currentValue * 10/2);
		_powerJauge.localScale = new Vector3(_powerJauge.localScale.x, (10 * _currentValue),_powerJauge.localScale.z);

	}

	public void ResetInputs()
	{
		_inputMode = true;
		_positiveCharge = true;
		_buttonPressed = false;
		_ballPowerLaunched = false;
		_interpolator = 0;
		Power = 0;
		_shooted = false;
	}

	void SetLevel()
	{
		LevelData levelData = Level.GetChild(0).GetComponent<LevelData>();
		_gravityIntensity = levelData.GravityPower;

		_canUpdateGravity = true;
	}

	void UpdateGravity()
	{
		_gravityDirection = (Level.position - Ball.position).normalized;
		_gravityPerpendicular = Vector3.Cross(_gravityDirection, Vector3.forward);

		Physics.gravity = _gravityDirection * _gravityIntensity;
	}

	Vector2 SetDirection(KeyCode key)
	{
		if(key == KeyCode.Keypad2)
		{
			return Vector2.down;
		}
		if (key == KeyCode.Keypad1)
		{
			return new Vector2(-1,-1);
		}
		if (key == KeyCode.Keypad3)
		{
			return new Vector2(1, -1);
		}
		if (key == KeyCode.Keypad4)
		{
			return Vector2.left;
		}
		if (key == KeyCode.Keypad6)
		{
			return Vector2.right;
		}
		if (key == KeyCode.Keypad7)
		{
			return new Vector2(-1,1);
		}
		if (key == KeyCode.Keypad8)
		{
			return Vector2.up;
		}
		if (key == KeyCode.Keypad9)
		{
			return Vector2.one;
		}

		return Vector2.zero;
	}

	void Launch()
	{
		if(!_shooted)
		{
			_shooted = true;


			Power = Curve.Evaluate(_currentValue) * PowerValue;
			Direction = BallEmptyLookat.TransformDirection(DirectionAim.x, DirectionAim.y, -Height);


			Debug.Log("tir");
			Debug.Log(Direction + " | " + Power);

			_ballScript = Ball.transform.gameObject.GetComponent<BallScript>();
			_ballScript.BallLaunch(Direction, Power);

			_inputMode = false;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(Ball.position, _gravityDirection);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Level.position, _gravityPerpendicular);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(Ball.position, Direction);

	}

	public IEnumerator Win()
	{
		yield return new WaitForSeconds(1);
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		else
			SceneManager.LoadScene(0);

	}

}
