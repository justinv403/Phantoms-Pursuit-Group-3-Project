using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    // public variables
    public Transform hinge;
    public float rotationSpeed = 3f;
    public bool isLocked = false;
    public float pushFactor = 1.5f;
    public float slamSpeed = 10f;
    public float creakThreshold = 2f;
    public AudioClip creak;
    public AudioClip slamSound;
    public AudioClip springClick;
    public float minAngle = 0f;
    public float maxAngle = 90f;
    public bool backwardsDirection = false;



    // private variables
    private Vector3 startDragPosition;
    private float startAngle;
    private bool isDragging = false;
    private float initialDistanceToDoor;
    private AudioSource audioSource;
    private int creakCounter = 0;
    private bool clickPlayed = true;

    void Start(){
        // get the audioSource from the child
        audioSource = GetComponentInChildren<AudioSource>();
    }
    
    void Update()
    {
        // slam the door when locked
        if(isLocked){
            hinge.localRotation = Quaternion.Lerp(hinge.localRotation, Quaternion.Euler(0,0,0), Time.deltaTime * slamSpeed);
            audioSource.PlayOneShot(slamSound);
        }

        // close door if mostly closed (within a few degrees)
        if(hinge.localEulerAngles.y < 10f && hinge.localEulerAngles.y > -10f && !isDragging){
            // rotate the hinge to the new angle
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            hinge.localRotation = Quaternion.Slerp(hinge.localRotation, targetRotation, Time.deltaTime * rotationSpeed * 3);
            if(!clickPlayed){
                audioSource.PlayOneShot(springClick);
                clickPlayed = true;
            }
        }
    }
    
    
    public void StartInteraction(Vector3 dragPosition, Vector3 playerPosition)
    {
        // start initial interaction
        if(!isLocked){
            isDragging = true;
            startDragPosition = dragPosition;
            startAngle = hinge.localEulerAngles.y;
        
            // calculate initial distance to door
            initialDistanceToDoor = Vector3.Distance(playerPosition, hinge.position);
        }

        // set click sound as not played
        clickPlayed = false;
    }

    public void EndInteraction()
    {
        isDragging = false;
    }

    public void Interact(Vector3 dragPosition, Vector3 playerPosition)
    {
        if (isDragging && !isLocked)
        {   
            // Project a point from the player towards the door at the initial interaction distance
            Vector3 projectedDragPosition = playerPosition + (dragPosition - playerPosition).normalized * initialDistanceToDoor * pushFactor;

            // get start and current drag positions.
            Vector3 hingeToStartDragPosition = startDragPosition - hinge.position;
            Vector3 hingeToProjectedDragPosition = projectedDragPosition - hinge.position;

            // Normalize the vectors
            hingeToStartDragPosition.Normalize();
            hingeToProjectedDragPosition.Normalize();

            // Calculate the angle using dot product and cross product
            float dot = Vector3.Dot(hingeToStartDragPosition, hingeToProjectedDragPosition);
            float cross = Vector3.Cross(hingeToStartDragPosition, hingeToProjectedDragPosition).y;
            float angle = Mathf.Atan2(cross, dot) * Mathf.Rad2Deg;

            // update angle, and set new angle
            float newAngle = Mathf.Clamp(startAngle + angle, minAngle, maxAngle);

            // if difference between new angle and old angle is great, then play sound
            // play creak sound
            if(Mathf.Abs(newAngle - hinge.localEulerAngles.y) > creakThreshold && creakCounter >= 50){
                audioSource.PlayOneShot(creak);
                creakCounter = 0;
            }
            creakCounter++;
            

            // rotate the hinge to the new angle
            if(backwardsDirection){
                newAngle = -newAngle;
            }
            Quaternion targetRotation = Quaternion.Euler(0, newAngle, 0);
            hinge.localRotation = Quaternion.Slerp(hinge.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}