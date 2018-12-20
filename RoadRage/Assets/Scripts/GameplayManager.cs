using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour {

    public List<EnemiesData> EnemiesDataList;

    public List<SidesData> Sides;
    public SidesData LeftSide {  get { return Sides[0]; } }
    public SidesData RightSide {  get { return Sides[1]; } }

    public Road road;

    public float Speed = 2;
    public float RoadSpeedMultiplier = 0.09f;

	// Use this for initialization
	void Start () {
        road.Scroll(Speed * RoadSpeedMultiplier);
        StartCoroutine(PopCouroutine());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PopCouroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            LeftSide.Generator.Pop("schoolgirl", Speed);
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
    }
}
