using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    public GameObject menuCamera;
    public GameObject gameCamera;

    private void Start()
    {
        menuCamera.gameObject.SetActive(true);
        gameCamera.gameObject.SetActive(false);
    }

    public void GameCameraOff()
    {
        menuCamera.gameObject.SetActive(true);
        gameCamera.gameObject.SetActive(false);
    }

    public void GameCameraOn()
    {
        menuCamera.gameObject.SetActive(false);
        gameCamera.gameObject.SetActive(true);
    }
}
