using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlight : MonoBehaviour
{
    public Sprite highlight;

    GameObject highlightObj;

    // Start is called before the first frame update
    void Start()
    {
        highlightObj = new GameObject("Mouse Position");
        highlightObj.transform.rotation = transform.rotation;
        highlightObj.AddComponent<SpriteRenderer>();
        highlightObj.GetComponent<SpriteRenderer>().sprite = highlight;
    }

    void Update()
    {
        highlightObj.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
