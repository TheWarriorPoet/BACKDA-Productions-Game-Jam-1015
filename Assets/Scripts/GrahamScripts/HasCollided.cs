using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 10/10/2015
// Brief:  Stores whether the object collided with the player or not
//      If it does collide with the player, it turns off its ability
//      to collide with the player via the Physics engine.
//      The effect is intended to allow the object to fall and hit terrain
//      under gravity, while still allowing it to behave like a trigger 
//      and allow the player to pass through it, without switching its layer.
//////////////////////////////////////////////////////////////////////

public class HasCollided : MonoBehaviour {

    private Collider2D m_AttachedCollider;

    private bool m_HasCollided = false;
    public bool hasCollided
    {
        get { return m_HasCollided; }
    }

    void Start()
    {
        m_AttachedCollider = this.gameObject.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D a_CollisionData)
    {
        // First check if weve already collided, in which case skip the tag search
        // Then see if the other object is the player
        if(m_HasCollided == false && a_CollisionData.gameObject.tag == "Player")
        {
            m_HasCollided = true;
            // Disables any further collision effects between this object and the player.
            Physics2D.IgnoreCollision(m_AttachedCollider, a_CollisionData.collider);
        }
    }
}
