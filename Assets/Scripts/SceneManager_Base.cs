using UnityEngine;
using System.Collections;

public class SceneManager_Base : MonoBehaviour {
    // public properties
    public GameObject gameManager = null;

    private GameManager _myGameManager = null;

    // private properties

    //-------------------------------------------------------------------------------------
    // Awake()
    // Initialise the class
    //-------------------------------------------------------------------------------------
    public virtual void Awake()
    {
        _myGameManager = GameManager.instance;
        if (_myGameManager == null && gameManager != null)
        {
            GameObject temp = Instantiate(gameManager);
            _myGameManager = temp.GetComponent<GameManager>();
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
