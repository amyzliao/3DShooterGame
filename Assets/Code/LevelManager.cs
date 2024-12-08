using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class LevelManager : MonoBehaviour
{
    private static LevelManager _singleton;

    private List<ICanBeReset> _objectsToReset;
    public GameObject gameOverScreen;

    private void Awake()
    {
        _singleton = this;
        _objectsToReset = new List<ICanBeReset>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _singleton.ResetGameInternal();
        // if no more enemies, proceed to next level
        if (FindObjectsOfType<EnemyTree>().Length == 0)
        {
            LoadNextLevel();
        }
        if (LivesManager.GetLives() <= 0)
        {
            StartCoroutine(GameOver());
        }
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
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            ResetGame();
        }
    }

    private IEnumerator GameOver()
    {
        // Display the Game Over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Wait for the specified delay
        yield return new WaitForSeconds(2f);

        // Reload the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}