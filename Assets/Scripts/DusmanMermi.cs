using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanMermi : MonoBehaviour
{
    public float moveSpeed = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position -= new Vector3(0, 0, moveSpeed * Time.deltaTime);
    }

    
}
