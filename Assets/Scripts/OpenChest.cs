using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public GameObject openChestPrefab;
    public GameData.WeaponType itemContained;

    public float interactionRadius;

    [SerializeField]
    private bool openable;
    private MouseHighlight mouseRayScript;

    private GameObject player;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        mouseRayScript = mainCam.GetComponent<MouseHighlight>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        openable = Vector3.Distance(player.transform.position, transform.position) < interactionRadius;

        if (Physics.Raycast(mouseRayScript.cameraRay, out mouseRayScript.cameraRayHit, Mathf.Infinity, 1 << 8))
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && openable && mouseRayScript.cameraRayHit.collider.tag == "Chest")
            {
                player.GetComponent<EntityHealth>().AddHealth(5);
                Instantiate(openChestPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

}
