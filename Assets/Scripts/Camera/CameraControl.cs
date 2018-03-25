using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {


    private Vector3 desiredPosition;
    private float rotateTimer;
    private float gameTime;

    public float GameTime { get { return gameTime; } }

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rotateTimer += gameTime;
        if (rotateTimer > 360)
            rotateTimer = 0;

        desiredPosition = new Vector3(0, rotateTimer, 0);
        transform.position += desiredPosition;
    }
}
