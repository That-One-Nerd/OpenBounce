using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//im fucking losing my mind
public class TriggerLogic : MonoBehaviour
{
    //[SerializeField] private RectTransform triggerRectTransform; // The trigger is a UI element (RectTransform)
    [SerializeField] public int points;//the specified points given or taken away for the specific trigger
    [SerializeField] public double factor;//same as prev but mult. 1.0 for nothing
    [SerializeField] public double factorPerRound;//multiply points value by this much per round played
    [SerializeField] public bool increaseMultPerRound = false;//if thhis item increases the mult per round, then  truye
    [SerializeField] public double cooldownTime;//the length of the cooldown for the trigger

    public AudioClip triggerSound;

    private AudioSource audioSource;

    public UnityEvent WhenTriggerEnter;

    public double timer = 0;


    void Start()
    {
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D ball)
    {
        // Check if the object triggering the event is a "Chip" and ensure cooldown is done
        if (WhenTriggerEnter != null && ball.gameObject.tag == "Chip" && timer <= 0)
        {
            WhenTriggerEnter.Invoke();
            restartCooldown();
            if (triggerSound != null)
            {
                AudioManager.Instance.PlayOneShot(triggerSound);  // Play the sound via AudioManager
            }
            TriggerActionBasedOnTag(ball);
        }
    }



    private void TriggerActionBasedOnTag(Collider2D ball)
    {
        // Logic based on trigger tag
        if (gameObject.CompareTag("Brick"))
        {
            earnPointsForBounces(ball.gameObject);
        }
        if (gameObject.CompareTag("Star") || gameObject.CompareTag("House"))
        {
            destroyTheBall(ball.gameObject);
        }
        if (gameObject.CompareTag("Portal"))
        {
            teleportTop(ball.gameObject);
        }
        if (gameObject.CompareTag("Jumprope"))
        {
            teleportUp(ball.gameObject);
        }
        if (gameObject.CompareTag("Parachute"))
        {
            flipGravity(ball);
        }
    }

    public void multiplyPointsValue()
    {
        if (increaseMultPerRound)
        {
            points = (int) (points * factorPerRound);
        }
    }

    public void multiplyPointsValue2()
    {
        points = (int)(points * factorPerRound);
    }

    public void restartCooldown()
    {
        timer = cooldownTime;

    }
    
    //teleport ball to the top of the level
    public void teleportTop(GameObject ball)
    {
        Vector3 newPos = new Vector3(ball.transform.position.x, 4.5f, ball.transform.position.z); // Example Y position
        ball.transform.position = newPos;
    }

    //teleport up 2 rows
    public void teleportUp(GameObject ball)
    {
        ball.transform.position += new Vector3(0, 2f, 0); // Move 2 units upward
    }

    //increase score by set point amount
    public void earnPoints()
    {
        GameStateManager.Instance._totalScore += points;
    }

    public void losePoints()
    {
        GameStateManager.Instance._totalScore -= points;
    }

    //multiply score by point amount
    public void multiplyPoints()
    {
        GameStateManager.Instance._totalScore = (int)(GameStateManager.Instance._totalScore * factor);
    }

    public void gamble()
    {
        int randomNumber = UnityEngine.Random.Range(0, 2); // 0 or 1


        if (randomNumber == 1)
        {
            earnPoints();
        }
        else
        {
            losePoints();
        }
    }

    //do this at the end of each round if it's an interest earning item like sprout
    public void increaseMultOfTrigger()
    {
        points = (int) (points * factor);
    }

    public void flipGravity(Collider2D ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale *= -1;
        }
    }


    public void earnPointsForBounces(GameObject ball)
    {
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            GameStateManager.Instance._totalScore += points * ballScript.wallBounces;
        }
    }


    public void onBallDroppedEarnPoints()
    {
        if (gameObject.CompareTag("Tarriff"))
        {
            GameStateManager.Instance._totalScore += points;
        }
    }

    public void destroyTheBall(GameObject ball)
    {
        Destroy(ball); // This triggers OnDestroy on the ball script
    }

    public void earnPercentOfScore()
    {
        GameStateManager.Instance._totalScore += (int)(GameStateManager.Instance._totalScore * factor);
    }

    public void losePercentOfScore()
    {
        GameStateManager.Instance._totalScore -= (int)(GameStateManager.Instance._totalScore * factor);
    }
}
