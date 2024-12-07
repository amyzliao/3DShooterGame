using TMPro;
using UnityEngine;

namespace Code
{
    public class LivesManager : MonoBehaviour, ICanBeReset
    {
        private static LivesManager _singleton;

        private int _currentLives;

        private TMP_Text _scoreDisplay;

        // Start is called before the first frame update
        private void Start()
        {
            _currentLives = 5;
            _scoreDisplay = GetComponentInChildren<TMP_Text>();
            UpdateLivesDisplay();
            _singleton = this;
            LevelManager.Register(this);
        }

        public void Reset()
        {
            _currentLives = 5;
            UpdateLivesDisplay();
        }

        public static void AddToLives(int amount)
        {
            _singleton.IncrementLivesInternal(amount);
        }

        private void IncrementLivesInternal(int amount)
        {
            _currentLives += amount;
            UpdateLivesDisplay();
        }

        private void UpdateLivesDisplay()
        {
            _scoreDisplay.text = $"Lives: {_currentLives}";
        }
    }
}