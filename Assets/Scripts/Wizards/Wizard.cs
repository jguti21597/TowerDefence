using UnityEngine;

public class Wizard : MonoBehaviour
{
    string wizName;
    int attackDamage;
    float range;
    float attackRate;
    float attackCooldown;
    int cost;
    float lineDuration = 0.1f;
    float lineCooldown = 0;
    private LineRenderer lineRenderer;
    MagicType magicType;

    public void setLineRenderer(LineRenderer lr)
    {
        lineRenderer = lr;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }
    public int getCost()
    {
        return cost;
    }

    public string getName() 
    {
        return wizName;
    }
    // Update is called once per frame
    protected void Update()
    {
        if (CurrentGameState.getGameState() == GameState.PLAYING)
        {
            attack();
        }
    }

    protected void assignStats(string name,int ad, int r, float ar, int c, MagicType mt)
    {
        wizName = name;
        attackDamage = ad;
        range = r;
        attackRate = ar;
        cost = c;
        magicType = mt;

    }
    void attack()
    {
        if(lineRenderer == null)
        {
            return;
        }
        GameObject enemy = getClosestInRange();
        if (enemy != null)
        {
            attackCooldown += Time.deltaTime;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, enemy.transform.position);
            if (attackCooldown >= attackRate)
            {
                enemy.GetComponent<Enemy>().takeDamage(attackDamage, magicType);
                attackCooldown = 0;
                lineRenderer.enabled = true;
            }
        }
        else
        {
            attackCooldown = 0;
        }
        if (lineRenderer.enabled)
        {
            lineCooldown += Time.deltaTime;

            if (lineCooldown >= lineDuration)
            {
                lineCooldown = 0;
                lineRenderer.enabled = false;
            }
        }
    }
    private float getScalar(Vector3 vector)
    {
        float value = 0;
        value = Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y) + (vector.z * vector.z));
        return value;
    }

    //Alternative, sperical collider as trigger
    GameObject getClosestInRange()
    {
        float closestDistance = -1;
        float temp;
        GameObject closest = null;
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemyArray)
        {
            if (e.activeSelf)
            {
                temp = getScalar(gameObject.transform.position - e.transform.position);

                if (temp <= range)
                {
                    if (closestDistance == -1)
                    {
                        closest = e;
                    }
                    else
                    {
                        if (temp < closestDistance)
                        {
                            closest = e;
                            closestDistance = temp;
                        }
                    }
                }
            }
        }
        return closest;
    }
}
