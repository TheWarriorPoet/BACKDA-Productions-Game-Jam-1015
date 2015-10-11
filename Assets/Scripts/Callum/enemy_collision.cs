using UnityEngine;
using System.Collections;

public class enemy_collision : MonoBehaviour {

	public float enemy_1_damage = 3;
	public float enemy_2_damage = 5;

	private SceneManager_MainGame _scene_manage;

	void Start()
	{
		_scene_manage = GameObject.Find("SceneManager").GetComponent<SceneManager_MainGame>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player"){
			if (this.tag == "Enemy1")
				_scene_manage.playerHealth -= (int)((enemy_1_damage/100)*_scene_manage.maxHealth);
			else if (this.tag == "Enemy2")
				_scene_manage.playerHealth -= (int)((enemy_2_damage/100)*_scene_manage.maxHealth);
		}
	}
}
