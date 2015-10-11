using UnityEngine;
using System.Collections;

public class couch_collider : MonoBehaviour {

	public bool _load_couch = false;
	public bool _game_over = false;

	public Sprite _new;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag.Equals("Player"))
		{
			print ("here");
			//haahahh
			other.gameObject.GetComponent<Renderer>().enabled = false;
			other.gameObject.GetComponent<PlayerMovement>()._move = false;
			other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
			GetComponent<SpriteRenderer>().sprite = _new;
			_game_over = true;
		}
	}

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(_load_couch){
			_load_couch = false;
			GetComponent<SpriteRenderer>().enabled = true;
			GetComponent<BoxCollider2D>().enabled = true;
			GameObject o = GameObject.FindGameObjectWithTag("Player");
			var t = o.transform.position;
			t.x += 10;
			t.y += 5;
			transform.position = t;
		}
	}
}
