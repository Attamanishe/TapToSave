using System.Collections;
using System.Collections.Generic;
using Help;
using UnityEngine;

public class ReceptionController : MonoBehaviour
{
    [SerializeField] private Connection _connection;

    void Start()
    {
        this._connection.Init("localhost", OnConnected);
    }

    private void OnConnected()
    {
        LoadLevelHelper.LoadMenu();
    }
}