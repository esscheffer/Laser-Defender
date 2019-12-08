using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : SingletonMonoBehavior {

    int score = 0;

    public void AddToScore(int scoreValue) {
        this.score += scoreValue;
    }

    public void ResetGame() {
        Destroy(gameObject);
    }

    public int GetScore() {
        return score;
    }
}
