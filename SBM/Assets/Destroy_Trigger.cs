using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroy_Trigger : MonoBehaviour
{
    public UnityEvent interactAction;

    private void OnTriggerEnter(Collider collision)

    {

        if (collision.gameObject.CompareTag("Player"))
        {

            interactAction.Invoke();
        }

    }

}


