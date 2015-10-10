using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 10/10/2015
// Brief:  Designed to be combined with the AboveOneWayCollider script
//      to implement a one way platform.
//      The concept is that unless the player is in contact with the "above"   
//      trigger, then the collision is disabled.
//////////////////////////////////////////////////////////////////////

public class OneWayCollider : MonoBehaviour {

    [Tooltip("The collider that sits above to make collision only trigger from one direction ")]
    public AboveOneWayCollider m_AboveTrigger;

    // Stores the object whose collision we are going to ignore if they collide from the wrong direction.
    private Collider2D m_ColliderIgnoredObject;
    private Collider2D m_ColliderThisObject;

    private bool m_CollisionDisabled = false;

	// Use this for initialization
	void Start () {
        m_ColliderThisObject = this.gameObject.GetComponent<Collider2D>();
	}

	void Update () {
        // A Slightly hacky but much simpler implementation of the old code i was using
        // All it does is set the trigger to false while the collider above the platform
        // is not being triggered.
        if(m_AboveTrigger.isAbove == true){ m_ColliderThisObject.isTrigger = false; }
        else{ m_ColliderThisObject.isTrigger = true; }
    }


    // OLD CODE:
    // IN Update:
    // void Update(){
        //// If we trigger the "above" trigger again and weve already disabled collision, enable the collision
        //if (m_CollisionDisabled == true && m_AboveTrigger.isAbove == true) 
        //{
        //    m_CollisionDisabled = false;
        //    Physics2D.IgnoreCollision(m_ColliderIgnoredObject, m_ColliderThisObject, false);
        //}
    //}

    //void OnCollisionEnter2D(Collision2D a_CollisionInfo)
    //{
    //    if (!m_AboveTrigger.isAbove) // If they are not in contact with the "above" trigger
    //    {
    //        m_ColliderIgnoredObject = a_CollisionInfo.collider; // store the object so we can enable the collision again later.
    //        Physics2D.IgnoreCollision(m_ColliderIgnoredObject, m_ColliderThisObject);
    //        m_CollisionDisabled = true;
    //    }
    //}

}
