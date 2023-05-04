using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlight : MonoBehaviour
{
    public Sprite highlight;

    [HideInInspector]
    public Ray cameraRay;

    [HideInInspector]
    public RaycastHit cameraRayHit;

    [HideInInspector]
    public GameObject highlightObj;

    private GameObject player;
    private UseWeapon playerWeaponScript;

    int rayLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeaponScript = player.GetComponent<UseWeapon>();

        highlightObj = new GameObject("Mouse Position");
        highlightObj.tag = "MouseHighlight";
        highlightObj.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        highlightObj.AddComponent<SpriteRenderer>();
        highlightObj.GetComponent<SpriteRenderer>().sprite = highlight;

        rayLayerMask = 1 << 6;
    }

    void Update()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, rayLayerMask))
            highlightObj.transform.position = new Vector3(cameraRayHit.point.x, cameraRayHit.point.y + 0.1f, cameraRayHit.point.z);

    }
}
