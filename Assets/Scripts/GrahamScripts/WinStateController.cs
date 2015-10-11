using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 10/10/2015
// Brief:  Sets the Win Conditions and Comfort zone spawning mechanic.
//      Primarily it runs a series of checks first to see if it needs to
//      spawn the comfort zone, second if youve reached it and won
//      or third if you didnt reach it in time.
//      It handles the spawning and lifetime management of the 
//      ComfortZone object via a prefab.
//////////////////////////////////////////////////////////////////////

public class WinStateController : MonoBehaviour {

    [Tooltip("The Main Scene Scene Manager")]
    public SceneManager_MainGame m_SceneManager;

    [Tooltip("Sofa Prefab")]
    public GameObject m_SofaPrefab;

    private GameObject m_CurrentSofa;

    [Tooltip("The Min distance the Sofa can spawn to the left or right of the player")]
    [Range(2.0f, 50.0f)]
    public float m_YMinOffset = 2.0f;
    [Tooltip("The Min distance the Sofa can spawn to the left or right of the player")]
    [Range(3.0f, 50.0f)]
    public float m_XMinOffset = 3.0f;

    [Tooltip("The Maximum distance the Sofa can spawn upwards from the player")]
    [Range(3.0f, 50.0f)]
    public float m_YMaximumOffset = 10.0f;
    [Tooltip("The Maximum distance the Sofa can spawn to the left or right of the player")]
    [Range(2.0f, 200.0f)]
    public float m_XMaximumOffset = 17.0f;

    [Tooltip("The amount of time the spawned Sofa/Comfort zone exists")]
    public float m_SecondsSofaExists = 20.0f;
    private float m_AccumulatedDeltaTime = 0; // For timer checks on the spawned sofa/comfort zone

    [Tooltip("The amount of health you lose when you fail to reach the sofa in time")]
    [Range(0, 100)]
    public int m_HealthLostIfTimerExpires = 50;

    public Sprite m_AniFrame1;
    public Sprite m_AniFrame2;
    public Sprite m_AniFrame3;

    private int m_TrackedHealth;
    private int m_MaxHealth;

    private bool m_SofaSpawned = false;
    public bool isSofaSpawned // Has the Sofa Spawned but the win has not triggered yet
    {
        get { return m_SofaSpawned; }
    }

    private bool m_WonGame = false;
    public bool hasWonGame
    {
        get { return m_SofaSpawned; } // Has triggered the win conditions for the Game/level
    }

    private bool m_WinTriggeredAnimation = false;
    private int m_SwitchCount = 0;
    private float m_DurationSwitch = 0.5f;
    private float m_AccumulatedDeltaTimeWinTrigger = 0;


	void Start () {
        m_MaxHealth = m_SceneManager.maxHealth;
        m_TrackedHealth = m_SceneManager.playerHealth;
    }
	
	void Update () {
        if (m_WonGame)
        {
            // If we already won, skip all win checks/processing
            return;
        }

        m_TrackedHealth = m_SceneManager.playerHealth;

        if (m_SofaSpawned == false && CheckSofaSpawnConditions() == true)
        {
            // If sofa is not spawned and we pass the sofa spawning conditions, spawn one
            SpawnSofa();
        }
        else if(m_SofaSpawned == true)
        {
            bool passedTimer = CheckSofaSpawnTimer();
            if(passedTimer == true)
            {
                // If we passed the timer, despawn the sofa and repeat the process from the start.
                Destroy(m_CurrentSofa);
                m_SofaSpawned = false;
                m_SceneManager.playerHealth -= m_HealthLostIfTimerExpires;
                return;
            }
            if(CheckWinConditions() == true)
            {
                // if sofa is spawned and we pass the win conditions, trigger the win resolution/processing
                WinTriggered();
            }
        }
        if (m_WinTriggeredAnimation)
        {
            WinTriggeredOngoing();
        }

	}
    
    // Should include any other kinds of checks we want to include for sofa spawning
    private bool CheckSofaSpawnConditions()
    {
        if(m_TrackedHealth >= m_MaxHealth)
        {
            return true;
        }
        return false;
    }

    private void SpawnSofa()
    {
        // Retrieve the player position
        GameObject m_Player = GameObject.FindGameObjectWithTag("Player");
        Vector3 spawnPosition = m_Player.transform.position;

        //Creating a random x offset amount from the player.
        float randomOffsetX = Random.Range(-m_XMaximumOffset, m_XMaximumOffset);
        // Checking if we are within the minimum offset from the player, if so we adjust
        if(randomOffsetX > -m_XMinOffset && randomOffsetX < m_XMinOffset)
        {
            randomOffsetX = m_XMinOffset;
        }

        // Add in a random offset from the player position
        spawnPosition.x += randomOffsetX;
        spawnPosition.y = spawnPosition.y + Random.Range(m_YMinOffset, m_YMaximumOffset); // Always above the player

        // Instantiate the prefab and spawn it at the position we just calculated
        m_CurrentSofa = Instantiate(m_SofaPrefab);
        m_CurrentSofa.transform.position = spawnPosition;
        m_SofaSpawned = true;
    }
    
    private bool CheckSofaSpawnTimer()
    {
        m_AccumulatedDeltaTime += Time.deltaTime;
        if(m_AccumulatedDeltaTime > m_SecondsSofaExists)
        {
            // Passed the timer;
            m_AccumulatedDeltaTime = 0.0f; // reset the timer
            return true;
        }
        else
        {
            // Not passed the timer
            return false;
        }
    }

    // Contains any kinds of checks we want for Win Conditions
    private bool CheckWinConditions()
    {
        if (m_CurrentSofa.GetComponent<HasCollided>().hasCollided == true)
        {
            return true;
        }
        return false;
    }

    private void WinTriggered()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = false;
        m_WinTriggeredAnimation = true;
    }

    private void WinTriggeredOngoing()
    {
        m_AccumulatedDeltaTimeWinTrigger += Time.deltaTime;
        if(m_AccumulatedDeltaTimeWinTrigger > m_DurationSwitch)
        {
            m_AccumulatedDeltaTimeWinTrigger = 0;
            if(m_SwitchCount == 0){
                m_CurrentSofa.GetComponent<SpriteRenderer>().sprite = m_AniFrame1;
            }
            else if(m_SwitchCount == 1){
                m_CurrentSofa.GetComponent<SpriteRenderer>().sprite = m_AniFrame2;
            }
            else if(m_SwitchCount == 2){
                m_CurrentSofa.GetComponent<SpriteRenderer>().sprite = m_AniFrame3;
            }
            m_SwitchCount++;
        }

        if(m_SwitchCount == 3)
        {
            m_WinTriggeredAnimation = false;
            Destroy(m_CurrentSofa);
            m_WonGame = true;
        }
    }
}
