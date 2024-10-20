using UnityEngine;

public class Enemy1 : Enemy
{
    // Start is called before the first frame update
    void Awake()
    {
        assignStats(5,100,10,10,5,MagicType.IMAGINARY);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
