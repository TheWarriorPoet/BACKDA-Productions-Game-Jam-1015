using UnityEngine;
using System.Collections;

public class camer_tracker : MonoBehaviour {
	public GameObject _player;
	private Vector3 _offset;
	// Use this for initialization
	void Start () {
		if(_player == null) Debug.LogError("Missing player reference @ camera");
		_offset = transform.position - _player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _player.transform.position + _offset;
	}
}
