using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int multiplier = 1;
    int streak = 0;

    void Start() {
        PlayerPrefs.SetInt("Score", 0);
    }

    void Update() {
        
    }

    void OnTriggerEnter(Collider col) {
        Destroy(col);
    }

    public void AddStreak() {
        streak++;

        if(streak >= 24)
            multiplier = 4;
        else if(streak >= 16)
            multiplier = 3;
        else if(streak >= 8)
            multiplier = 2;
        else
            multiplier = 1;

        UpdateGUI();
    }

    public void ResetStreak() {
        streak = 0;
        multiplier = 1;
        UpdateGUI();
    }

    void UpdateGUI() {
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("Mult", multiplier);
    }

    public int GetScore() {
        return 100 * multiplier;

    }
}
