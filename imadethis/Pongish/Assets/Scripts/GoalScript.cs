using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

    [SerializeField]
    int attackingPlayer;
    [SerializeField]
    GameManagerScript gameMan;
    [SerializeField]
    ParticleSystem partSys;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.name == "Ball")
        {
            gameMan.GoalScored(attackingPlayer);
            partSys.transform.position = new Vector2(partSys.transform.position.x, other.transform.position.y);
            partSys.Play();
        }
    }
}
