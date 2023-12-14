using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // instantiate in inspector
    public GameObject target;

    public GameObject effectsManager;
    public GameObject hitEffect;
    public float effectDuration = 0.1f;

    public AudioClip hitSound;

    // protected variable
    protected Effect effectScript;

    // Awake is called before the other start methods
    void Awake()
    {
        effectScript = effectsManager.GetComponent<Effect>();
    }

    // Update is called once per frame
    public virtual void Process(RaycastHit hit)
    {
        
    }
}
