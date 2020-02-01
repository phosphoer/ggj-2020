using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  public bool SpawnOnPlayerSpawn = true;
  public float SpawnRadius = 2;

  [SerializeField]
  private AIAstronautController _aiPrefab = null;

  private void OnEnable()
  {
    if (SpawnOnPlayerSpawn)
      PlayerAstronautController.PlayerSpawned += OnPlayerSpawned;
  }

  private void OnDisable()
  {
    if (SpawnOnPlayerSpawn)
      PlayerAstronautController.PlayerSpawned -= OnPlayerSpawned;
  }

  private void OnPlayerSpawned(PlayerAstronautController player)
  {
    Vector3 spawnPos = transform.position + Random.insideUnitSphere.WithY(0) * SpawnRadius;
    Quaternion spawnRot = Quaternion.Euler(0, Random.value * 360, 0);
    AIAstronautController aiAstronaut = Instantiate(_aiPrefab, spawnPos, spawnRot);
  }
}