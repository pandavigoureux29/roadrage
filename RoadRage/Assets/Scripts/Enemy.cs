using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject SpriteObject;
    
    public enum State { DEAD, MOVING, SHOT }
    public State CurrentState = State.DEAD;

    Transform m_transform;
    /// <summary>
    /// Distance done by the note from its starting point 
    /// </summary>
    protected float m_distanceDone = 0;
    protected Vector3 m_startPos;
    protected float m_startTime;
    protected float m_targetTime;

    // Use this for initialization
    void Start () {
        m_transform = transform;
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
        var Y = ComputePosition();
        /*var moveVector = new Vector2(0, -currentSpeed * Time.deltaTime);
        transform.Translate(moveVector);*/

        var newPos = transform.position;
        newPos.y = Y;
        transform.position = newPos;

        if(transform.position.y <= -6.0f)
        {
            CurrentState = State.DEAD;
        }
    }

    void Dying()
    {

    }

    public void Pop(float timeTarget, Vector2 position)
    {
        transform.position = position;
        CurrentState = State.MOVING;
        m_startPos = position;
        m_startTime = (float) GameplayManager.instance.MusicTimeElapsedMs;
        m_targetTime = timeTarget;
    }


    protected void UpdateSpeed()
    {
        Vector3 pos = m_transform.localPosition;

        //compute note position
        pos.x = ComputePosition();

        //compute total distance done
        m_distanceDone += Mathf.Abs(pos.x - m_transform.localPosition.x);

        m_transform.localPosition = pos;
    }

    float ComputePosition()
    {
        //time of the music
        var t = (float) GameplayManager.instance.MusicTimeElapsedMs;
        //difference betwen target time and start time
        float percent = (t - m_startTime) / (m_targetTime - m_startTime); //(t - ti) / (tf - ti)
        //total distance to go
        float d = GameplayManager.instance.RoadLength;

        float y = m_startPos.y - (d * percent);
        return y;
    }
}
