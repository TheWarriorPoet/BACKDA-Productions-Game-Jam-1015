using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 09/10/2015
// Brief: Basic Platform Moving Script
//      Moves the platform in one direction, them moves it reversing in 
//      the opposite direction then repeats
//      Direction, speed and distance is all customisable via public variables
// Changelog: AJ caching components for efficiency 
//////////////////////////////////////////////////////////////////////

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
    private Vector3 m_DirMovementReverse; // Calculated in Start

    // Cache components for efficiency
    private Transform m_platformTransform = null;
    private BoxCollider m_boxCollider = null;
    // giving other scripts access to whether the platform is moving
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
        m_platformTransform = transform;
        m_boxCollider = this.GetComponent<BoxCollider>();
        m_DirMovement.Normalize();
        m_DirMovementReverse = new Vector3(-m_DirMovement.x, -m_DirMovement.y,0);

        m_StartPosition = m_platformTransform.position;
        m_EndPosition = m_StartPosition + m_DirMovement * m_DistanceToMovement;
    }

	void Update () {
        if (m_MovingForward)
        {
            // Frame Independant movement in the direction set by public variables.
            m_platformTransform.position += (m_DirMovement * m_MoveSpeed) * Time.deltaTime;
            // If it reaches the end position
            if (m_boxCollider.bounds.Contains(m_EndPosition))
            {
                m_MovingForward = false;
                m_MovingReverse = true;
            }
        }
        else if (m_MovingReverse)
        {
            // Frame Independant movement in the reverse direction to the direction set by public variables
            m_platformTransform.position += (m_DirMovementReverse * m_MoveSpeed) * Time.deltaTime;
            // If it reaches the start position
            if (m_boxCollider.bounds.Contains(m_StartPosition))
            {
                m_MovingReverse = false;
                m_MovingForward = true;
            }
        }


    }
}
