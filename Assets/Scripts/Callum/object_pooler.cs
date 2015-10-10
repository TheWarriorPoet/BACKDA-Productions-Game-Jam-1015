using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class object_pooler : MonoBehaviour
{
	public GameObject _obj;
	public int _pool_size = 5;	
	private List<GameObject> _pooled_objs = new List<GameObject>();

	void Start()
	{
		for (int i = 0; i < _pool_size; ++i)
		{
			GameObject obj = (GameObject)Instantiate(_obj);
			obj.SetActive(false);
			_pooled_objs.Add(obj);
		}
	}
	
	public GameObject get_object()
	{
		for (int i = 0; i < _pooled_objs.Count; ++i)
			if (!_pooled_objs[i].activeInHierarchy)
				return _pooled_objs[i];

		GameObject obj = (GameObject)Instantiate(_obj);
		_pooled_objs.Add(obj);
		return obj;
	}
}
