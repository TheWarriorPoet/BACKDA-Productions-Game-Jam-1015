using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    // Update is called once per frame

    private bool m_LeftMoving = false;
    private bool m_RightMoving = false;
    private bool m_UpMoving = false;
    private bool m_DownMoving = false;

    void Update () {
        ProcessInput();
        MovementUpdate();
    }

    void ProcessInput()
    {
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

        if(Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400, 0));
            //m_UpMoving = true;
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            //m_UpMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_DownMoving = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_DownMoving = false;
        }
    }

    void MovementUpdate()
    {
        if (m_LeftMoving)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(10, 0, 0));
        }
        if(m_RightMoving)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-10, 0, 0));
        }
        //if(m_UpMoving)
        //{
        //    this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
        //}
        //if(m_DownMoving)
        //{
        //    this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -10, 0));
        //}
    }
}
