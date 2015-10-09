using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 09/10/2015
// Brief: Basic Control System for the character in the 2D Platformer
//      Includes a basic left/right movement and a jump mechanic
//////////////////////////////////////////////////////////////////////

public class Movement : MonoBehaviour {

    private bool m_LeftMoving = false;
    private bool m_RightMoving = false;

    void Update () {
        ProcessInput();
        MovementUpdate();
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
        }
        if(m_RightMoving)
        {
            // A Right force, made frame independant
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-600, 0, 0) * Time.deltaTime);
        }
    }
}
