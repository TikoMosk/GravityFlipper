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

    private LevelSelector selector;
    private LevelSerializer serializer;

    private void Awake()
    {
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
        GetLevelsCountDB(OnLevelCountCallback);
        GetCompleteLevels(OnCompleteLevelsCallback);
    }

    private void OnLevelCountCallback(bool success, LevelCount count)
    {
        if (success)
            DownloadLevels(count);
    }

    private void OnCompleteLevelsCallback(bool success, LevelCount count)
    {
        selector = FindObjectOfType<LevelSelector>();
        if (success)
        {
            //todo
            if (count.level_id > 12)
            {
                count.level_id = 12;
            }
            selector.AmountOfButtons(count.level_id);
        }

    }

    private void GetLevelsCountDB(Action<bool, LevelCount> callback)
    {
        serializer.GetLevelsCount(callback);
    }

    private void GetCompleteLevels(Action<bool, LevelCount> callback)
    {
        serializer.GetCompleteLevels(callback);
    }

    private void DownloadLevels(LevelCount count)
    {
        selector = FindObjectOfType<LevelSelector>();
        for (int i = 1; i <= count.level_id; i++)
        {
            serializer.LoadLevelFromServer(i);
        }
    }

    public void LoadLevel()
    {
        GameController.Game.ChangeScene("SampleScene");
    }
}
