using UnityEngine;

public class PONGController : MonoBehaviour
{
    public Vector3 fightPosition = new Vector3(10.6f, 0, 0);

    private Vector3 startPosition;

    private float flyinDuration = 5.0f;
    public float timeElapsed = 0.0f;
    
    public float SingleAttackCooldown = 6.0f;
    public float RapidAttackCooldown = 9.0f;
    public float ThreeHitAttackCooldown = 12.0f;

    public float _cooldownTime = 0.0f;

    public float minRotation = -115;
    public float maxRotation = -75;
    public float rotationTime = 1.0f;

    public int health = 20;

    public float redTime = 0.0f;
    private bool red = false;

    public GameObject BallPrefab;

    public int currentAttack = -1;
    public int currentAttackData = 0;
    
    private float deathTime = 3;

    private Transform _iris;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        _iris = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && timeElapsed < deathTime)
        {
            timeElapsed += Time.deltaTime;
        } else if (health <= 0)
        {
            GameManager.i.PONGKilled();
            Destroy(this.gameObject);
        }
        else if (health > 0)
        {
            if (GameManager.i.State == GameState.PONGTransition)
            {
                transform.position = Vector3.Lerp(startPosition, fightPosition, timeElapsed / (flyinDuration - 1));

                if (timeElapsed > flyinDuration)
                {
                    GameManager.i.StartPongFight();
                }

                timeElapsed += Time.deltaTime;
            }

            if (GameManager.i.State == GameState.FightingPONG)
            {
                if (_cooldownTime <= 0)
                {
                    currentAttack = currentAttack == -1 ? Random.Range(0, 1) : currentAttack;

                    switch (currentAttack)
                    {
                        case 0:
                            FireSingleAttack();
                            break;
                        // case 1:
                        //     FireRapidAttack();
                        //     break;
                        // case 2:
                        //     FireThreeHitAttack();
                        //     break;
                    }
                }

                _cooldownTime -= Time.deltaTime;
                timeElapsed += Time.deltaTime;
            }

            if (red)
            {
                if (redTime > 0)
                {
                    redTime -= Time.deltaTime;
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.white;
                    red = false;
                }
            }

        }
    }

    void FireSingleAttack()
    {
        float angle = Random.Range(minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        GameObject ball = Instantiate(BallPrefab);
        ball.transform.position = _iris.position;
        BallController c = ball.GetComponent<BallController>();
        c.SetColor(Color.red);
        c.Launch(transform.rotation * Vector3.down, 200);
        
        currentAttack = -1;
        _cooldownTime = SingleAttackCooldown;
    }

    void FireRapidAttack()
    {
        if (currentAttackData < 3 && timeElapsed > 0.5f)
        {
            FireSingleAttack();
            currentAttackData--;
            currentAttack = 1;
            _cooldownTime = 0;
            timeElapsed = 0;
        }

        if (currentAttackData >= 3)
        {
            currentAttackData = 0;
            currentAttack = -1;
        }
    }

    void FireThreeHitAttack()
    {
        currentAttack = -1;
    }
    
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Scoreable"))
        {
            TakeDamage();
            Destroy(otherCollider.gameObject);
        }
    }

    void TakeDamage()
    {
        health -= 1;
        SingleAttackCooldown -= 1f / 25f * 4;

        if (health <= 0)
        {
            Die();
        }
        
        GetComponent<SpriteRenderer>().color = new Color(224f / 255f, 174f / 255f, 174 / 255f);
        red = true;
        redTime = 0.5f;
    }

    void Die()
    {
        timeElapsed = 0;
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<Rigidbody2D>().AddTorque(8000);
        GetComponent<Rigidbody2D>().AddForceX(-10);

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Scoreable"))
        {
            Destroy(ball);
        }
    }
}
