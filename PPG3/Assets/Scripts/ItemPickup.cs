using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public static int items;

    public AudioClip pickupSound;

    void Start()
    {
        items++;
        Debug.Log(items);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Pickup");
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                items--;
                Debug.Log(items);
                Destroy(gameObject);
            }
        }
    }


}
