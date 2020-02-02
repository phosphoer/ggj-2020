using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateConstantly : MonoBehaviour
{
  public float xspeed = 0;
  public float yspeed = 50;
  public float zspeed = 0;

  private void Update()
  {
    transform.Rotate(xspeed * Time.deltaTime, yspeed * Time.deltaTime, zspeed * Time.deltaTime);
  }
}
