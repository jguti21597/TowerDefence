using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //ToDo
    /**
     * End of wave reset, add new list, reset/change rate
     */

    //Variable declarations
    float spawnRate = 3; //Number of seconds to enemy spawn
    float spawnTimer = 0;
    List<string> enemies = new List<string>();
    public List<GameObject> wayPointList;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public bool isEmpty()
    {
        if(enemies.Count > 0)
        {
            return false;
        }
        return true;
    }
    public void setList(List<string> e)
    {
        enemies = e;
    }

    void spawnEnemy()
    {
        if (CurrentGameState.getGameState() == GameState.PLAYING)
        {
            if (enemies.Count > 0)
            {
                GameObject newEnemy = Instantiate(GameObject.Find(enemies[0]), transform.position, transform.rotation);
                newEnemy.GetComponent<Enemy>().setWaypointList(wayPointList);
                newEnemy.tag = "Enemy";
                enemies.RemoveAt(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer+=Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0;
            spawnEnemy();
        }

    }
}
