using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeThrough : MonoBehaviour
{
    public float minAlphaLeaves;

    Ray ray;
    RaycastHit hit;

    GameObject currentTree;

    int rayLayerMask;

    bool hitting = false;
    GameObject hitObject;

    void Start()
    {
        rayLayerMask = 1 << 9;
    }

    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayerMask))
        {
            currentTree = hit.transform.gameObject;
            if (hit.transform.gameObject.layer == 9)
            {
                GameObject obj = hit.transform.gameObject;

                if (hitObject == null)
                {
                    obj.SendMessage("OnHitEnter");
                }
                else if (hitObject.GetInstanceID() == obj.GetInstanceID())
                {
                    hitObject.SendMessage("OnHitStay");
                }
                else
                {
                    hitObject.SendMessage("OnHitExit");
                    obj.SendMessage("OnHitEnter");
                }

                hitting = true;
                hitObject = obj;
            }
        }
        else if (hitting)
        {
            hitObject.SendMessage("OnHitExit");
            hitting = false;
            hitObject = null;
        }
    }



}
