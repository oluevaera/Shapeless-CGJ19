using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }

            return instance;
        }
        private set => instance = value;
    }

    [SerializeField] private List<Transform> spawnPoints;

    private List<PlayerInput> players = new List<PlayerInput>();

    private void Awake()
    {
        instance = this;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoints[players.Count];
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        var spawnPoint = GetSpawnPoint();
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;

        players.Add(player);
    }

    public void OnPlayerLeft(PlayerInput player)
    {
        players.Remove(player);
    }
}
