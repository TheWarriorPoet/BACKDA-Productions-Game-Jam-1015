using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    public enum Type
    {
        eType1,
        eType2,
        eType3,
        eType4
    }

    private Type m_PickupType = Type.eType1;
    public Type pickupType
    {
        get { return m_PickupType; }
        set { m_PickupType = value; }
    }

    void OnTriggerEnter(Collider a_CollisionInfo)
    {
        if(a_CollisionInfo.tag == "Player")
        {
            // TO DO: Behaviour based on pickup types
            switch(m_PickupType)
            {
                case Type.eType1:
                {
                    // Add global value modifications etc here
                    break;
                }
                case Type.eType2:
                {
                    // Add global value modifications etc here
                    break;
                }
                case Type.eType3:
                {
                    // Add global value modifications etc here
                    break;
                }
                case Type.eType4:
                {                  
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
}
