using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  public bool SpawnOnPlayerSpawn = false;
  public bool SpawnOnGameStart = false;
  public float SpawnOnTimer = -1;
  public float SpawnRadius = 2;

  [SerializeField]
  private AIAstronautController _aiPrefab = null;

  private void OnEnable()
  {
    if (SpawnOnPlayerSpawn)
      PlayerAstronautController.PlayerSpawned += OnPlayerSpawned;

    if (SpawnOnGameStart)
      GameStateManager.GameStarted += OnGameStarted;

    if (SpawnOnTimer > 0)
      StartCoroutine(SpawnTimerAsync());
  }

  private void OnDisable()
  {
    if (SpawnOnPlayerSpawn)
      PlayerAstronautController.PlayerSpawned -= OnPlayerSpawned;

    if (SpawnOnGameStart)
      GameStateManager.GameStarted -= OnGameStarted;
  }

  private IEnumerator SpawnTimerAsync()
  {
    while (enabled)
    {
      for (float timer = 0; timer < SpawnOnTimer; timer += Time.deltaTime)
      {
        yield return null;
      }

      SpawnEnemy();
      yield return null;
    }
  }

  private void OnPlayerSpawned(PlayerAstronautController player)
  {
    SpawnEnemy();
  }

  private void OnGameStarted()
  {
    SpawnEnemy();
  }

  private void SpawnEnemy()
  {
    Vector3 spawnPos = transform.position + Random.insideUnitSphere.WithY(0) * SpawnRadius;
    Quaternion spawnRot = Quaternion.Euler(0, Random.value * 360, 0);
    AIAstronautController aiAstronaut = Instantiate(_aiPrefab, spawnPos, spawnRot);
  }
}