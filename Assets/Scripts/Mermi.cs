using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermi : MonoBehaviour
{
    public float moveSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
    }
}
