using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDownloader : MonoBehaviour
{
    private static LevelDownloader instance;

    private int levelId;

    public static LevelDownloader Instance { get => instance; set => instance = value; }
    public int LevelId { get => levelId; set => levelId = value; }

    private LevelSerializer serializer;

    private void Awake()
    {
        Debug.Log("kanchuma");
        DontDestroyOnLoad(this);
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }          
    }

    private void Start()
    {
        serializer = FindObjectOfType<LevelSerializer>();
        GetLevelsCountDB(OnLevelCount);
    }

    private void OnLevelCount(bool success, LevelCount count)
    {
        if (success)
            DownloadLevels(count);
    }

    private void DownloadLevels(LevelCount count)
    {
        LevelSelector selector = FindObjectOfType<LevelSelector>();
        for (int i = 1; i <= count.count; i++)
        {
            serializer.LoadLevelFromServer(i);
        }
        selector.AmountOfButtons(count.count);
    }

    private void GetLevelsCountDB(Action<bool, LevelCount> callback)
    {
        serializer.GetLevelsCount(callback);
    }

    public void LoadLevel()
    {
        GameController.Game.ChangeScene("SampleScene");
    }
}
