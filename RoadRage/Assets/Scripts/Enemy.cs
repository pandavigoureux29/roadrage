using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject SpriteObject;

    float currentSpeed;

    public enum State { DEAD, MOVING, SHOT }
    public State CurrentState = State.DEAD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (CurrentState)
        {
            case State.MOVING:
                Moving();
                break;
            case State.SHOT:
                Dying();
                break;
        }
	}

    void Moving()
    {
        var moveVector = new Vector2(0, -currentSpeed * Time.deltaTime);
        transform.Translate(moveVector);

        if(transform.position.y <= -6.0f)
        {
            CurrentState = State.DEAD;
        }
    }

    void Dying()
    {

    }

    public void Pop(float speed, Vector2 position)
    {
        currentSpeed = speed;
        transform.position = position;
        CurrentState = State.MOVING;
    }
}
