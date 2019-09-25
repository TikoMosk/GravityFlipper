using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Loads the level.
    /// </summary>
    /// <param name="s">S.</param>      
    public void LoadLevel(string s)
    {
        LoadAsynchronously(s);
    }

    IEnumerator LoadAsynchronously(string s)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(s);

        while(!operation.isDone)
        {
            //float progress = Mathf.Clamp01(s);


            yield return null;
        }
    }
}
