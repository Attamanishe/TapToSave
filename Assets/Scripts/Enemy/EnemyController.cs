using System;
using System.Collections;
using System.Collections.Generic;
using Help;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : Singleton<EnemyController>
{
    public event Action OnAllPlayersKilled;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _maxOffset;

    private List<Enemy> _enemies = new List<Enemy>();

    /// <summary>
    /// Starts the spawn
    /// </summary>
    /// <param name="timeDelay">spawn delay</param>
    /// <param name="spawnCount">count of spawned enemies by one iteration</param>
    public void Init(int timeDelay, int spawnCount)
    {
        StartCoroutine(SpawnEnemy(timeDelay, spawnCount));
    }

    /// <summary>
    /// Kill the concrete enemy
    /// </summary>
    /// <param name="enemy">enemy to kill</param>
    public void KillEnemy(Enemy enemy)
    {
        this._enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    /// <summary>
    /// Kill all enemies
    /// </summary>
    public void KillAllEnemies()
    {
        for (var i = 0; i < this._enemies.Count; i++)
        {
            Destroy(this._enemies[i].gameObject);
        }

        this._enemies.Clear();
    }

    /// <summary>
    /// Spawn group of enemies around position
    /// </summary>
    /// <param name="count">count for spawn</param>
    /// <param name="positionAround">position with will be spawn around</param>
    /// <param name="minOffset">min distance from position around to spawn position</param>
    private void SpawnGroup(int count, Vector3 positionAround, float minOffset)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy enemy = Instantiate(this._enemyPrefab, transform);
            enemy.transform.position = GetRandomPositionDelayed(positionAround, minOffset);
            enemy.SetTarget(PlayerController.Instance.GetRandomPlayer());

            enemy.OnLostTarget += e =>
                                  {
                                      //get new target for enemy
                                      Player player = PlayerController.Instance.GetRandomPlayer();

                                      if (player == null)
                                      {
                                          //all players killed - detect the end of game
                                          OnAllPlayersKilled.Invoke();
                                          StopAllCoroutines();
                                      }
                                      else
                                      {
                                          e.SetTarget(PlayerController.Instance.GetRandomPlayer());
                                      }
                                  };

            this._enemies.Add(enemy);
        }
    }

    /// <summary>
    /// Finds the position in the world far enough from world point
    /// </summary>
    /// <param name="center">position from which to move away</param>
    /// <param name="minDistance">min distance of moving away</param>
    /// <returns>random position</returns>
    private Vector3 GetRandomPositionDelayed(Vector3 center, float minDistance)
    {
        //TODO: remove long while
        Vector3 position;

        do
        {
            position = new Vector3(
                                   Random.Range(-this._maxOffset, this._maxOffset),
                                   0,
                                   Random.Range(-this._maxOffset, this._maxOffset));
        } while (Vector3.Distance(position, center) < minDistance);

        return position;
    }

    /// <summary>
    /// Coroutine for spawn enemies 
    /// </summary>
    /// <param name="delay">delay between spawns groups</param>
    /// <param name="spawnCount">count of spawned enemies by one iteration</param>
    /// <returns></returns>
    private IEnumerator SpawnEnemy(float delay, int spawnCount)
    {
        Player player;
        float time = 0;

        do
        {
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }

            //get the random player for detect the position enemies should spawn around
            player = PlayerController.Instance.GetRandomPlayer();

            if (player != null)
            {
                SpawnGroup(spawnCount, player.transform.position, 20);
            }

            time = delay;
        } while (player != null);
    }
}