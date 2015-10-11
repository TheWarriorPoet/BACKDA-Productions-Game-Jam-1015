using UnityEngine;
using System.Collections;

public class SceneManager_MainMenu : SceneManager_Base {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame()
    {
        Application.LoadLevel("MainGame002");
    }

    public void Credits()
    {
        Application.LoadLevel("Credits");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
