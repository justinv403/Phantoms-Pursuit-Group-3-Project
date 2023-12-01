using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f;

    public ParticleSystem muzzleFlash;


    private Camera fpsCamera;
    private float nextTimeToFire;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GameObject.Find("FirstPersonController").GetComponentInChildren<Camera>();
        audioSource = gameObject.GetComponent<AudioSource>();
        nextTimeToFire = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ready(to fire)
        bool ready = Time.time >= nextTimeToFire;
        if (ready && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // shoot method
    void Shoot()
    {
        // display muzzle flash if it exists
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // play fire sound
        if (audioSource != null)
        {
            audioSource.Play();
        }
        
        // initiate RaycastHit
        RaycastHit hit;

        // fire and test raycast position for hit detection
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            // try to get a Target, if it has one
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                // call target hit method
                target.Process(hit);
            }

            // set cooldown
            nextTimeToFire = Time.time + 0.2f;
        }
    }
}
