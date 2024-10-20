using UnityEngine;

public class Fortress : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        Skillpoints.initialize();

    }

    public void takeDamage(int damageVal)
    {
        Skillpoints.addPoints(-damageVal);
        Debug.Log("Damage: " +  damageVal);
    }
}
