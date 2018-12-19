using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour {

    public List<EnemiesData> EnemiesDataList;

    public List<EnemiesGenerator> Generators;

    public Road road;

    public float Speed = 2;
    public float RoadSpeedMultiplier = 0.09f;

	// Use this for initialization
	void Start () {
        road.Scroll(Speed * RoadSpeedMultiplier);
		Generators.First().Pop("schoolgirl", Speed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [System.Serializable]
    public class EnemiesData
    {
        public string Id;
        public GameObject Prefab;
    }
}
