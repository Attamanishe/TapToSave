using System;
using UnityEngine;

public class Enemy : Player
{
    public event Action<Enemy> OnLostTarget;
    public event Action<Enemy> OnEnemyClicked;
    
    private Player _target;

    /// <summary>
    /// Invokes for setting target to follow
    /// </summary>
    /// <param name="player">target tot follow</param>
    public void SetTarget(Player player)
    {
        if (player != null)
        {
            player.OnPlayerDeath += p =>
                                   {
                                       _target = null;
                                       OnLostTarget.Invoke(this);
                                   };

            this._target = player;
        }
    }

    private void FixedUpdate()
    {
        if (this._target != null)
        {
            //moving to target position
            SetTarget(this._target.transform.position);

            if (Vector3.Distance(transform.position, this._target.transform.position) < 2)
            {
                this._target.Kill();
            }
        }
    }

    private void OnMouseDown()
    {
        if (OnEnemyClicked != null)
        {
            OnEnemyClicked.Invoke(this);
        }
    }
}