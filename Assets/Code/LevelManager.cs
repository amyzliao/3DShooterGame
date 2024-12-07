using System.Collections.Generic;
using Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class LevelManager : MonoBehaviour
{
    private static LevelManager _singleton;

    private List<ICanBeReset> _objectsToReset;

    private void Awake()
    {
        _singleton = this;
        _objectsToReset = new List<ICanBeReset>();
    }

    // Start is called before the first frame update
    private void Start()
    {
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
}