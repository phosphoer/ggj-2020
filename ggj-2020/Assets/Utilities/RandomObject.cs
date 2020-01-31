using UnityEngine;

public class RandomObject : MonoBehaviour
{
  [SerializeField]
  private GameObject[] _objects = null;

  private void Start()
  {
    foreach (GameObject obj in _objects)
    {
      obj.SetActive(false);
    }

    _objects[Random.Range(0, _objects.Length)].SetActive(true);
  }
}