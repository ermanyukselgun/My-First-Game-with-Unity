using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dusman : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField]
    private ParticleSystem explosionBullet;
    [SerializeField]
    private ParticleSystem explosionPlayer;
    public float speed = 10f;

    private bool startMoving = false;
    private float moveZaman = 0;
    private float flipAnimZaman = 0;
    private float shootZaman = 0;
    private float shootAralik;

    public static int score = 0;

    public GameObject healthPowerUp;
    public GameObject mermiPowerUp;
    public GameObject meteor;

    public GameObject dusmanMermi;
    private Transform namlu;
    public LayerMask layer;


    private int dusmanHealth = 2;
    


    void Start()
    {
        namlu = transform.GetChild(5);
        startPosition = transform.position;

        if (SceneManager.GetActiveScene().buildIndex == 2)
            shootAralik = 2;
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            shootAralik = 5;
    }

    private void Update()
    {
        moveZaman += Time.deltaTime;
        if (moveZaman > 7)
        {
            
            startMoving = true;
            SetNewTargetPosition();
            moveZaman = 0;
        }

        MoveOneStep();

        flipAnimZaman += Time.deltaTime;
        if (flipAnimZaman > 5)
        {
            Flip();
            flipAnimZaman = 0;
        }

        shootZaman += Time.deltaTime;

        if (shootZaman > shootAralik && Control())
        {
            int random = Random.Range(1, 100);


            if (random == 3)
            {
                Shoot();
                shootZaman = 0;
            }
        }

    }

    private void MoveOneStep()
    {
        if (startMoving)
        {
            if (transform.position.z >= targetPosition.z)
                transform.position = Vector3.Lerp(transform.position, targetPosition, 0.025f);
            else
                startMoving = false;
        }
    }

    private void SetNewTargetPosition()
    {
        targetPosition = transform.position - Vector3.forward * 1.25f;
    }

    private void Flip()
    {
        animator.SetTrigger("Flip");
    }

    private void ResetFlipTrigger()
    {
        animator.ResetTrigger("Flip");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Mermi")
        {
            

            ParticleSystem fx = Instantiate(explosionBullet, transform.position, Quaternion.identity);

            


            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                
                if (dusmanHealth == 1)
                {
                    Destroy(this.gameObject);
                    score += 20;
                    CreatePowerUp();
                }

                dusmanHealth -= 1;
            }
            else if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                Destroy(this.gameObject);
                score += 20;
                CreatePowerUp();
            }
            

           

            if (!Character_Move.mermiPowerUp)
            {
                Destroy(collision.gameObject);
            }
            
        }
        else if(collision.gameObject.tag == "Player")
        {
            ParticleSystem fx = Instantiate(explosionPlayer, transform.position, Quaternion.identity);
        }
        
    }

    private void CreatePowerUp()
    {
        int random = Random.Range(1, 25);
        if (random  == 3)
        {
            Instantiate(healthPowerUp, transform.position, Quaternion.identity);
        }
        else if(random == 8)
        {
            Instantiate(mermiPowerUp, transform.position, Quaternion.identity);
        }
        else if(random == 12)
        {
            Instantiate(meteor, transform.position, Quaternion.identity);
        }
    }
    private void Shoot()
    {
        Instantiate(dusmanMermi, namlu.position, Quaternion.identity);
    }

    bool Control()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 200, layer))
        {
            return false;
        }
        else
        {
            
            return true;
        }
    }

    

}
