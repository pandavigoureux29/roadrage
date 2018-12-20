using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemiesGenerator : MonoBehaviour {

    public GameplayManager manager;

    public Dictionary<string, List<Enemy>> Enemies = new Dictionary<string, List<Enemy>>();
    public List<Enemy> AliveEnemies = new List<Enemy>();

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Enemy> OnClick()
    {
        List<Enemy> enemiesHit = new List<Enemy>();
        foreach(var enemy in AliveEnemies)
        {
            if (enemy.IsHit)
            {
                enemiesHit.Add(enemy);
            }
        }
        return enemiesHit;
    }

    public void Pop(string enemyId, float time)
    {
        var enemy = GetEnemy(enemyId);
        enemy.Pop(time, transform.position);

        AliveEnemies.Add(enemy);
        enemy.OnEnemyDead += OnEnemyDead;
    }

    void OnEnemyDead(object sender, EventArgs eventArgs)
    {
        var enemy = sender as Enemy;
        enemy.OnEnemyDead -= OnEnemyDead;
        AliveEnemies.RemoveAll(x => x.Id == enemy.Id);
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
        enemy.Id = enemyId + Enemies[enemyId].Count;

        go.name = enemy.Id;
        
        Enemies[enemyId].Add(enemy);

        return enemy;
    }

}
