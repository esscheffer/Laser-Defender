using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
    List<Transform> waypoints;
    int pathIndex = 0;

    int waypointIndex = 0;

    void Start() {
        waypoints = waveConfig.GetWaypoints(pathIndex);
        transform.position = waypoints[waypointIndex].position;
        waypointIndex++;
    }

    void Update() {
        if (waypointIndex < waypoints.Count) {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movimentThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movimentThisFrame);
            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        } else {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
    }

    public void SetPathIndex(int pathIndex) {
        this.pathIndex = pathIndex;
    }
}
