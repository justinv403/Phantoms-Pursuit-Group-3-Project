using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonAnimator : MonoBehaviour
{
    // animator variables
    Animator visibleAnimator;
    Animator shadowAnimator;

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

        visibleAnimator = remyVisible.GetComponent<Animator>();
        shadowAnimator = remyShadow.GetComponent<Animator>();

        // get FPS controller
        fpsController = gameObject.GetComponentInChildren<FirstPersonController>();

    }

    // Update is called once per frame
    void Update()
    {
        // set the walk state of the player
        float h = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        if(h > 0 && !fpsController.isCrouched)
        {
            visibleAnimator.SetFloat("WalkState", 1.0f);
            shadowAnimator.SetFloat("WalkState", 1.0f);

        } else if (h > 0 && fpsController.isCrouched)
        {
            visibleAnimator.SetFloat("WalkState", 0.66f);
            shadowAnimator.SetFloat("WalkState", 0.66f);

        } else if (h == 0 && fpsController.isCrouched)
        {
            visibleAnimator.SetFloat("WalkState", 0.33f);
            shadowAnimator.SetFloat("WalkState", 0.33f);

        } else
        {
            visibleAnimator.SetFloat("WalkState", 0.0f);
            shadowAnimator.SetFloat("WalkState", 0.0f);
        }
        
    }
}
