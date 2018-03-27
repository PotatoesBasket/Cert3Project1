using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour {

    LayerMask layerMask;
    public GameObject aimIndicator;

	void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
	}
	
	void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 lookat = hit.point;
            lookat.y = transform.position.y;
            transform.LookAt(lookat);

            Vector3 indicatorPos = lookat;
            indicatorPos.y = aimIndicator.transform.position.y;
            aimIndicator.transform.position = indicatorPos;
        }
	}
}
