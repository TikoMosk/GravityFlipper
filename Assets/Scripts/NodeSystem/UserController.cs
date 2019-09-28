using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    #region Singleton

    private static UserController instance;
    public static UserController Instance { get => instance; }
    private void Awake() { instance = this; }

    #endregion

    private DeviceData deviceData;
    private UserData userData;
    public DeviceData DeviceData { get => deviceData; }

    private LevelSerializer serializer;

    public void DeviceExists(Action<bool> callback)
    {
        serializer = GameController.Game.LevelController.levelSerializer;
        serializer.GetDeviceData((success, deviceData) =>
        {
            if (deviceData != null)
            {
                if (deviceData.device_id != SystemInfo.deviceUniqueIdentifier)
                {
                    callback(false);
                }
                else
                {
                    this.deviceData = deviceData;
                    callback(true);
                }
            }
        });
    }

    public void UserExists(string username, Action<bool> callback)
    {
        serializer.GetUsername(username, (success, userData) =>
        {
            if (success)
            {
                if (userData == null)
                {
                    callback(false);
                }
                else
                {
                    callback(true);
                }
            }
            else
            {
                callback(true);
            }            
        });
    }

    public void RegisterNewUser(string username)
    {
        serializer.UploadNewUser(username);
    }

}
