using UnityEngine;
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

    [Tooltip("The scene manager in this scene")]
    public SceneManager_Base m_SceneManager;

    [Tooltip("Sets the Type of Pickup")]
    public Type m_PickupType;
    private Type m_LastFramePickupType; // To detect if its changed in the inspector in runtime.

    [Tooltip("Sets the Type of Pickup")]
    [Range(0, 50)]
    public int m_SizeScalar = 1;
    private int m_SizeScalarLastFrame;

    public Sprite m_Cabbage;
    public Sprite m_Celery;
    public Sprite m_CheeseBurger;
    public Sprite m_Chips;
    public Sprite m_Hotdog;
    public Sprite m_Chocolate;
    //public Sprite m_Carrot; // NO IMAGE
    //public Sprite m_Sundae; // NO IMAGE

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
                attachedgameObject.transform.localScale = new Vector3(0.60674f, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eCheeseBurger:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_CheeseBurger;
                attachedgameObject.transform.localScale = new Vector3(1.7f, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eChocolate:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Chocolate;
                attachedgameObject.transform.localScale = new Vector3(0.73299f, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eFries:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Chips;
                attachedgameObject.transform.localScale = new Vector3(0.55700f, 1, 0) * 
                    ((float)m_SizeScalar / 10.0f);
                break;
            case Type.eHotDog:
                attachedgameObject.GetComponent<SpriteRenderer>().sprite = m_Hotdog;
                attachedgameObject.transform.localScale = new Vector3(1.33484f, 1, 0) * 
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
            // This is a hack to allow this to work in both the main scene and my test scene.
            // In the final version, will just use mainGame public variable.
            SceneManager_Graham m_RefSceneManagerGraham = null;
            SceneManager_MainGame m_RefSceneManagerMain = null;
            if (m_SceneManager.name == "SceneManager")
            {
                // Currently in main game screen
                m_RefSceneManagerMain = m_SceneManager as SceneManager_MainGame;
            }
            else if(m_SceneManager.name == "SceneManagerTest")
            {
                // currently in graham test scene
                m_RefSceneManagerGraham = m_SceneManager as SceneManager_Graham;
            }

            switch(m_PickupType)
            {
                case Type.eCheeseBurger:
                {
                    if(m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 4; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 4; };
                    break;
                }
                case Type.eHotDog:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 3; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 3; };
                    break;
                }
                case Type.eChocolate:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 2; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 2; };
                    break;
                }
                case Type.eFries:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 2; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 2; };
                    break;
                }
                case Type.eCabbage:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth -= 5; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth -= 5; };
                    break;
                }
                case Type.eCelery:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth -= 3; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth -= 3; };
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
            Destroy(this.gameObject);
        }
    }
}
