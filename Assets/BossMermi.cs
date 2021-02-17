using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMermi : MonoBehaviour
{
    public float moveSpeed = 20;
    private Vector3 playerLastPos;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerLastPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2.2f);
        }
        

        Destroy(this.gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,playerLastPos, moveSpeed * Time.deltaTime);
        
    }

}
