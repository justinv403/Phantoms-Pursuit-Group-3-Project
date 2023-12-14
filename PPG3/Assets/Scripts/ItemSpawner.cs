using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject toyPrefab1;
    public GameObject toyPrefab2;
    public int itemTotal;

    public GameObject[] spawnLocations;

    private int itemModel;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("ItemSpawnLocation");
        Shuffle(spawnLocations);

        for (int i = 0; i < itemTotal; i++)
        {
            itemModel = Random.Range(0, 2);
            if (itemModel == 0)
            {
                Instantiate(toyPrefab1, spawnLocations[i].transform);
            }
            else if (itemModel == 1)
            {
                Instantiate(toyPrefab2, spawnLocations[i].transform);
            }
        }
    }

    // Code courtesy of https://forum.unity.com/threads/randomize-array-in-c.86871/
    // Utilize Knuth shuffle algorithm
    void Shuffle(GameObject[] locations)
    {
        for (int i = 0; i < locations.Length; i++)
        {
            GameObject temp = locations[i];
            int random = Random.Range(i, locations.Length);
            locations[i] = locations[random];
            locations[random] = temp;
        }
    }
}
