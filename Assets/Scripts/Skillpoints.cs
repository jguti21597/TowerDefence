using System.Linq;
using TMPro;
using UnityEngine;

public static class Skillpoints
{
    static int skillPoints;
    public static void initialize()
    {
        skillPoints = 1000;
    }

    public static int getPoints()
    {
        return skillPoints;
    }

    public static int addPoints(int toAdd)
    {
        skillPoints += toAdd;
        return skillPoints;
    }

    public static void updatePointState()
    {
        if (skillPoints < 0)
        {
            CurrentGameState.setState(GameState.LOSS);
        }
        else if (GameController.checkEmpty() && (GameObject.FindGameObjectsWithTag("Enemy").Count() == 0))//Temporary until skilltree
        {
            CurrentGameState.setState(GameState.WIN);
        }
        else if(GameController.checkEmpty())
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Count());
        }
    }
}
