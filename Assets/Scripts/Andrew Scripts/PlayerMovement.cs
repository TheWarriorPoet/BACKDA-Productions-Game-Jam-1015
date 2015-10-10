using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 09/10/2015
// Brief: Basic Control System for the character in the 2D Platformer
//      Includes a basic left/right movement and a jump mechanic
//      Includes an implementation to make the user move with the platform
// Changelog: Adjusting movement AJ
//////////////////////////////////////////////////////////////////////

public class PlayerMovement : MonoBehaviour
{
    private bool m_Idle = true;
    private bool m_facingRight = true;
    private bool m_LeftMoving = false;
    private bool m_RightMoving = false;
    private bool m_Jumping = false;
    private bool m_firstJump = false;
    private bool m_doubleJump = false;

    private Transform m_ParentTransform;
    private Transform m_PlayerTransform;
    private Rigidbody2D m_PlayerRigidbody2D;
    private Animator m_PlayerAnimator;

    public Transform m_CameraTransform;
    public float m_Speed;

    void Start()
    {
        m_ParentTransform = this.gameObject.transform.parent;
        m_PlayerTransform = transform;
        m_PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInput();
        MovementUpdate();
    }


    void OnCollisionEnter2D(Collision2D a_CollisionInfo)
    {
        // When the Player is setting on a moving platform, its position relative to the platform will stay
        // until it is no longer in contact.
        if (a_CollisionInfo.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            m_PlayerTransform.SetParent(a_CollisionInfo.gameObject.transform, true);
            if (m_Jumping == true)
            {
                m_Jumping = false;
                m_firstJump = false;
                m_doubleJump = false;
            }
        }
        else if (a_CollisionInfo.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_PlayerTransform.SetParent(m_ParentTransform, true);
            if (m_Jumping == true)
            {
                m_Jumping = false;
                m_firstJump = false;
                m_doubleJump = false;
            }
        }
    }

    void ProcessInput()
    {
        // Essentially sets a flag to true whenever the key is pressed and not yet released
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_facingRight = true;
            m_RightMoving = true;
            m_Idle = false;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_RightMoving = false;
            m_Idle = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            m_facingRight = false;
            m_LeftMoving = true;
            m_Idle = false;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_LeftMoving = false;
            m_Idle = true;
        }

        // Triggers the jump force when the up key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (m_firstJump == false)
            {
                m_Jumping = true;
                m_PlayerRigidbody2D.AddForce(new Vector3(0, 400, 0));
                m_firstJump = true;
            }
            else if (m_doubleJump == false)
            {
                m_PlayerRigidbody2D.AddForce(new Vector3(0, 400, 0));
                m_doubleJump = true;
            }
        }
    }

    void MovementUpdate()
    {
        // Essentially applies a constant force while the flags are true, which happens
        //  while the key is held down
        if (m_LeftMoving)
        {
            // A Left force, made frame independant
            //m_PlayerRigidbody2D.AddForce(new Vector3(600, 1, 0)* Time.deltaTime);
            m_PlayerTransform.position = new Vector3(m_PlayerTransform.position.x - (m_Speed * Time.deltaTime), m_PlayerTransform.position.y, m_PlayerTransform.position.z);
            if (!m_Jumping)
                m_PlayerAnimator.Play("Kid_running_left");
        }
        else if (m_RightMoving)
        {
            // A Right force, made frame independant
            //m_PlayerRigidbody2D.AddForce(new Vector3(-600, 0, 0) * Time.deltaTime);
            m_PlayerTransform.position = new Vector3(m_PlayerTransform.position.x + (m_Speed * Time.deltaTime), m_PlayerTransform.position.y, m_PlayerTransform.position.z);
            if (!m_Jumping)
                m_PlayerAnimator.Play("Kid_running");
        }
        if (m_Idle)
        {
            if (m_facingRight && !m_Jumping)
                m_PlayerAnimator.Play("Kid_idle");
            else if (!m_facingRight && !m_Jumping)
                m_PlayerAnimator.Play("Kid_idle_left");
        }
        if (m_Jumping)
        {
            if (m_facingRight)
                m_PlayerAnimator.Play("Kid_jumping");
            else if (!m_facingRight)
                m_PlayerAnimator.Play("Kid_jumping_left");
        }

        m_CameraTransform.position = new Vector3(m_PlayerTransform.position.x, m_CameraTransform.position.y, m_CameraTransform.position.z);
    }
}
