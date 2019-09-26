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

        serializer = FindObjectOfType<LevelSerializer>();
        DownloadLevels();
    }

    void DownloadLevels()
    {

        LevelSelector selector = FindObjectOfType<LevelSelector>();
        for (int i = 1; i <= 12; i++)
        {
            serializer.LoadLevelFromServer(i);
        }
        selector.AmountOfButtons(12);
    }

    public void LoadLevel()
    {
        GameController.Game.ChangeScene("SampleScene");
    }
}
