using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    public event Action<Player> OnPlayerDeath;
    [SerializeField] private float _speed;

    private NavMeshAgent _agent;
    private bool _isAlive = true;

    private void Awake()
    {
        this._agent = GetComponent<NavMeshAgent>();
        this._agent.speed = this._speed;
    }

    /// <summary>
    /// Set the position of player destination
    /// </summary>
    /// <param name="target">destination point</param>
    public void SetTarget(Vector3 target)
    {
        this._agent.SetDestination(target);
    }

    /// <summary>
    /// Kill the player
    /// </summary>
    public void Kill()
    {
        if (_isAlive)
        {
            _isAlive = false;
            OnPlayerDeath.Invoke(this);
        }
    }
}