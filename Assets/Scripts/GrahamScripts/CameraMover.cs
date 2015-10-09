using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author:  Graham Johnston
// Date Created: 09/10/2015
// Brief: Tracks the Camera to the movement of the object given as a 
//      public variable
//////////////////////////////////////////////////////////////////////

public class CameraMover : MonoBehaviour {

    public Transform m_Target;

    // Called after all updates are called
    // Specifically reccomended for camera movement
    void LateUpdate()
    {
        if(m_Target == null){ return; } // Break if we have no target

        // Gets the X/Y co-ords from the target
        // Keep the Z position of the camera that already exists.
        Vector3 targetVector = new Vector3(m_Target.transform.position.x, 
            m_Target.transform.position.y, 
            this.gameObject.transform.position.z);
        // Applies them to the camera;
        this.gameObject.transform.position = targetVector;
    }

}
