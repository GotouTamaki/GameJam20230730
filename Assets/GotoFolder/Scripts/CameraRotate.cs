using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Vector3 angle;

    void Start()
    {
        angle = this.gameObject.transform.localEulerAngles;
    }

    void Update()
    {
        angle.y += Input.GetAxis("Mouse X");

        angle.x -= Input.GetAxis("Mouse Y");

        this.gameObject.transform.localEulerAngles = angle;
    }
}
