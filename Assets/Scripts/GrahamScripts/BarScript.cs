using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////////////
// Author: Graham Johnston
// Date Created: 09/10/2015
// Brief: A Generic Implementation for a power bar.
//      Fills from the left to right, changes the width of the image based on
//      the percentage value.
// Instructions: Should be able to apply it to any UI image that you need to
//      fil from left to right(like a power bar) can adjust the value via public
//      parameter.
//////////////////////////////////////////////////////////////////////

public class BarScript : MonoBehaviour {

    private float m_PercentageAmount = 1.0f;
    public float amount // Percentage between 0.0 and 1.0.
    {
        get { return m_PercentageAmount; }
    }
    private float m_StartingWidth;
    private float m_StartingXPos;

	void Start () {
        m_StartingWidth = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;

        // This is related to the leftmost position of the power bar as it fills and the related
        // Position adjustments made in update to correct the fact that it fills from the center
        // in each directions by default
        Vector2 anchoredPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
        anchoredPos.x -= m_StartingWidth / 2;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPos;

        m_StartingXPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.x;

        // m_StartingXPos = this.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
    }
	
	void Update () {
        //Clamp the amount to be a percentage between 0 and 1
        m_PercentageAmount = Mathf.Clamp(m_PercentageAmount, 0.0f, 1.0f);

        RectTransform attachedRectTransform = this.gameObject.GetComponent<RectTransform>();

        // Adjusting the size of the bar depending on the value of the percentage amount
        Vector2 rectSize = attachedRectTransform.sizeDelta;
        rectSize.x = m_StartingWidth * m_PercentageAmount;
        attachedRectTransform.sizeDelta = rectSize;

        // Adjusting the X Position of the object so that the bar fills from the instead of 
        // spreading out in each direction from the center 
        Vector2 anchoredRectPos = attachedRectTransform.anchoredPosition;
        anchoredRectPos.x = m_StartingXPos + (m_StartingWidth * m_PercentageAmount)*0.5f;
        attachedRectTransform.anchoredPosition = anchoredRectPos;
    }
}
