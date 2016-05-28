using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour 
{

    [SerializeField]
    float forceValue = 4.5f;
    Vector3 startScale;
    [SerializeField]
    AudioClip ballHit;
    AudioSource audSource;
    TrailRenderer trail;

    int xDirection;
    int yDirection;
    int timesHitPaddle;
    Vector2 previousPosition;

    Rigidbody2D myBody;
    SpriteRenderer myRenderer;
	// Use this for initialization
	void Start () 
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.AddForce(new Vector2(forceValue * 50, 50));
        forceValue = forceValue * -1;
        myRenderer = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
        audSource = GetComponent<AudioSource>();
        previousPosition = new Vector2(transform.position.x, transform.position.y);
        trail = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {

    }

    void LateUpdate()
    {
        previousPosition = new Vector2(transform.position.x, transform.position.y);
    }

    int GetDirectionX()
    {
        //Return a 1 if moving right, -1 if left
        if (previousPosition.x > transform.position.x)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    int GetDirectionY()
    {
        //1 if moving up, -1 otherwise
        if(previousPosition.y > transform.position.y)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    void OnCollisionExit2D(Collision2D objCollidedWith)
    {
        //Change color to paddle color when hit
        if (objCollidedWith.gameObject.name == "Player1" || objCollidedWith.gameObject.name == "Player2")
        {
            int xChange = 0;
            int yChange = 0;
            SpriteRenderer otherRenderer = objCollidedWith.gameObject.GetComponent<SpriteRenderer>();
            myRenderer.color = otherRenderer.color;
            if (timesHitPaddle < 9)
            {
                if (GetDirectionX() == 1)
                {
                    xChange = 1;
                }
                else if (GetDirectionX() == -1)
                {
                    xChange = -1;
                }
                if (GetDirectionY() == -1)
                {
                    yChange = -1;
                }
                else if (GetDirectionY() == 1)
                {
                    yChange = 1;
                }
                //Gets longer for how fast the ball gets
                trail.time += 0.05f;
            }
            else
            {
                xChange = 0;
                yChange = 0;
            }
            myBody.velocity = new Vector2(myBody.velocity.x + xChange, myBody.velocity.y + yChange);
            timesHitPaddle++;
        }
        //Change wall color to the ball's color
        if(objCollidedWith.gameObject.tag == "Walls")
        {
            SpriteRenderer otherRenderer = objCollidedWith.gameObject.GetComponent<SpriteRenderer>();
            otherRenderer.color = myRenderer.color;
        }

        //Need to increase the speed of the ball if hit by a paddle
    }

    void OnCollisionEnter2D(Collision2D objCollideWith)
    {
        StartCoroutine(ScaleBall(0.3f));
        PlaySound(ballHit);
    }

    //This function will scale the ball up and down using easing when it hits a wall or paddle(same thing)
    IEnumerator ScaleBall(float time)
    {
        float elapsedTime = 0;
        Vector3 targetScale = new Vector3(3, 3);
        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(targetScale, startScale, elapsedTime/time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void Stop()
    {
        myBody.velocity = Vector2.zero;
    }

    public void Reset()
    {
        myBody.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0);
        myBody.AddForce(new Vector2(forceValue * 50, 50));
        forceValue = forceValue * -1;
    }

    public void PlaySound(AudioClip clip)
    {
        audSource.clip = clip;
        audSource.Play();
    }
}
