using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    
    public float RotationSpeed = 1f;
    public Vector3 Axis = new Vector3(0f,0f,1f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Axis.normalized,RotationSpeed*Time.deltaTime,Space.Self);
    }
}
