using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonAnimator : MonoBehaviour
{
    // variable to hold animator from child object
    Animator[] animators;

    // get FirstPersonController script
    private FirstPersonController fpsController;
    private GameObject remyVisible;
    private GameObject remyShadow;
    
    // Start is called before the first frame update
    void Start()
    {
        // get animator from child
        remyVisible = GameObject.Find("RemyVisible");
        remyShadow = GameObject.Find("RemyShadow");
        animators[0] = remyVisible.GetComponent<Animator>();
        animators[1] = remyShadow.GetComponent<Animator>();

        // get FPS controller
        fpsController = gameObject.GetComponentInChildren<FirstPersonController>();

        Debug.Log("remyVisible = " + remyVisible);
    }

    // Update is called once per frame
    void Update()
    {
        // set the walk state of the player
        float h = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        /*
        if(h > 0 && !fpsController.isCrouched)
        {
            animators[0].SetFloat("WalkState", 1.0f);
            animators[1].SetFloat("WalkState", 1.0f);

        } else if (h > 0 && fpsController.isCrouched)
        {
            animators[0].SetFloat("WalkState", 0.66f);
            animators[1].SetFloat("WalkState", 0.66f);

        } else if (h == 0 && fpsController.isCrouched)
        {
            animators[0].SetFloat("WalkState", 0.33f);
            animators[1].SetFloat("WalkState", 0.33f);

        } else
        {
            animators[0].SetFloat("WalkState", 0.0f);
            animators[1].SetFloat("WalkState", 0.0f);
        }
        */
        
        
        
    }
}
