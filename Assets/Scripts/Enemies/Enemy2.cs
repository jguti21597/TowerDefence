using UnityEngine;

public class Enemy2 : Enemy
{
    // Start is called before the first frame update
    void Awake()
    {
        assignStats(10,75,5,20,5,MagicType.MAGICAL);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
