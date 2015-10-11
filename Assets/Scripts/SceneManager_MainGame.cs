using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author : Andrew Jones
// Modifications : Graham Johnston Date Modified
// Date Modified : 09/10/15 
//////////////////////////////////////////////////////////////////////

public class SceneManager_MainGame : SceneManager_Base {

    // Min and max health values, to clamp the player health and prevent bugs
    // Read only access to both.
    private int m_MaxHealth = 100;
	private bool _couch_loaded = false;
	private GameObject _couch;
    public int maxHealth
    {
        get { return m_MaxHealth; }
    }
    //private int m_MinHealth = 0;
    //public int minHealth
    //{
    //    get { return m_MinHealth; }
    //}

    // Global Health variable used by A Variety of Scripts to alter and retrieve health.
    // Check with other programmers before changing the name of the parameter, or youll break scripts
    private int m_PlayerHealth = 50;
    public int playerHealth
    {
        get { return m_PlayerHealth; }
        // Value is clamped between min and max to prevent bugs
        set { m_PlayerHealth = value;}
    }

    public BarScript healthBar;

    // Use this for initialization
    void Start () {
		_couch = GameObject.Find("Couch");
	}
	
	// Update is called once per frame
	void Update () {
        //print(((float)m_PlayerHealth / (float)m_MaxHealth).ToString());
        healthBar.percentageValue = (float)m_PlayerHealth / (float)m_MaxHealth;

        if(m_PlayerHealth > m_MaxHealth || Input.GetKeyDown(KeyCode.T))
        {
            m_PlayerHealth = m_MaxHealth;
			if(!_couch_loaded)
			{
				print("loading...");
				_couch.GetComponent<couch_collider>()._load_couch = true;
				_couch_loaded = true;
			}
        }
    }
}
