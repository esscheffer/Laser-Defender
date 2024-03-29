﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<GameObject> pathsPrefabs;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public int GetPathsCount() {
        return pathsPrefabs.Count;
    }

    public List<Transform> GetWaypoints(int pathIndex) {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathsPrefabs[pathIndex].transform) {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTimeBeteweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
}
