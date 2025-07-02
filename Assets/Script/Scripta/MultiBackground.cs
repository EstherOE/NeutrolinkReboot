using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBackground : MonoBehaviour
{

     public Transform[] panes;

    public float scrollSpeed = -2f;

    float paneWidth;

    void Start()
    {
        if (panes.Length == 0) return;
         paneWidth = panes[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
         float dx = scrollSpeed * Time.deltaTime;
        for (int i = 0; i < panes.Length; i++)
            panes[i].position += Vector3.right * dx;

        for (int i = 0; i < panes.Length; i++)
        {
            if (panes[i].position.x <= -paneWidth)
            {
                 float maxX = float.MinValue;
                foreach (var p in panes)
                    if (p.position.x > maxX) maxX = p.position.x;
        panes[i].position = new Vector3(maxX + paneWidth,
                                               panes[i].position.y,
                                               panes[i].position.z);
            }
        }
    }

}
