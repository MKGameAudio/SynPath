using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRTPCtoDistance : MonoBehaviour
{
    public AK.Wwise.RTPC GameParameter;
    public Transform PlayerPosition;
    void Update()
    {
       float DistanceToObject = Vector3.Distance(PlayerPosition.position, transform.position);
        GameParameter.SetGlobalValue(DistanceToObject);
    }
}
