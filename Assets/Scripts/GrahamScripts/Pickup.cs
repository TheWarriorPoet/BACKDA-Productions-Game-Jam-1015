using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour {

    [Tooltip("The scene manager in this scene")]
    public SceneManager_Base m_SceneManager;

    public enum Type
    {
        eCheeseBurger, // 4%
        eHotDog, // 3%
        eChocolate, // 2%
        eSundae, // 2%
        eFries, // 2%
        eCabbage // -3% (bad food)
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

    private Type m_PickupType = Type.eCheeseBurger;
    public Type pickupType
    {
        // Value should be set by the function, which spawns the appropriate asset to match the pickup
        get { return m_PickupType; }
    }

    // Static storage of the filenames of each pickup type
    static List<PickupData> m_AvailablePickups = new List<PickupData>();

    void Awake()
    {
        // Initalises the pickuplist with prefab names for each of them
        if(m_AvailablePickups.Count == 0)
        {
            // TO DO :Add in appropraite file names
            m_AvailablePickups.Add(new PickupData(Type.eCheeseBurger, "none"));
            m_AvailablePickups.Add(new PickupData(Type.eHotDog, "none"));
            m_AvailablePickups.Add(new PickupData(Type.eChocolate, "none"));
            m_AvailablePickups.Add(new PickupData(Type.eSundae, "none"));
            m_AvailablePickups.Add(new PickupData(Type.eFries, "none"));
            m_AvailablePickups.Add(new PickupData(Type.eCabbage, "none"));
        }
    }

    public void SetPickUpType(Type a_eType)
    {
        m_PickupType = a_eType;
        string associatedPrefabName = GetPickupFileName(a_eType);

        // TO DO: Load in the appropriate asset based on the type and given filename
    }


    void OnTriggerEnter(Collider a_CollisionInfo)
    {
        if(a_CollisionInfo.tag == "Player")
        {
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

            // TO DO: Behaviour based on pickup types
            switch(m_PickupType)
            {
                case Type.eCheeseBurger:
                {
                    if(m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 4; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 4; };
                    // Add global value modifications etc here
                    break;
                }
                case Type.eHotDog:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 3; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 3; };
                    // Add global value modifications etc here
                    break;
                }
                case Type.eChocolate:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 2; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 2; };
                    // Add global value modifications etc here
                    break;
                }
                case Type.eSundae:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 2; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 2; };
                    // Add global value modifications etc here
                    break;
                }
                case Type.eFries:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth += 2; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth += 2; };
                    // Add global value modifications etc here
                    break;
                }
                case Type.eCabbage:
                {
                    if (m_RefSceneManagerGraham != null) { m_RefSceneManagerGraham.playerHealth -= 3; };
                    if (m_RefSceneManagerMain != null) { m_RefSceneManagerMain.playerHealth -= 3; };
                    // Add global value modifications etc here
                    break;
                }
                default:
                        break;
            }

            // Should trigger destruction of pickup.
            Destroy(this.gameObject);
        }
    }

    string GetPickupFileName(Type a_PickupType)
    {
        foreach (var PickUp in m_AvailablePickups)
        {
            if (m_PickupType == PickUp.m_eType)
            {
                return PickUp.m_FileName;
            }
        }
        return " ";
    }
}
