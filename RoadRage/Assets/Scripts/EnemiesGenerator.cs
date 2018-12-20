using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemiesGenerator : MonoBehaviour {

    public Dictionary<string, List<Enemy>> Enemies = new Dictionary<string, List<Enemy>>();
    public GameplayManager manager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pop(string enemyId, float time)
    {
        var enemy = GetEnemy(enemyId);
        enemy.Pop(time, transform.position);
    }

    Enemy GetEnemy(string enemyId)
    {
        if (Enemies.ContainsKey(enemyId))
        {
            var existing = Enemies[enemyId].FirstOrDefault(x => x.CurrentState == Enemy.State.DEAD);
            if(existing != null)
            {
                return existing;
            }
        }
        else
        {
            Enemies[enemyId] = new List<Enemy>();
        }

        var data = manager.EnemiesDataList.FirstOrDefault(x => x.Id == enemyId);

        GameObject go = Instantiate(data.Prefab);
        go.transform.SetParent(transform);

        var enemy = go.GetComponent<Enemy>();
        
        Enemies[enemyId].Add(enemy);

        return enemy;
    }

}
