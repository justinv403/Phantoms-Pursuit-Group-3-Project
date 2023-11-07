using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonAnimator : MonoBehaviour
{
    // variable to hold animator from child object
    Animator[] animators;

    // get FirstPersonController script
    FirstPersonController fpsController;
    GameObject parentGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        // get animator from child
        parentGameObject = transform.parent.gameObject;
        animators = parentGameObject.GetComponentsInChildren<Animator>();
        fpsController = parentGameObject.GetComponentInChildren<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        // set the walk state of the player
        float h = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

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
        
        
        
        
    }
}
