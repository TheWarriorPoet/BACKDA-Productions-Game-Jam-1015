using UnityEngine;
using System.Collections;

//redux of grahams script for 2d sprites

public class platform_mover_v2d : MonoBehaviour {

	public enum platform_types {still, horizontal, vertical};

	public float _speed;
	public float _max_motion;
	public platform_types _type = platform_types.still;

	private Vector2 _orig_pos;
	private bool _going_left = true;

	void Start () {
		_orig_pos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch(_type)
		{
		case platform_types.still: return;
		case platform_types.horizontal:
			if(transform.position.x > (_orig_pos.x + _max_motion) || transform.position.x < (_orig_pos.x - _max_motion))
				_going_left = !_going_left;
			transform.position = new Vector2((transform.position.x+(_going_left?_speed:-_speed)*Time.deltaTime), transform.position.y);
			break;
		case platform_types.vertical:
			if(transform.position.y > (_orig_pos.y + _max_motion) || transform.position.y < (_orig_pos.y - _max_motion))
				_going_left = !_going_left;
			transform.position = new Vector2(transform.position.x, (transform.position.y+(_going_left?_speed:-_speed)*Time.deltaTime));
			break;
		}
	}
}
