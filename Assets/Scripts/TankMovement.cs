using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {

    public float speed = 12.0f;
    public float turnSpeed = 100.0f;

    private Rigidbody rigid;
    private float movementInput;
    private float turnInput;

	void Awake()
    {
		rigid = GetComponent<Rigidbody>();
	}

    private void OnEnable()
    {
        rigid.isKinematic = false;
        movementInput = 0;
        turnInput = 0;
    }

    private void OnDisable()
    {
        rigid.isKinematic = true;
    }

    void Update()
    {
        movementInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
	}

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInput * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + movement);
    }

    private void Turn()
    {
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigid.MoveRotation(rigid.rotation * turnRotation);
    }
}