using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;

    public float m_DistanceToMovement = 5.0f;

    private bool m_MovingUp = true;
    private bool m_MovingDown = false;

    public float m_MoveSpeed = 1.0f;

    private Vector3 m_UpDir = new Vector3(0, 1, 0);
    private Vector3 m_DownDir = new Vector3(0, -1, 0);

    public bool isMovingDown
    {
        get { return m_MovingDown; }
    }
    public bool isMovingUp
    {
        get { return m_MovingUp; }
    }

    void Start()
    {
        m_StartPosition = this.gameObject.transform.position;
        m_EndPosition = m_StartPosition + new Vector3(0, m_DistanceToMovement, 0);
    }

	void Update () {
        if (m_MovingUp)
        {
            this.gameObject.transform.position += (m_UpDir * m_MoveSpeed)*Time.deltaTime;

            if (this.gameObject.GetComponent<BoxCollider>().bounds.Contains(m_EndPosition))
            {
                m_MovingUp = false;
                m_MovingDown = true;
            }
        }
        else if (m_MovingDown)
        {
            this.gameObject.transform.position += (m_DownDir * m_MoveSpeed) * Time.deltaTime;
            //if reached desitination m_MovingUp = false;
            if (this.gameObject.GetComponent<BoxCollider>().bounds.Contains(m_StartPosition))
            {
                m_MovingDown = false;
                m_MovingUp = true;
            }
        }


    }
}
