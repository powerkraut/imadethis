using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour 
{

    [SerializeField]
    bool isPlayerTwo;
    [SerializeField]
    float speed = 0.2f;
    Transform myTransform;
    int direction = 0;
    float previousPositionY;
    CameraShakeScript camShake;
    Vector3 screenPoint;
    Vector3 mouseOffset;
    TrailRenderer trail;
    SpriteRenderer render;

	// Use this for initialization
	void Start () 
    {
        myTransform = transform;
        previousPositionY = myTransform.position.y;
        camShake = GetComponent<CameraShakeScript>();
        trail = GetComponent<TrailRenderer>();
        render = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        screenPoint = Camera.main.WorldToScreenPoint(myTransform.position);
        mouseOffset = myTransform.position - Camera.main.ScreenToWorldPoint(new Vector3(myTransform.position.x, Input.mousePosition.y, screenPoint.z));
        trail.material.color = render.color;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (isPlayerTwo)
        {
            if (Input.GetKey("o"))
            {
                MoveUp();
            }
            else if (Input.GetKey("l"))
            {
                MoveDown();
            }
        }
        else
        {
            if (Input.GetKey("q"))
            {
                MoveUp();
            }
            else if (Input.GetKey("a"))
            {
                MoveDown();
            }
        }

        if(previousPositionY > myTransform.position.y)
        {
            direction = -1;
        }
        else if (previousPositionY < myTransform.position.y)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
	}

    void OnCollisionExit2D(Collision2D other)
    {
        camShake.Shake();
    }

    void LateUpdate()
    {
        previousPositionY = myTransform.position.y;
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(myTransform.position.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + mouseOffset;
        myTransform.position = currentPosition;
    }

    //move paddle up by certain speed
    void MoveUp()
    {
        myTransform.position = new Vector2(myTransform.position.x, myTransform.position.y + speed);
    }
    //move paddle down by certain speed
    void MoveDown()
    {
        myTransform.position = new Vector2(myTransform.position.x, myTransform.position.y - speed);
    }
   
}
