using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObjects : MonoBehaviour
{
    public float moveSpeed = 6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateItself();
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
    }

    private void RotateItself()
    {
        transform.Rotate(Vector3.up * 75 * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.right * 75 * Time.deltaTime, Space.Self);
    }
}
