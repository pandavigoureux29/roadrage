using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameplayManager : MonoBehaviour {

    public List<EnemiesData> EnemiesDataList;

    public List<SidesData> Sides;
    public SidesData LeftSide { get { return Sides[0]; } }
    public SidesData RightSide { get { return Sides[1]; } }

    public Road road;

    public float Speed = 2;
    public float RoadSpeedMultiplier = 0.09f;

    public DateTime BeginTime;

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        road.Scroll(Speed * RoadSpeedMultiplier);
        StartCoroutine(PopCouroutine());
        BeginTime = DateTime.UtcNow;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PopCouroutine()
    {
        /*while (true)
        {*/
            yield return new WaitForSeconds(1);
            LeftSide.Generator.Pop("schoolgirl", MusicTimeElapsedMs + 2000);
        //}
    }

    public void OnClick(bool left)
    {
        var side = left ? LeftSide : RightSide;
        side.BlinkAnimation.Play();
    }

    public float RoadLength
    {
        get
        {
            return Mathf.Abs( LeftSide.Aim.transform.position.y - LeftSide.Generator.transform.position.y );
        }
    }

    public float MusicTimeElapsedMs
    {
        get
        {
            return (float) (DateTime.UtcNow - BeginTime).TotalMilliseconds;
        }
    }

    protected static GameplayManager _instance;
    public static GameplayManager instance
    {
        get
        {
            return _instance;
        }
    }

    [System.Serializable]
    public class EnemiesData
    {
        public string Id;
        public GameObject Prefab;
    }

    [System.Serializable]
    public class SidesData
    {
        public EnemiesGenerator Generator;
        public GameObject Aim;
        public Animation BlinkAnimation;
        public Animator FireAnimator;
    }
}
