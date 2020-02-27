using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class DeadNaught : MonoBehaviour
{
    public Animator BodyAnim;
    public Transform BodyRoot;
    public Vector2 InitRotXZ_Max = new Vector2(45f,45f);
    public Vector2 InitRotXZ_Min = new Vector2(-45f,-45f);


    // Start is called before the first frame update
    void Start()
    {
        BodyAnim.SetFloat("IdleState",3f); //this should force them in the "Float_Airlock" state. The head should be good by default.
        
        //Set the initial XZ rotation of the thing...
        BodyRoot.localEulerAngles += new Vector3(
            Random.Range(InitRotXZ_Min.x,InitRotXZ_Max.x),
            Random.Range(InitRotXZ_Min.y,InitRotXZ_Max.y),
            0f
        );
    }
}
