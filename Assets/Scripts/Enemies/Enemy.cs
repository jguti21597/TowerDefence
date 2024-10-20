using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int moveSpeed;
    int health;
    int attackDamage;
    int pointReward;
    float attackRate;
    float attackCooldown;
    MagicType weakenss;
    List<GameObject> wayPointList;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
    protected void assignStats(int ms, int h, int d, int pr, float ar, MagicType w)
    {
        moveSpeed = ms;
        health = h;
        attackDamage = d;
        pointReward = pr;
        attackRate = ar;
        weakenss = w;
    }
    // Update is called once per frame
    protected void Update()
    {
        if (CurrentGameState.getGameState() == GameState.PLAYING)
        {
            move();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Fortress")
        {
            attack(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Fortress")
        {
            attackCooldown = 0;
        }
    }

    public void setWaypointList(List<GameObject> l)
    {
        wayPointList = new List<GameObject>(l.ToList());
    }

    void move()
    {
        if(wayPointList == null)
        {
            return;
        }
        if(wayPointList.Count != 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, wayPointList[0].transform.position, (float)(moveSpeed)*Time.deltaTime);
            if (transform.position == wayPointList[0].transform.position)
            {
                wayPointList.RemoveAt(0);
            }
        }
    }

    void attack(GameObject f)
    {
        attackCooldown+= Time.deltaTime;
        if (attackCooldown >= attackRate)
        {
            f.GetComponent<Fortress>().takeDamage(attackDamage);
            attackCooldown = 0;
        }

    }

    public void takeDamage(int damage, MagicType attackType)
    {
        health -= damage;
        if(attackType==weakenss)
        {
            health-=damage;
        }
        if(health <= 0)
        {
            die();
        }
    }

    void die()
    {
        Skillpoints.addPoints(pointReward);
        Destroy(gameObject);
    }
}
