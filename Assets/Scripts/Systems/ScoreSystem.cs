using System;
using UnityEngine;

namespace Scripts.Systems
{

    public class ScoreSystem 
    {

        public event Action<int> OnScoreChanged;

        private int currentLevelScore;

        public ScoreSystem()
        {


        }

        public void ResetLevelScore()
        {
            currentLevelScore = 0;
            OnScoreChanged?.Invoke(currentLevelScore);
        }

        public void AddScore(int points)
        {
            currentLevelScore += points;
            Debug.Log($"ScoreSystem: score = {currentLevelScore}");
            OnScoreChanged?.Invoke(currentLevelScore);
        }

        public int GetCurrentLevelScore()
        {
            return currentLevelScore;
        }
    }
}