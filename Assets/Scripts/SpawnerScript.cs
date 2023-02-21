using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public int enemiesOnScreen;
    public GameObject[] allEnemies;
    public int[] enemyActive;

    private float ct;
    public int sec;
    public int val;

    public int totaltime;
    public GameObject instr;

    // Start is called before the first frame update
    void Start()
    {
        ct = 0;
        sec = 0;
        val = 5;
        totaltime = 0;
        instr.SetActive(true);

        enemiesOnScreen = 1;

        //randomize a enemy to begin, deactivate all others
        int beginRandom = Random.Range(0, 4);
        for(int a = 0; a< allEnemies.Length; a++)
        {
            if(a == beginRandom)
            {
                allEnemies[a].SetActive(true);
                enemyActive[a] = 1;
                allEnemies[a].tag = "redEnemy";
            }
            else
            {
                allEnemies[a].SetActive(false);
                enemyActive[a] = 0;
            }
 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //manage time
        ct += Time.deltaTime;
        if (ct >= 1)
        {
            sec++;
            totaltime++;
            ct = 0;
        }


        //manage instructions
        if (totaltime >= 10)
        {
            instr.SetActive(false);
        }


        //if not all enemies are activated
        if (enemiesOnScreen < 4)
        {
            if (sec >= val)
            {
                sec = 0;
                /**
                for(int g = 0; g < allEnemies.Length; g++)
                {
                    
                }**/
                //activate enemy
                bool activated = false;
                while(activated == false)
                {
                    //randomize
                    int getRandom = Random.Range(0, 4);

                    Debug.Log("spawntry" + getRandom);

                    if (enemyActive[getRandom] == 0)
                    {
                        enemyActive[getRandom] = 1;
                        allEnemies[getRandom].SetActive(true);
                        enemiesOnScreen++;
                        activated = true;

                        //randomize color
                        int coin = Random.Range(0, 2);
                        if (coin == 0)
                        {
                            allEnemies[getRandom].tag = "redEnemy";
                            allEnemies[getRandom].GetComponent<SpriteRenderer>().color = Color.red;
                        }
                        else
                        {
                            allEnemies[getRandom].tag = "blueEnemy";
                            allEnemies[getRandom].GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                    }
                    else
                    {
                        activated = false;
                    }
                   
                }
            }
        }
    }

    //called by enemies when killed
    public void HasDeactivated(int which)
    {
        enemiesOnScreen--;
        //allEnemies[which].SetActive(false);
        enemyActive[which] = 0;
    }
}
