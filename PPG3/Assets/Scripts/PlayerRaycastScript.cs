using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{
    public float interactionDistance = 5f;
    private Camera mainCamera;
    private DoorScript selectedDoor;

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

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedDoor != null)
            {
                selectedDoor.EndInteraction();
                selectedDoor = null;
            }
        }

        if (selectedDoor != null)
        {
            selectedDoor.Interact(hit.point);
        }
    }
}