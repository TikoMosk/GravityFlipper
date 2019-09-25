using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDownloader : MonoBehaviour
{
    private static LevelDownloader instance;

    private int levelId;

    public static LevelDownloader Instance { get => instance; set => instance = value; }
    public int LevelId { get => levelId; set => levelId = value; }

    private void Awake() {
        DontDestroyOnLoad(this);
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }
    public void DownloadAndLoad() {
        string path = "level" + levelId + ".json";
        GameController.Game.ChangeScene("SampleScene");
    }
}
