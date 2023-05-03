using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tides : MonoBehaviour
{
    public float tideHeight;
    public float tideSpeed;

    float baseTideHeight;
    float maxTideHeight;
    float minTideHeight;

    public bool tideUp;

    void Start()
    {
        baseTideHeight = transform.position.y;
        maxTideHeight = baseTideHeight + tideHeight;
        minTideHeight = baseTideHeight - tideHeight;

        tideUp = true;

        StartCoroutine(MoveTide());
    }

    IEnumerator MoveTide()
    {
        if (transform.position.y < maxTideHeight && tideUp)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 0.002f, transform.position.z), Time.fixedDeltaTime);
        else
        {
            tideUp = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z), Time.fixedDeltaTime);
        }

        if (transform.position.y <= baseTideHeight)
            tideUp = !tideUp;

        yield return new WaitForSeconds(tideSpeed);
        StartCoroutine(MoveTide());
    }
}
