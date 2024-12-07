using System.Collections.Generic;
using Code;
using UnityEngine;

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
}