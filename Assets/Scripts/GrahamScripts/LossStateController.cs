using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 10/10/2015
// Brief:  A Basic loss state controller, that checks for loss conditions
//      then implements a response.
//////////////////////////////////////////////////////////////////////

public class LossStateController : MonoBehaviour {

    [Tooltip("The Main Scene Scene Manager")]
    public SceneManager_MainGame m_SceneManager;

    private bool m_LossTriggered;


	// Update is called once per frame
	void Update () {
        // If we havent lost already, and if the loss conditions are triggered
        // Check if weve lost already first, to reduce load if loss conditions are already triggered
        if (m_LossTriggered == false && CheckLossConditions() == true)
        {
            LossConditionsTriggered();
        }
        else if (m_LossTriggered)
        {
            OnGoingLossEffect();
        }
	}

    bool CheckLossConditions()
    {
        // Currently just check if health is below 0
        if(m_SceneManager.playerHealth < 0)
        {
            return true;
        }
        return false;
    }

    void LossConditionsTriggered()
    {
        m_LossTriggered = true;
        Application.LoadLevel("MainGame002");
        // Note : Can include other conditions that are set when the loss is triggered.
    }

    // Played every frame after the loss conditions are triggered, for ongoing effects
    void OnGoingLossEffect()
    {
        // TO DO: Darkening effect on death
    }
}
