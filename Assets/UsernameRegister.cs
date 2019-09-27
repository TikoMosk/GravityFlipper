using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameRegister : MonoBehaviour
{
    private UserController userController;
    [SerializeField]
    private InputField newUsernameInput;
    [SerializeField]
    private Text errorText;

    private void Start()
    {
        userController = UserController.Instance;
        //userController.DeviceExists(OnDeviceExists);
        gameObject.SetActive(false);
    }

    private void OnDeviceExists(bool success)
    {
        if (success)
        {
            Debug.Log("Device exists.");
            gameObject.SetActive(false);
        }
    }

    public void SubmitNewUser()
    {
        if (string.IsNullOrEmpty(newUsernameInput.text))
        {

        }
        else
        {
            userController.UserExists(newUsernameInput.text, OnUserExists);
        }
    }

    private void OnUserExists(bool success)
    {
        if (!success)
        {
            userController.RegisterNewUser(newUsernameInput.text);
            gameObject.SetActive(false);
        }
        else
        {
            errorText.text = "Username is already exists.";
        }
    }
}
