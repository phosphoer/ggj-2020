using UnityEngine;

public class Move : MonoBehaviour
{
  public Vector3 MoveVector = Vector3.forward;

  private void Update()
  {
    transform.position += MoveVector * Time.deltaTime;
  }
}