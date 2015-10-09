using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 09/10/2015
// Brief: Basic Control System for the character in the 2D Platformer
//      Includes a basic left/right movement and a jump mechanic
//      Includes an implementation to make the user move with the platform
//////////////////////////////////////////////////////////////////////

public class Movement : MonoBehaviour {

    private bool m_LeftMoving = false;
    private bool m_RightMoving = false;

    private Transform m_ParentTransform;

    void Start()
    {
        m_ParentTransform = this.gameObject.transform.parent;
    }

    void Update () {
        ProcessInput();
        MovementUpdate();
    }


    void OnCollisionStay(Collision a_CollisionInfo)
    {
        // When the Player is setting on a moving platform, its position relative to the platform will stay
        // until it is no longer in contact.
        if(a_CollisionInfo.gameObject.tag == "Platform")
        {
            this.gameObject.transform.SetParent(a_CollisionInfo.gameObject.transform,true);
        }
        else
        {
            this.gameObject.transform.SetParent(m_ParentTransform, true);
        }
    }

    void ProcessInput()
    {
        // Essentially sets a flag to true whenever the key is pressed and not yet released
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_RightMoving = true;
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            m_RightMoving = false;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            m_LeftMoving = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_LeftMoving = false;
        }

        // Triggers the jump force when the up key is pressed
        if(Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
        }
    }

    void MovementUpdate()
    {
        // Essentially applies a constant force while the flags are true, which happens
        //  while the key is held down
        if (m_LeftMoving)
        {
            // A Left force, made frame independant
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(600, 1, 0)* Time.deltaTime);
            Camera.main.transform.position += this.gameObject.GetComponent<Rigidbody>().velocity;
        }
        if(m_RightMoving)
        {
            // A Right force, made frame independant
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-600, 0, 0) * Time.deltaTime);
            Camera.main.transform.position += this.gameObject.GetComponent<Rigidbody>().velocity;
        }
    }
}
