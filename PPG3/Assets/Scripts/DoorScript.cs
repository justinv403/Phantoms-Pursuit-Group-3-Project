using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform hinge;
    private Vector3 startDragPosition;
    private float startAngle;
    private float minAngle = 0f;
    private float maxAngle = 90f;
    private bool isDragging = false;

    public void StartInteraction(Vector3 dragPosition)
    {
        isDragging = true;
        startDragPosition = dragPosition;
        startAngle = hinge.localEulerAngles.y;
    }

    public void EndInteraction()
    {
        isDragging = false;
    }

    public void Interact(Vector3 dragPosition)
    {
        if (isDragging)
        {
            Vector3 hingeToStartDragPosition = startDragPosition - hinge.position;
            Vector3 hingeToCurrentDragPosition = dragPosition - hinge.position;
            float angle = Vector3.SignedAngle(hingeToStartDragPosition, hingeToCurrentDragPosition, hinge.up);
            float newAngle = Mathf.Clamp(startAngle + angle, minAngle, maxAngle);
            hinge.localRotation = Quaternion.Euler(0, newAngle, 0);
        }
    }
}