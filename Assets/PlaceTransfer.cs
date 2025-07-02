using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlaceTransfer : MonoBehaviour
{
    public Transform edgeLeft;
    public Transform edgeRight;

    void Update()
    {
        if (transform.position.x > edgeRight.position.x)
            transform.position = new UnityEngine.Vector3(edgeLeft.position.x, transform.position.y, transform.position.z);
        else if (transform.position.x < edgeLeft.position.x)
            transform.position = new UnityEngine.Vector3(edgeRight.position.x, transform.position.y, transform.position.z);
    }

}
