using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractOnE : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public UnityEvent interactAction01;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            interactAction.Invoke();
            AkSoundEngine.PostEvent("Play_Painting", gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)

    {

        if (collision.gameObject.CompareTag("Player"))
        {

            isInRange = true;



        }

    }
    private void OnTriggerExit(Collider collision)

    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            interactAction01.Invoke();
        }

    }

}

