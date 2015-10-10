using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class enemy_mover : MonoBehaviour {

	public enum enemy_types {patrol, jump, jump_move};

	public float _jump_height = 4.0f;
	public float _speed = 1;
	public enemy_types _type = enemy_types.patrol;
	public GameObject _platform;	

	private bool _going_left = true;
	private bool _just_switched = false;
	private Rigidbody2D _rb;

	void Start () {
		transform.SetParent(_platform.transform, true);
		_rb = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter(Collision other)
	{
		_rb.gravityScale = 0;
		print (other.gameObject.name);
	}

	void OnCollisionExit(Collision other)
	{
		_rb.gravityScale = 1;
	}

	void FixedUpdate () {
		_just_switched = false;
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
			jump_vert(_just_switched);
			break;
		}
		}
	}
	bool detect_ground()
	{
		//fucking horrible
		Vector2 us = new Vector2(transform.position.x+(_going_left ?1.5f:-1.5f), transform.position.y-0.5f);
		//Debug.DrawRay(us, new Vector2((_going_left ? 1 : -1), -1).normalized, Color.green);
		return(Physics2D.Raycast(us, new Vector2((_going_left ? -1 : 1), -1).normalized, 5, LayerMask.GetMask("Platform")).collider != null);
	}

	void move()
	{
		if(detect_ground())
			transform.position = new Vector2((transform.position.x + (_going_left ? _speed : -_speed)*Time.deltaTime), transform.position.y);
		else {
			_going_left = !_going_left;
			_just_switched = true;
			flip ();
		}
	}

	void jump_vert(bool state)
	{
		if(state)
			_rb.AddForce(new Vector2(0.0f, _jump_height), ForceMode2D.Impulse);
	}

	void flip()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

}
