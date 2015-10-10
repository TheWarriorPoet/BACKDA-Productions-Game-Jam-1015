using UnityEngine;
using System.Collections;

public class player_mover : MonoBehaviour {
	public float _jump_height = 7;
	public float _speed = 1;

	private Animator _animator;
	private Rigidbody2D _rigid_body;
	private bool _facing_left = false;

	public Transform _ground_checker;
	public bool _is_grounded = false;

	bool _jumping = false;
	bool _jump_1 = false;
	bool _jump_2 = false;

	void Start () {
		_animator = GetComponent<Animator>();
		_rigid_body = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		_is_grounded = Physics2D.OverlapCircle(_ground_checker.position, 1.0f, (1 << 10) | (1 << 11) | (1 << 12));
		Physics2D.IgnoreLayerCollision(9, 12, !_is_grounded || _rigid_body.velocity.y > 0);
	}
	
	// Update is called once per frame
	void Update () {
		string to_play = (_jumping ? "Kid_jumping" : "Kid_idle");
		if(_rigid_body.velocity.y == 0.0f) _jump_1 = _jump_2 = _jumping = false;
		if(Input.GetKey(KeyCode.D))
		{
			flip(_facing_left == false);
			transform.position = new Vector3(transform.position.x + (_speed * Time.deltaTime), transform.position.y, transform.position.z);
			if(!_jumping)to_play = "Kid_running";
		}
		else if(Input.GetKey (KeyCode.A))
		{
			flip (_facing_left == true);
			transform.position = new Vector3(transform.position.x + (-_speed * Time.deltaTime), transform.position.y, transform.position.z);
			if(!_jumping)to_play = "Kid_running";
		}
		if(Input.GetKeyDown (KeyCode.W))
		{
			if(!_jump_1 || !_jump_2){
				_rigid_body.AddForce(new Vector3(0, _jump_height, 0), ForceMode2D.Impulse);
				to_play = "Kid_jumping";
				_jumping = true;
			}
			_jump_2 = _jump_2 ? _jump_2 : _jump_1;
			_jump_1 = true;
		}
		_animator.Play(to_play);
	}

	void flip(bool state)
	{
		if (state) return;
		_facing_left = !_facing_left;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}	
}
