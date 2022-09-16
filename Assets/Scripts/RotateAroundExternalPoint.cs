using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundExternalPoint : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject pivotObject;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
    }
}
