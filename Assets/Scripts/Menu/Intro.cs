using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Intro : MonoBehaviour
{
    public bool isClickable = false;

    public void Start()
    {

    }

    public void Update()
    {
        StartCoroutine(IntroScene());

        if (Input.GetMouseButtonDown(0) && isClickable == true)
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator IntroScene()
    {
        yield return new WaitForSeconds(2.5f);
        isClickable = true;
    } 
}
