using UnityEngine;
using System.Collections;

public class enemy_mover : MonoBehaviour {

	public float _speed = 1.0f;
	public GameObject _platform;
	

	private bool _going_left = true;

	void Start () {
		this.transform.SetParent(_platform.transform, true);
	}

	void FixedUpdate () {
		if(detect_ground())
				this.transform.position = new Vector2(this.transform.position.x + (_going_left ? _speed : -_speed), this.transform.position.y);
		else 
			_going_left = !_going_left;
	}

	bool detect_ground()
	{
		Vector2 us = new Vector2(this.transform.position.x+(_going_left ? 0.5f : -0.5f), this.transform.position.y);
		return (Physics2D.Raycast(us, new Vector2(us.x, us.y-2)).collider != null);
	}
}
