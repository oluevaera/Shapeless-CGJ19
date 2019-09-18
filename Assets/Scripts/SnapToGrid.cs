using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class SnapToGrid : MonoBehaviour
{
    void Update()
    {
        if (Application.isEditor)
        {
            // This snaps to a grid when moving the cubes, but only if it is the parent gameobject.
            if (transform.parent == null)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            }
        }
    }
}
