using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameRegister : MonoBehaviour
{
    public GameObject usernameField;

    private UserController userController;
    private string newUsername;

    private void Start()
    {
        userController = UserController.Instance;
        userController.DeviceExists(OnDeviceExists);
    }

    private void OnDeviceExists(bool success)
    {
        if (success)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnUserExists(bool success)
    {
        newUsername = usernameField.GetComponent<Text>().text;
        if (!success)
        {
            userController.RegisterNewUser(newUsername);
            gameObject.SetActive(false);
        }
    }
}
