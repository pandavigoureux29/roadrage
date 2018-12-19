using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    public float StartSpeed;
    float currentSpeed;

    Vector2 appliedOffset;

    public SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        appliedOffset = renderer.material.mainTextureOffset;
        //Scroll(StartSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        appliedOffset.y += Time.deltaTime * currentSpeed;
        renderer.material.mainTextureOffset = appliedOffset;
    }

    public void Scroll(float speed)
    {
        currentSpeed = speed;
    }
}
