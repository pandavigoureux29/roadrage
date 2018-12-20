using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {

    public string Id { get; set; }

    public float ErrorOffsetMs = 150;

    public GameObject SpriteObject;
    public Animator animator;
    
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

    public EventHandler<EventArgs> OnEnemyDead;

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

        var newPos = transform.position;
        newPos.y = Y;
        transform.position = newPos;

        if(transform.position.y <= -6.0f)
        {
            //Die();
        }
    }

    void Dying()
    {
    }
    
    void Die()
    {
        OnEnemyDead?.Invoke(this, null);
        CurrentState = State.DEAD;
    }

    public void Pop(float timeTarget, Vector2 position)
    {
        transform.position = position;
        CurrentState = State.MOVING;
        m_startPos = position;
        m_startTime = (float) GameplayManager.instance.MusicTimeElapsedMs;
        m_targetTime = timeTarget;
    }
    
    public bool IsHit
    {
        get
        {
            var time = (GameplayManager.instance.MusicTimeElapsedMs);
            return time > m_targetTime - ErrorOffsetMs && time < m_targetTime + ErrorOffsetMs;
        }
    }

    public void Hit()
    {
        CurrentState = State.SHOT;
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
