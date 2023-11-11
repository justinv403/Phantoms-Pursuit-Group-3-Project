using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{
    public float interactionDistance = 5f;
    private Camera mainCamera;
    private DoorScript selectedDoor;

    void Start()
    {
        // get the mainCamera from the player
        mainCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // initialize raycast
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance) && selectedDoor == null && Input.GetMouseButtonDown(0))
        {
            // attempt door detection
            DoorDetect(hit);

            // attempt item grab
            ItemGrab();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedDoor != null)
            {
                selectedDoor.EndInteraction();
                selectedDoor = null;
            }
        }

        // if there is a selected door
        if (selectedDoor != null)
        {
            selectedDoor.Interact(hit.point);
        }
    }

    // see if target of raycast is a door
    void DoorDetect(RaycastHit hit){

        DoorScript doorScript = hit.transform.GetComponent<DoorScript>();
        if (doorScript != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedDoor == null)
                {
                    selectedDoor = doorScript;
                    selectedDoor.StartInteraction(hit.point);
                }
            }
        }

    }

    void ItemGrab(){
        // TODO: write item grabbing script
    }
}