using UnityEngine;

public class TargetStack : Target
{
    // force applied to the target
    public float impactForce;
    
    Rigidbody targetRB;

    // Start is called before the first frame update
    void Start()
    {
        targetRB = target.GetComponent<Rigidbody>();
    }

    public override void Process(RaycastHit hit)
    {
        targetRB.AddForce(-hit.normal * impactForce);

        effectScript.Play(hit, hitSound, hitEffect, effectDuration);
    }
}
