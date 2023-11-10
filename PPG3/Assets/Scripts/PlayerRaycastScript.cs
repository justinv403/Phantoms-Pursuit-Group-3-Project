using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{
    public float interactionDistance = 5f;
    private Camera mainCamera;
    private DoorScript selectedDoor;
    private float distanceToSelectedDoor = 0f; // track distance to door

    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // initialize raycast
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance))
        {
            DoorScript doorScript = hit.transform.GetComponent<DoorScript>();
            if (doorScript != null)
            {
                if(distanceToSelectedDoor == interactionDistance){
                    distanceToSelectedDoor = hit.distance; // Update the distance
                }
                
                
                if (Input.GetMouseButtonDown(0))
                {
                    selectedDoor = doorScript;
                    selectedDoor.StartInteraction(hit.point);
                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedDoor = null;
            distanceToSelectedDoor = interactionDistance;
        }

        if (selectedDoor != null)
        {
            selectedDoor.Interact(hit.point);
        }

    }
}