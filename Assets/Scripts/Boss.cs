using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    public GameObject bossMermi;
    private Transform namlu,namlu1;

    private float shootTime;
    public HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;
    

    [SerializeField]
    private ParticleSystem explosionBullet;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private ParticleSystem bossBigExplosion;
    void Start()
    {
        namlu = transform.GetChild(6);
        namlu1 = transform.GetChild(5);
        waitTime = startWaitTime;
        randomSpot = Random.Range(0,moveSpots.Length);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        panel.SetActive(false);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,moveSpots[randomSpot].position,speed * Time.deltaTime);

        if (Vector3.Distance(transform.position,moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                shootTime += Time.deltaTime;
                if(shootTime > 1)
                {
                    Shoot();
                    shootTime = 0;
                }
                
                waitTime -= Time.deltaTime;
            }
        }
        TakeDamage();
        if (currentHealth == 0)
        {
            panel.SetActive(true);
            Instantiate(bossBigExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Shoot()
    {
        Instantiate(bossMermi, namlu.position, Quaternion.identity);
        Instantiate(bossMermi, namlu1.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Mermi")
        {
            currentHealth -= 5;
            ParticleSystem fx = Instantiate(explosionBullet, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Debug.Log(currentHealth);
        }
    }
    public void TakeDamage()
    {
        healthBar.SetHealth(currentHealth);

    }
}
