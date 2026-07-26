using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D body;
    private float forcex;
    private float forcey;
    private bool towardsplayer;
    private AudioSource audioSource;
    public AudioClip bonksound,playerscoreSound,  computerscoreSound;

   [HideInInspector] public float ballposy;

    private ScoreManager scoreManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        int roll = Random.Range(0, 2);
        forcex = 5f;
        if (roll ==0)
        {
             towardsplayer = true;
        }
        else
        {
            towardsplayer = false;  
        }
        forcey = Random.Range(-2,2);
        moveball();
    }
    void moveball()
    {
        if (towardsplayer == true)
        {
            body.linearVelocity = new Vector2(forcex, forcey);
        }
        else
        {
            body.linearVelocity = new Vector2(-forcex, forcey);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Top")
        {
            body.linearVelocity += new Vector2(0, 1).normalized;
        }
        else if (collision.collider.gameObject.name == "Down")
        {
            body.linearVelocity += new Vector2(0, -1).normalized;
        } 
        else if(collision.collider.gameObject.name == "Top Wall")
        {
            body.linearVelocity += new Vector2(0, -1.25f).normalized;
        }
        else if (collision.collider.gameObject.name == "Bottom Wall")
        {
            body.linearVelocity += new Vector2(0, 1.25f).normalized;
        }
        if (audioSource.clip != bonksound)
        {
            audioSource.clip = bonksound;
        }
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.name == "left wall")
        {
            scoreManager.IncreasePlayerScore();
            
            audioSource.clip = playerscoreSound;
            audioSource.Play();

            towardsplayer = true;
        } else if (trigger.name == "right wall")
        { 
            scoreManager.IncreaseComputerScore();

            audioSource.clip = computerscoreSound;
            audioSource.Play();

            towardsplayer = false;
        }
        ResetBall();
    }
    // Update is called once per frame
    void Update()
    {
        ballposy = transform.position.y;
    }
    void ResetBall()
    {
        transform.position = new Vector2(0, 0);
        moveball();
    }
}

