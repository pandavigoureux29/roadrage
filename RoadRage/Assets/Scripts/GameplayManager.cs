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
    public float TotalTime = 0;

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
        TotalTime += Time.deltaTime;
	}

    IEnumerator PopCouroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            LeftSide.Generator.Pop("schoolgirl", MusicTimeElapsedMs + 2000);
        }
    }

    public void OnClick(bool left)
    {
        var side = left ? LeftSide : RightSide;

        var list = side.Generator.OnClick();
        foreach (var enemyHit in list)
        {
            enemyHit.Hit(left);
        }

        if (list.Count > 0)
        {
            side.BlinkAnimation.Play();
            side.FireAnimator.SetTrigger("shoot");
        }
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
            return TotalTime * 1000;
            //return (float) (DateTime.UtcNow - BeginTime).TotalMilliseconds;
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
