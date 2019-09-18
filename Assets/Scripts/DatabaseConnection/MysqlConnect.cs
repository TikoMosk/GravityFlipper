using UnityEngine;
using System.Collections;

public class MysqlConnect : MonoBehaviour
{

    private void Connect()
    {
        WWW connect = new WWW(url);

        if (connect.isDone)
        {
            Debug.Log(connect.text);
        }
        else if (connect.error == null)
        {
            Debug.Log("Error");
        }
    }

    private void OnGUI()
    {

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 20), "Connect"))
        {
            Connect();
        }
    }
}
