using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    // public variables
    public Transform hinge;
    public float rotationSpeed = 3f;
    public bool isLocked = false;
    public float slamSpeed = 10f;
    public float creakThreshold = 2f;
    public AudioClip creak;
    public AudioClip slamSound;
    public AudioClip springClick;
    public AudioClip attemptOpen;
    public float minAngle = 0f;
    public float maxAngle = 90f;
    public bool leftHinged = false;



    // private variables
    private Vector3 startDragPosition;
    private float startAngle;
    private bool isDragging = false;
    private AudioSource audioSource;
    private int creakCounter = 0;
    private bool clickPlayed = true;
    private bool slamPlayed = true;
    private GameObject doorDistanceObject;
    private GameObject playerCameraObject;

    void Start(){
        // get the audioSource from the child
        audioSource = GetComponentInChildren<AudioSource>();
        
        // get player camera for transform
        playerCameraObject = GameObject.FindWithTag("MainCamera");
    }
    
    void Update()
    {
        // slam the door when locked
        if(isLocked){
            hinge.localRotation = Quaternion.Lerp(hinge.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime * slamSpeed);
            if(!slamPlayed)
            {
                audioSource.PlayOneShot(slamSound);
                slamPlayed = true;
            }
        } else {
            slamPlayed = false;
        }

        // close door if mostly closed (within a few degrees)
        if(hinge.localEulerAngles.y < 15f && hinge.localEulerAngles.y > -15f && !isDragging && !isLocked){
            // rotate the hinge to the new angle
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            hinge.localRotation = Quaternion.Slerp(hinge.localRotation, targetRotation, Time.deltaTime * rotationSpeed * 3);
            if(!clickPlayed){
                audioSource.PlayOneShot(springClick);
                clickPlayed = true;
            }
        }
    }
    
    
    public void StartInteraction(Vector3 dragPosition)
    {
        // start initial interaction
        if(!isLocked){
            isDragging = true;
            startDragPosition = dragPosition;
            startAngle = hinge.localEulerAngles.y;
        
            // spawn an empty GameObject at the raycast hit point
            doorDistanceObject = new GameObject("DoorTracker");
            doorDistanceObject.transform.position = dragPosition;
            doorDistanceObject.transform.SetParent(playerCameraObject.transform);
            
            // if distance less than 1, set to 2 otherwise get distance
            float distanceToDoor = Vector3.Distance(playerCameraObject.transform.position, doorDistanceObject.transform.position);
            if(distanceToDoor <= 1){
                distanceToDoor = 2f;
            }
            
        } else if(isLocked)
        {
            audioSource.PlayOneShot(attemptOpen);
        }

        // set click sound as not played
        clickPlayed = false;
    }

    public void EndInteraction()
    {
        isDragging = false;
        Destroy(doorDistanceObject);
    }

    public void Interact(Vector3 dragPosition)
    {
        if (isDragging && !isLocked)
        {   
        
            // get direction vector from the Gameobject to the target point
            Vector3 direction = doorDistanceObject.transform.position - transform.position;
            
            // calculate desired rotation (as quaternion)
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // offset direction by 90 degrees for proper rotation angle
            float offset = leftHinged ? -90 : 90;
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + offset, 0);
            
            // get the y angle
            float yAngle = targetRotation.eulerAngles.y;

            // adjust for wrap-around
            if (yAngle > 180)
                yAngle -= 360;

            // clamp the angle
            float clampedY = Mathf.Clamp(yAngle, minAngle, maxAngle);
            
            // create a new quaternion with the clamped y angle
            targetRotation = Quaternion.Euler(0, clampedY, 0);
            
            // if difference between new angle and old angle is great, then play sound
            // play creak sound
            if(Mathf.Abs(targetRotation.eulerAngles.y - hinge.localEulerAngles.y) > creakThreshold && creakCounter >= 50){
                audioSource.PlayOneShot(creak);
                creakCounter = 0;
            }
            creakCounter++;

            // interpolate using Slerp with threshold
            float thresholdAngle = 1.0f; // you can adjust this value
            if (Quaternion.Angle(hinge.localRotation, targetRotation) > thresholdAngle) {
                hinge.localRotation = Quaternion.Slerp(hinge.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
            } else {
                hinge.localRotation = targetRotation;
            }

        }
    }
}