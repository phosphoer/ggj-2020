using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
    public Vector3 RotationMask = new Vector3(1f,1f,1f);
    public float RotationSpeed = 2f;

    float rotAmount = 0f;
    Vector3 rotAxis = new Vector3();
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        NewRotation();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotAxis,rotAmount*Time.deltaTime*RotationSpeed,Space.Self);
        timer+=Time.deltaTime;
        if(timer>=3f) NewRotation();

    }

    public void NewRotation()
    {
        rotAxis = new Vector3(
            Random.Range(0f,1f)*RotationMask.x,
            Random.Range(0f,1f)*RotationMask.y,
            Random.Range(0f,1f)*RotationMask.z
            ).normalized;
        
        rotAmount = Random.Range(0f,180f);
         
    }
}
