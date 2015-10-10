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
		this.transform.SetParent(_platform.transform, true);
		this._rb = this.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		switch(this._type){
		case enemy_types.patrol:
		{
			this.move ();
			break;
		}
		case enemy_types.jump:
		{
			this.jump_vert(this._rb.velocity.y.Equals(0.0f));
			break;
		}
		case enemy_types.jump_move:
		{
			this.move();
			this.jump_vert(this._rb.velocity.y.Equals(0.0f) && this._last_left !=this. _going_left);
			break;
		}
		}
		this._last_left = this._going_left;
	}
	bool detect_ground()
	{
		Vector2 us = new Vector2(this.transform.position.x+(this._going_left ? 0.5f : -0.5f), this.transform.position.y);
		return (Physics2D.Raycast(us, new Vector2(us.x, us.y-2)).collider != null);
	}

	void move()
	{
		if(detect_ground())
			this.transform.position = new Vector2(this.transform.position.x + (this._going_left ? this._speed : -this._speed), this.transform.position.y);
		else 
			this._going_left = !this._going_left;
	}

	void jump_vert(bool state)
	{
		if(state)
			this._rb.AddForce(new Vector2(0.0f, this._jump_height), ForceMode2D.Impulse);
	}


}
