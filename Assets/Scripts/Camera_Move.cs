using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform follow;
    public float smoothing = 0.125f;
    //public Vector3 distance = new Vector3(0,2f,-3f) ;
    private Vector3 distance;
    

    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position - follow.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        Follow_Character();
    }

    void Follow_Character()
    {
        if (Character_Move.game_over == true)
        {
            transform.position = transform.position;
        }
        else
        {
            
            Vector3 camera_position = follow.position + distance;
            Vector3 smooth_position = Vector3.Lerp(transform.position,camera_position, smoothing);
            transform.position = smooth_position;          
            
        }
    }
}
