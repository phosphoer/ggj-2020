using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
  public IReadOnlyList<PlayerAstronautController> Players => _players;

  [SerializeField]
  private PlayerAstronautController _playerPrefab = null;

  private List<PlayerAstronautController> _players = new List<PlayerAstronautController>();

  public bool IsPlayerJoined(int playerId)
  {
    return _players.Count > playerId && _players[playerId] != null;
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
        PlayerAstronautController astroPlayer = Instantiate(_playerPrefab, transform);
        astroPlayer.RewiredPlayer = player;
        _players.Add(astroPlayer);
      }
    }
  }
}