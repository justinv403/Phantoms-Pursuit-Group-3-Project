using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalP2P : MonoBehaviour
{
    public GameObject fpsController;
    public GameObject p2pPortalManager;
    public GameObject destinationPortal;

    private P2PPortalInfo p2pPortalInfo;

    private void Start()
    {
        p2pPortalInfo = p2pPortalManager.GetComponent<P2PPortalInfo>();
    }

    private void Update()
    {
        p2pPortalInfo.portalLockoutTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (p2pPortalInfo.portalLockoutTimer >= p2pPortalInfo.portalLockoutMax)
            {
                p2pPortalInfo.portalLockoutTimer = 0f;
                Vector3 position = destinationPortal.transform.position;
                MoveTo(other.gameObject, position);
            }
        }
    }

    void MoveTo(GameObject player, Vector3 position)
    {
        player.transform.position = position;
    }
}
