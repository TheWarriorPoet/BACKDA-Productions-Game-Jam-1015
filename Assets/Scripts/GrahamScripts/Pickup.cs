﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//////////////////////////////////////////////////////////////////////
// Author:  Graham Johnston
// Date Created: 09/10/2015
// Brief: Implements the spawning and destruction of pickups in the game.
//     Handles the reaction when the Player collides with the pickup
//     Handles applying the sprites and relevant health values to the
//     pickups
// Instructions: Set the type in the inspector or via the m_PickupType
//      variable at runtime. Can be changed at any time before its destruction
// Note :In the final version, can disable the runtime checks for if the type
//      or scale was changed in the inspector
//////////////////////////////////////////////////////////////////////

[ExecuteInEditMode]
public class Pickup : MonoBehaviour {

    [Tooltip("Sets the Type of Pickup")]
    public Type m_PickupType;
    private Type m_LastFramePickupType; // To detect if its changed in the inspector in runtime.

    [Tooltip("Sets the Type of Pickup")]
    [Range(0, 50)]
    public int m_SizeScalar = 1;
    private int m_SizeScalarLastFrame;

    private SceneManager_MainGame m_SceneManager;

    public Sprite m_Cabbage;
    public Sprite m_Celery;
    public Sprite m_CheeseBurger;
    public Sprite m_Chips;
    public Sprite m_Hotdog;
    public Sprite m_Chocolate;
    //public Sprite m_Carrot; // NO IMAGE
    //public Sprite m_Sundae; // NO IMAGE

	public float m_respawnTime = 10;

    public enum Type
    {
        eCheeseBurger, // 4%
        eHotDog, // 3%
        eChocolate, // 2%
        //eSundae, // 2% // NO IMAGE
        eFries, // 2%
        eCabbage, // -3% (bad food)
        eCelery // -3% (bad food)
        //eCarrot // -2% (bad food) // NO IMAGE
    }

    void Start()
    {
        SetPickUpType(m_PickupType);
        m_LastFramePickupType = m_PickupType;
        m_SizeScalarLastFrame = m_SizeScalar;

        // Slightly hacky, but as it will only work for SceneManager_Main Game, i feel a public
        // variable would imply it works elsewhere
        m_SceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager_MainGame>();
    }

    void Update()
    {
        // Exists to alter the sprite during development if it changes during runtime
        // can be removed in the final version.
        if(m_LastFramePickupType != m_PickupType || m_SizeScalarLastFrame != m_SizeScalar)
        {
            // If the pickup type was changed via the public variable
            SetPickUpType(m_PickupType);
            m_LastFramePickupType = m_PickupType;
            m_SizeScalarLastFrame = m_SizeScalar;
        }
    }

    public struct PickupData
    {
        public PickupData(Type a_eType, string a_FileName)
        {
            m_eType = a_eType;
            m_FileName = a_FileName;
        }
        public Type m_eType;
        public string m_FileName;
    }

    private void SetPickUpType(Type a_eType)
    {
        // Sets teh pickup to its relevant sprite
        // Scales the image to preserve the aspect ratio.
        // Size is determined by m_SizeScalar
        m_PickupType = a_eType;
        GameObject attachedgameObject = this.gameObject;
        switch (a_eType)
        {
            case Type.eCabbage:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Cabbage;
                attachedgameObject.transform.localScale = new Vector3(1.11054f, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eCelery:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Celery;
                attachedgameObject.transform.localScale = new Vector3(1, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eCheeseBurger:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_CheeseBurger;
                attachedgameObject.transform.localScale = new Vector3(1, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eChocolate:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Chocolate;
                attachedgameObject.transform.localScale = new Vector3(1, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eFries:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Chips;
                attachedgameObject.transform.localScale = new Vector3(1, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eHotDog:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Hotdog;
                attachedgameObject.transform.localScale = new Vector3(1, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            //case Type.eSundae: // NO Image
            //    this.gameObject.GetComponent<SpriteRenderer>().sprite = m_Sundae;
            //    break;
            //case Type.eCarrot: // NO Image
            //    this.gameObject.GetComponent<SpriteRenderer>().sprite = m_Carrot;
            //    break;
            default:
                break;
        }
    }


    void OnTriggerEnter2D(Collider2D a_Collider2D)
    {
        if(a_Collider2D.tag == "Player")
        {
            SceneManager_MainGame m_RefSceneManagerMain = null;
            m_RefSceneManagerMain = m_SceneManager;

            switch(m_PickupType)
            {
                case Type.eCheeseBurger:
                {
                    m_RefSceneManagerMain.playerHealth += 4;
                    break;
                }
                case Type.eHotDog:
                {
                    m_RefSceneManagerMain.playerHealth += 3;
                    break;
                }
                case Type.eChocolate:
                {
                    m_RefSceneManagerMain.playerHealth += 2;
                    break;
                }
                case Type.eFries:
                {
                    m_RefSceneManagerMain.playerHealth += 2;
                    break;
                }
                case Type.eCabbage:
                {
                    m_RefSceneManagerMain.playerHealth -= 5;
                    break;
                }
                case Type.eCelery:
                {
                    m_RefSceneManagerMain.playerHealth -= 3;
                    break;
                }
                //case Type.eSundae: // NO IMAGE
                //{
                //    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 2; };
                //    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 2; };
                //    break;
                //}
                //case Type.eCarrot:  // NO IMAGE
                //{
                //    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth -= 3; };
                //    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth -= 3; };
                //    break;
                //}
                default:
                        break;
            }

            // Should trigger destruction of pickup.
            //Destroy(this.gameObject);
			StartCoroutine(Respawner(gameObject, m_respawnTime));
        }
    }


	public IEnumerator Respawner(GameObject go, float time)
	{
		go.GetComponent<Renderer>().enabled = false;
		go.GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(time);		
		go.GetComponent<Renderer>().enabled = true;
		go.GetComponent<Collider2D>().enabled = true;
	}
}
