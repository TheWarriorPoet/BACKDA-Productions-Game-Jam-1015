using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Colorize : MonoBehaviour {

    public bool m_Red;
    public bool m_Blue;
    public bool m_Green;

	void Update () {
        if (m_Red)
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.1f, 0, 0, 0.1f);
        }
        else if(m_Blue)
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0.1f, 0.1f);
        }
        else if(m_Green)
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0.1f, 0, 0.1f);
        }
	}
}
