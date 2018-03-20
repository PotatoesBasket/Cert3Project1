using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    public Camera menuCamera;
    public Camera gameCamera;
    public GameObject player;

    private void Start()
    {
        menuCamera.enabled = true;
        gameCamera.enabled = false;
    }

    private void Update()
    {
        if (player == null)
        {
            menuCamera.enabled = !menuCamera.enabled;
            gameCamera.enabled = !gameCamera.enabled;
        }
    }
}
