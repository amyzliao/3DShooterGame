using System.Collections;
using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _singleton;
    public GameObject gameOverScreen;
    public GameObject youWinScreen;

    private List<ICanBeReset> _objectsToReset;

    private void Awake()
    {
        _singleton = this;
        _objectsToReset = new List<ICanBeReset>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (youWinScreen != null) youWinScreen.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _singleton.ResetGameInternal();
        // if no more enemies, proceed to next level - if already on last level, display you win screen
        if (FindObjectsOfType<EnemyTree>().Length == 0)
        {
            if (IsLastLevel())
                StartCoroutine(ShowYouWinScreen());
            else
                LoadNextLevel();
        }

        if (LivesManager.GetLives() <= 0) StartCoroutine(GameOverDelayed());
    }

    public static bool Register(ICanBeReset obj)
    {
        return _singleton.RegisterInternal(obj);
    }

    private bool RegisterInternal(ICanBeReset obj)
    {
        _objectsToReset.Add(obj);
        return true;
    }

    public static void GameOver()
    {
        _singleton.GameOverInternal();
    }

    private void GameOverInternal()
    {
        StartCoroutine(GameOverDelayed());
    }

    public static void ResetGame()
    {
        _singleton.ResetGameInternal();
    }

    private void ResetGameInternal()
    {
        foreach (var obj in _objectsToReset)
            obj.Reset();
    }

    public void LoadNextLevel()
    {
        var currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        var nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextLevelIndex);
        else
            ResetGame();
    }

    private IEnumerator GameOverDelayed()
    {
        // display the Game Over screen
        if (gameOverScreen != null) gameOverScreen.SetActive(true);

        // wait for the specified delay
        yield return new WaitForSeconds(2f);

        // reload the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ShowYouWinScreen()
    {
        // display the You Win screen
        if (youWinScreen != null) youWinScreen.SetActive(true);

        // wait for the specified delay
        yield return new WaitForSeconds(2f);

        // reload the first level
        SceneManager.LoadScene(0);
    }

    private bool IsLastLevel()
    {
        return SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;
    }
}