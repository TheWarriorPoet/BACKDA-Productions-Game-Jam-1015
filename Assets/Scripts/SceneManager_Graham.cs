using UnityEngine;
using System.Collections;

public class SceneManager_Graham : SceneManager_Base {

    // Min and max health values, to clamp the player health and prevent bugs
    // Read only access to both.
    private int m_MaxHealth = 100;
    public int maxHealth
    {
        get { return m_MaxHealth; }
    }
    private int m_MinHealth = 0;
    public int minHealth
    {
        get { return m_MinHealth; }
    }

    // Global Health variable used by A Variety of Scripts to alter and retrieve health.
    // Check with other programmers before changing the name of the parameter, or youll break scripts
    private int m_PlayerHealth;
    public int playerHealth
    {
        get { return m_PlayerHealth; }
        // Value is clamped between min and max to prevent bugs
        set { m_PlayerHealth = Mathf.Clamp(value, m_MinHealth, m_MinHealth); }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
