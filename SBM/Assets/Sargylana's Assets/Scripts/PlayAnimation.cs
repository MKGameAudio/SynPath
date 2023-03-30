using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public string animationName;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play(animationName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
