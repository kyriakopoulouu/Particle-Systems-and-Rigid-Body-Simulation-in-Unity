using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float orbitSpeed = 10f;
    public float rotateSpeed = 30f;
    public float sensitivity = 100f;

    // Update is called once per frame
    void Update()
    {
        float yAxisRotation = Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime;

        float xAxisRotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, yAxisRotation, Space.Self);
        transform.Rotate(Vector3.down, xAxisRotation, Space.Self);

        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(2)) // left mouse button
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, mouseX * sensitivity * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.right, -mouseY * sensitivity * Time.deltaTime, Space.Self);
        }
    }
}
