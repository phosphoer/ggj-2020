using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
  public IReadOnlyList<PlayerAstronautController> Players => _players;

  [SerializeField]
  private PlayerAstronautController _playerPrefab = null;

  [SerializeField]
  private Transform[] _spawnPoints = null;

  private List<PlayerAstronautController> _players = new List<PlayerAstronautController>();
  private List<bool> _playerJoinedStates = new List<bool>();
  private int _nextSpawnIndex = 0;

  public bool IsPlayerJoined(int playerId)
  {
    return _playerJoinedStates.Count > playerId && _playerJoinedStates[playerId];
  }

  private void Awake()
  {
    Instance = this;
  }

  private void Update()
  {
    if (!Rewired.ReInput.isReady)
    {
      return;
    }

    // Iterate over existing rewired players and spawn their character if they press a button
    for (int i = 0; i < Rewired.ReInput.players.playerCount; ++i)
    {
      Rewired.Player player = Rewired.ReInput.players.GetPlayer(i);
      if (!IsPlayerJoined(i) && player.GetAnyButton())
      {
        AddPlayer(player);
      }
    }
  }

  private PlayerAstronautController AddPlayer(Rewired.Player rewiredPlayer)
  {
    Transform spawnPoint = _spawnPoints[_nextSpawnIndex];
    PlayerAstronautController astroPlayer = Instantiate(_playerPrefab, transform);
    astroPlayer.RewiredPlayer = rewiredPlayer;
    astroPlayer.transform.position = spawnPoint.transform.position;
    astroPlayer.transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);
    _players.Add(astroPlayer);

    _nextSpawnIndex = (_nextSpawnIndex + 1) % _spawnPoints.Length;

    // Set joined state
    if (rewiredPlayer != null)
    {
      while (_playerJoinedStates.Count <= rewiredPlayer.id)
        _playerJoinedStates.Add(false);
      _playerJoinedStates[rewiredPlayer.id] = true;
    }

    return astroPlayer;
  }

  [ContextMenu("Add Debug Player")]
  private void DebugAddPlayer()
  {
    AddPlayer(null);
  }
}