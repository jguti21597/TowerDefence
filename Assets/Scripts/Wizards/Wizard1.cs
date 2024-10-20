using UnityEngine;

public class Wizard1 : Wizard
{
    // Start is called before the first frame update
    void Awake()
    {
        assignStats("Wizard 1", 50,10,1,100,MagicType.IMAGINARY);
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
