using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List <Button> spawnButtons;
    string selectedWizard = "";
    public TextMeshProUGUI points;
    public TextMeshProUGUI stateText;

    static List<List<string>> roundList = new List<List<string>>();
    static int roundNum;
    float roundWait = 5;
    float roundTimer;
    Ray spawnRay;
    RaycastHit spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        points.text = "SkillPoints: " + Skillpoints.getPoints();
        stateText.text = "";
        //Get the text from the button, which should be the required wizard type
        foreach (Button button in spawnButtons)
        {
            button.onClick.AddListener(delegate { selectWizard(button.tag); });
            button.GetComponentInChildren<TextMeshProUGUI>().text = getButtonText(button.tag);
        }
        roundNum = -1;
        roundTimer = 0;
        setTestWaves();
    }

    void setTestWaves()
    {
        List<string> temp = new List<string>();
        List<string> temp1 = new List<string>();
        List<string> temp2 = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            temp.Add("Enemy1");
            temp.Add("Enemy1");

            temp1.Add("Enemy2");
            temp1.Add("Enemy2");

            temp2.Add("Enemy1");
            temp2.Add("Enemy2");
        }
        roundList.Add(temp);
        roundList.Add(temp1);
        roundList.Add(temp2);
    }
    void setWave()
    {
        foreach(GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            spawner.GetComponent<EnemySpawner>().setList(new List<string>(roundList[roundNum]));
        }
    }
    public void nextRound()
    {
        roundNum++;
        if(!outOfRounds())
        {
            setWave();
        }
    }
    public static bool outOfRounds()
    {
        return(roundNum > roundList.Count-1);
    }
    string getButtonText(string objName)
    {
        Wizard wizard = GameObject.Find(objName).GetComponent<Wizard>();
        string name = wizard.getName();
        int cost = wizard.getCost();
        return (name + "\n(cost " + cost + ")");
    }

    // Update is called once per frame
    void Update()
    {
        points.text = "SkillPoints: " + Skillpoints.getPoints();
        checkState();

        if(CurrentGameState.getGameState() == GameState.INTERMISSION)
        {
            roundTimer += Time.deltaTime;
            if(roundTimer > roundWait)
            {
                nextRound();
                roundTimer = 0;
                CurrentGameState.setState(GameState.PLAYING);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if(CurrentGameState.getGameState() == GameState.PAUSED)
            {
                CurrentGameState.setState(GameState.PLAYING);
            }
            else if(CurrentGameState.getGameState()==GameState.PLAYING)
            {
                CurrentGameState.setState(GameState.PAUSED);
            }
        }
    }

    void checkState()
    {
        Skillpoints.updatePointState();
        GameState state = CurrentGameState.getGameState();
        if (state == GameState.LOSS)
        {
            stateText.text = "Game Over";
            stateText.color = Color.red;
        }
        else if(state == GameState.WIN)
        {
            stateText.text = "You Win!";
            stateText.color= Color.green;
        }
        else if(state == GameState.PAUSED)
        {
            stateText.text = "Game Paused";
            stateText.color = Color.grey;
        }
        else if(state == GameState.PLAYING)
        {
            stateText.text = "Round: " + (roundNum+1);
            stateText.color = Color.blue;
        }
        else if(state == GameState.INTERMISSION)
        {
            stateText.text = "Intermission";
            stateText.color = Color.blue;
        }
    }
    void selectWizard(string wizTag)
    {
        selectedWizard = wizTag;
    }

    private void OnMouseDown()
    {
        if (selectedWizard != "")
        {
            spawnRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(spawnRay, out spawnPoint)) 
            {
                spawnWizard(selectedWizard, spawnPoint.point);
            }
        }
        selectedWizard = "";
    }

    void spawnWizard(string wizard, Vector3 position)
    {
        int cost = GameObject.Find(wizard).GetComponent<Wizard>().getCost();
        if (Skillpoints.getPoints() - cost >= 0)
        {
            position.y = 1;
            GameObject newWizard = Instantiate(GameObject.Find(wizard), position, Quaternion.identity);
            LineRenderer lr = newWizard.AddComponent<LineRenderer>();
            newWizard.GetComponent<Wizard>().setLineRenderer(lr);
            newWizard.tag = "Wizard";
            Skillpoints.addPoints(-GameObject.Find(wizard).GetComponent<Wizard>().getCost());
        }
        selectedWizard = "";
    }

    public static bool checkEmpty()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach(GameObject spawner in spawners) 
        {
            if(!spawner.GetComponent<EnemySpawner>().isEmpty())
            {
                return false;
            }
        }
        return true;
    }
}
