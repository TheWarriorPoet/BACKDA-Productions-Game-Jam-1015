using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;

    [Tooltip("The Speed at which the platform moves up/down")]
    public float m_MoveSpeed = 1.0f;
    [Tooltip("The Distance from its current position it moves")]
    public float m_DistanceToMovement = 5.0f;

    private bool m_MovingForward = true;
    private bool m_MovingReverse = false;

    [Tooltip("The Direction the platform moves, will be normalised at runtime")]
    public Vector3 m_DirMovement = new Vector3(0, 1, 0);
    private Vector3 m_DirMovementReverse;

    public bool isMovingForward
    {
        get { return m_MovingForward; }
    }
    public bool isMovingReverse
    {
        get { return m_MovingReverse; }
    }

    void Start()
    {
        m_DirMovement.Normalize();
        m_DirMovementReverse = new Vector3(-m_DirMovement.x, -m_DirMovement.y,0);

        m_StartPosition = this.gameObject.transform.position;
        m_EndPosition = m_StartPosition + m_DirMovement * m_DistanceToMovement;
    }

	void Update () {
        if (m_MovingForward)
        {
            this.gameObject.transform.position += (m_DirMovement * m_MoveSpeed)*Time.deltaTime;
            // If it reaches the end position
            if (this.gameObject.GetComponent<BoxCollider>().bounds.Contains(m_EndPosition))
            {
                m_MovingForward = false;
                m_MovingReverse = true;
            }
        }
        else if (m_MovingReverse)
        {
            this.gameObject.transform.position += (m_DirMovementReverse * m_MoveSpeed) * Time.deltaTime;
            // If it reaches the start position
            if (this.gameObject.GetComponent<BoxCollider>().bounds.Contains(m_StartPosition))
            {
                m_MovingReverse = false;
                m_MovingForward = true;
            }
        }


    }
}
