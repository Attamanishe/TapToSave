using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Help;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _cameraRoot;

    private List<Player> _players = new List<Player>();

    private float _positionOffset;

    /// <summary>
    /// Spawns the players and set position fro them
    /// </summary>
    /// <param name="count">count to spawn</param>
    /// <param name="groupPosition">position of players</param>
    /// <param name="positionOffset"> the offset players can scattered around  group position</param>
    public void Init(int count, Vector3 groupPosition, float positionOffset)
    {
        _positionOffset = positionOffset;

        for (int i = 0; i < count; i++)
        {
            Player player = Instantiate(this._playerPrefab, transform);

            player.transform.position = new Vector3(
                                                    groupPosition.x + Random.Range(0, positionOffset),
                                                    groupPosition.y,
                                                    groupPosition.z + Random.Range(0, positionOffset));

            player.OnPlayerDeath += p =>
                                   {
                                       this._players.Remove(p);
                                       Destroy(p.gameObject);
                                   };

            this._players.Add(player);
        }
    }

    /// <summary>
    /// Set target to follow for all players
    /// </summary>
    /// <param name="target">target to follow</param>
    public void SetTarget(Vector3 target)
    {
        for (var i = 0; i < this._players.Count; i++)
        {
            Vector3 randomTargetOffset = GetRandomVectorOffset(this._positionOffset);

            this._players[i].SetTarget(target + randomTargetOffset);
        }
    }

    /// <summary>
    /// Set the camera position following to players position
    /// </summary>
    private void FixedUpdate()
    {
        if (this._players.Count > 0)
        {
            this._cameraRoot.position = this._players[0].transform.position;
        }
    }
 
    public Player GetRandomPlayer()
    {
        if (this._players.Count > 0)
        {
            return this._players[Random.Range(0, this._players.Count)];
        }
        return null;
    }

    private Vector3 GetRandomVectorOffset(float maxOffset)
    {
        return new Vector3(
                           Random.Range(0, maxOffset) * (Random.Range(0, 2) > 0 ? -1 : 1),
                           0,
                           Random.Range(0, maxOffset) * (Random.Range(0, 2) > 0 ? -1 : 1));
    }
}