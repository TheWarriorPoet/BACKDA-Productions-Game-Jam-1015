using UnityEngine;
using System.Collections;


public class enemy_mover_2 : MonoBehaviour {
	public float _speed = 1;
	private bool _going_left = true;
	public Transform _trans;
	
	void Start () {
	}
	
	void FixedUpdate () {
		bool col = Physics2D.OverlapCircle(_trans.position, 0.1f, (1 << 11));
		if(!col){
			flip();
			_going_left = !_going_left;
		}

		transform.position = new Vector2((transform.position.x + (_going_left ? _speed : -_speed)*Time.deltaTime), transform.position.y);
	}
	
	void flip()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	
}
