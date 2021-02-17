using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Character_Move : MonoBehaviour
{
    public float speed = 8f;
    public float rot_speed = 0.1f;
    public static bool game_over = false;
    public GameObject namlu,namlu1;
    public GameObject mermi;
    public ParticleSystem FireEffect;
    public ParticleSystem FireEffect2;

    private float shootZaman = 0;
    private float shootAralik;

    private int score = 0;
    private int maxHealth = 100;
    private  int currentHealth;
    public HealthBar healthBar;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;

    private int currentScene;

    private int toplamScore;

    public static bool mermiPowerUp = false;
    private float mermiPowerUpTime = 0;
    public GameObject enemies;

    public static int howManyEnemiesLeft;
    
    [SerializeField]
    private ParticleSystem explosionBullet;

    [SerializeField]
    private ParticleSystem playerBigExplosion;

    private float restartTime;
    // Start is called before the first frame update
    void Start()
    {
        mermiPowerUp = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentScene = SceneManager.GetActiveScene().buildIndex;

        levelText.text = "Level " + currentScene.ToString();

        if (SceneManager.GetActiveScene().buildIndex == 2)
            shootAralik = 1;
        else if (SceneManager.GetActiveScene().buildIndex == 1)
            shootAralik = .25f;
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            shootAralik = .6f;

    }

    // Update is called once per frame
    void Update()
    {
        howManyEnemiesLeft = enemies.transform.childCount;


        score = Dusman.score;
        scoreText.text = score.ToString();
        toplamScore = score;
        
        
        TakeDamage();
        
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }

        if (mermiPowerUp)
        {
            mermiPowerUpTime += Time.deltaTime;

        }

        if (mermiPowerUpTime > 3)
        {
            mermiPowerUpTime = 0;
            mermiPowerUp = false;
        }

        if (currentHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        NextScene();
    }
    private void FixedUpdate()
    {
        Char_Move();

        shootZaman += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && shootZaman > shootAralik)
        {
            FireEffect.Play();
            FireEffect2.Play();
            shootZaman = 0;
            Shoot();
        }
    }
    void Char_Move()
    {
        float time_fix = speed * Time.deltaTime;
        float yatay = Input.GetAxis("Horizontal");
        transform.position += new Vector3(yatay * time_fix, 0, 0);

        transform.position = new Vector3(   Mathf.Clamp(transform.position.x, -7, 11),
                                            transform.position.y,
                                            transform.position.z);




        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 30f), Time.time * rot_speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -30f), Time.time * rot_speed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.time * rot_speed);

        }

        
    }

    void NextScene()
    {
        if (howManyEnemiesLeft == 0 && SceneManager.GetActiveScene().buildIndex != 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void Shoot()
    {
        Instantiate(mermi, namlu.transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(mermi, namlu1.transform.position, Quaternion.Euler(0, 0, 0));
    }

    public void TakeDamage()
    {
        healthBar.SetHealth(currentHealth);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DusmanMermi")
        {
            ParticleSystem fx = Instantiate(explosionBullet, collision.transform.position + Vector3.up , Quaternion.identity);
            Destroy(collision.gameObject);
            currentHealth -= 10;
            
        }
        if (collision.gameObject.tag == "Health")
        {
            Destroy(collision.gameObject);
            currentHealth += 15;
        }

        if (collision.gameObject.tag == "MermiPowerup")
        {
            mermiPowerUp = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Meteor")
        {
            currentHealth -= 25;
            ParticleSystem fx = Instantiate(explosionBullet, collision.transform.position + Vector3.up, Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "BossMermi")
        {
            ParticleSystem fx = Instantiate(explosionBullet, collision.transform.position + Vector3.up, Quaternion.identity);
            Destroy(collision.gameObject);
            currentHealth -= 10;

        }
    }
}
