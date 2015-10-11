using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinScreen : MonoBehaviour {

    private int m_FrameNumber = 0;

    public Sprite Frame2;
    public Sprite Frame3;

    private float m_TimeBetweenFrames = 0.7f;
    private float m_AccumulatedFrameSwitchTime = 0;

	// Update is called once per frame
	void Update () {
        m_AccumulatedFrameSwitchTime += Time.deltaTime;
        if(m_AccumulatedFrameSwitchTime > m_TimeBetweenFrames)
        {
            m_AccumulatedFrameSwitchTime = 0;
            if (m_FrameNumber <= 2)
            {
                m_FrameNumber++;
            }
            else
            {
                Application.LoadLevel("MainMenu");
            }
            
            if(m_FrameNumber == 1)
            {
                this.gameObject.GetComponent<Image>().sprite = Frame2;
            }
            else if(m_FrameNumber == 2)
            {
                this.gameObject.GetComponent<Image>().sprite = Frame3;
            }
        }


	}
}
