using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start() {
        do {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves() {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SapwnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SapwnAllEnemiesInWave(WaveConfig waveConfig) {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++) {
            for (int pathCount = 0; pathCount < waveConfig.GetPathsCount(); pathCount++) {
                var newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWaypoints(pathCount)[0].transform.position,
                    Quaternion.identity);
                newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                newEnemy.GetComponent<EnemyPathing>().SetPathIndex(pathCount);
                
            }
            yield return new WaitForSeconds(waveConfig.GetTimeBeteweenSpawns());
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
