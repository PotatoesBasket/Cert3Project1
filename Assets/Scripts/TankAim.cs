using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAim : MonoBehaviour {

    LayerMask m_LayerMask;

    public GameObject m_aimIndicator;

	// Use this for initialization
	void Start () {
        m_LayerMask = LayerMask.GetMask("Ground");
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            Vector3 lookat = hit.point;
            lookat.y = transform.position.y;
            transform.LookAt(lookat);

            Vector3 indicatorPos = lookat;
            indicatorPos.y = m_aimIndicator.transform.position.y;
            m_aimIndicator.transform.position = indicatorPos;
        }
	}
}
