using UnityEngine;
using System.Collections;

public class BallTrailScript : MonoBehaviour {

    TrailRenderer trail;
    SpriteRenderer renderer;

    // Use this for initialization
	void Start () {
        trail = GetComponent<TrailRenderer>();
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateTrailColors();
	}

    void UpdateTrailColors()
    {
        Color ballColor = renderer.color;
        trail.material.color = ballColor;
    }
}
