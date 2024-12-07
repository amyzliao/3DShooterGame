using TMPro;
using UnityEngine;

namespace Code
{
    public class ScoreManager : MonoBehaviour, ICanBeReset
    {
        private static ScoreManager _singleton;

        private int _currentScore;

        private TMP_Text _scoreDisplay;

        // Start is called before the first frame update
        private void Start()
        {
            _currentScore = 0;
            _scoreDisplay = GetComponentInChildren<TMP_Text>();
            UpdateScoreDisplay();
            _singleton = this;
            LevelManager.Register(this);
        }

        public void Reset()
        {
            _currentScore = 0;
            UpdateScoreDisplay();
        }

        public static void AddToScore(int amount)
        {
            _singleton.IncrementScoreInternal(amount);
        }

        private void IncrementScoreInternal(int amount)
        {
            _currentScore += amount;
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            _scoreDisplay.text = $"Score: {_currentScore}";
        }
    }
}