using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {


    void OnTriggerEnter(Collider a_CollisionInfo)
    {
        if(a_CollisionInfo.tag == "Player")
        {
            // Triger destruction of Pickup
            // Do global stats modification of whatever is needed for pickup Type
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
