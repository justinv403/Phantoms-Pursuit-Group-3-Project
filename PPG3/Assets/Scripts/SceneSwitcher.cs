using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void OpenScene(int index)
    {
        ItemPickup.items = 0;
        SceneManager.LoadScene(index);
    }
}
