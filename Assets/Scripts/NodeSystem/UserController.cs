using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UserExists(Action<bool> callback)
    {
        serializer.GetUsername((success, userData) =>
        {
            if (userData.username != SystemInfo.deviceUniqueIdentifier)
            {
                callback(false);
            }
            else
            {
                this.userData = userData;
                callback(true);
            }

        });
    }

    public void RegisterNewUser(string username)
    {
        serializer.UploadNewUser(username);
    }

}
