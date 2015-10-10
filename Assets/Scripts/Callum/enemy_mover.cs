using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class enemy_mover : MonoBehaviour {

	public enum enemy_types {patrol, jump, jump_move};

	public float _jump_height = 4.0f;
	public float _speed = 0.05f;
	public enemy_types _type = enemy_types.patrol;
	public GameObject _platform;	

	private bool _going_left = true;
	private bool _last_left = true;
	private Rigidbody2D _rb;

	void Start () {
		transform.SetParent(_platform.transform, true);
		_rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		switch(_type){
		case enemy_types.patrol:
		{
			move ();
			break;
		}
		case enemy_types.jump:
		{
			jump_vert(_rb.velocity.y.Equals(0.0f));
			break;
		}
		case enemy_types.jump_move:
		{
			move();
			jump_vert(_rb.velocity.y.Equals(0.0f) && _last_left != _going_left);
			break;
		}
		}
		_last_left = _going_left;
	}
	bool detect_ground()
	{
		Vector2 us = new Vector2(transform.position.x+(_going_left ? 0.5f : -0.5f), transform.position.y);
		return (Physics2D.Raycast(us, new Vector2(us.x, us.y-2)).collider != null);
	}

	void move()
	{
		if(detect_ground())
			transform.position = new Vector2((transform.position.x + (_going_left ? _speed : -_speed)*Time.deltaTime), transform.position.y);
		else 
			_going_left = !_going_left;
	}

	void jump_vert(bool state)
	{
		if(state)
			_rb.AddForce(new Vector2(0.0f, _jump_height), ForceMode2D.Impulse);
	}


}
