using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 10/10/2015
// Brief:  A Very Simple class, designed to be combined with the OneWayCollider
//      What this class stores is a bool that reports whether you have entered
//      or left its trigger.
//      The idea is, unless it returns true, you turn the collider off on
//      the one way object.
//      In effect, this means unless your colliding with both objects,
//      it will ignore the collision.
//////////////////////////////////////////////////////////////////////

public class AboveOneWayCollider : MonoBehaviour {

    private bool m_IsAbove;
    public bool isAbove
    {
        get { return m_IsAbove; } // Read only public access
    }

    void OnTriggerEnter2D()
    {
        m_IsAbove = true;
    }
    void OnTriggerExit2D()
    {
        m_IsAbove = false;
    }
}
