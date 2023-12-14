using UnityEngine;
using UnityEngine.SceneManagement;

public class Exorcism : MonoBehaviour
{
    public GameObject ExorcismText;
    public GameObject CannotExorcismText;
    public GameObject WinPanel; // Reference to the win UI panel
    public GameObject ReturnMenuText; // Reference to the return button

    void Start()
    {
        ExorcismText.SetActive(false); // Hide text
        CannotExorcismText.SetActive(false);
        WinPanel.SetActive(false); // Initially hide the win panel
        ReturnMenuText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ItemPickup.items <= 0) // All items picked up
            {
                ExorcismText.SetActive(true); // Show text indicating that you can and how to exorcise
                if (Input.GetKey(KeyCode.E)) // Press E to Exorcise
                {
                    ExorcismText.SetActive(false); // Remove text if player wins
                    GetComponent<AudioSource>().Play();
                    Debug.Log("Winner");
                    ShowWinPanel(); // Show win panel instead of just logging
                }
            }
            else
            {
                CannotExorcismText.SetActive(true); // If player doesn't have enought items, show text saying they don't
            }
        }
    }

    // Remove text when player leaves area
    private void OnTriggerExit(Collider other)
    {
        ExorcismText.SetActive(false);
        CannotExorcismText.SetActive(false);
    }

    private void ShowWinPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        WinPanel.SetActive(true); // Activate the win panel
        ReturnMenuText.SetActive(true);
    }

    public void LoadMainMenu() // Function to load the main menu
    {
        SceneManager.LoadScene("Start Scene"); // Replace with your main menu scene name
    }
}
