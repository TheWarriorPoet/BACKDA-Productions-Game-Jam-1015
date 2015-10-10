using UnityEngine;
using System.Collections;

public class platform_spawner : MonoBehaviour
{
	public float _spawn_speed = 5;
	private float _timer = 0.0f;
	private object_pooler _pool;
	
	void Start()
	{
		_pool = GameObject.Find("object_pooler").GetComponent<object_pooler>();
	}
	
	void Update()
	{
		_timer += Time.deltaTime;
		if(_timer > _spawn_speed)
		{
			_timer = 0.0f;
			GameObject obj = _pool.get_object();
			if (obj == null) return;
			obj.transform.position = transform.position;
			obj.SetActive(true);
		}
	}
}