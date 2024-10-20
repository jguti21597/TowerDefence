using UnityEngine;

public class Wizard2 : Wizard
{
    // Start is called before the first frame update
    void Awake()
    {
        assignStats("Wizard 2",75, 10, 0.5f, 150, MagicType.PHYSICAL);
        gameObject.GetComponent<Renderer>().material.color = Color.magenta;
    }
}
