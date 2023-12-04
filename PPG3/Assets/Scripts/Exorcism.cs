using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exorcism : MonoBehaviour
{
    public GameObject ExorcismText;
    public GameObject CannotExorcismText;

    void Start()
    {
        ExorcismText.SetActive(false);
        CannotExorcismText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ItemPickup.items <= 0)
            {
                ExorcismText.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    ExorcismText.SetActive(false);
                    GetComponent<AudioSource>().Play();
                    Debug.Log("Winner");
                }
            }
            else
            {
                CannotExorcismText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ExorcismText.SetActive(false);
        CannotExorcismText.SetActive(false);
    }
}
