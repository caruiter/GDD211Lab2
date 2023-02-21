using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private Animator ani;
    [SerializeField] private SpawnerScript spawner;
    [SerializeField] private int self;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void EntryDone()
    {
        ani.SetBool("int", true);
    }

    public void EnemyDie()
    {
        GameObject pl = GameObject.FindWithTag("player");
        pl.GetComponent<PlayerController>().ScoreUp();
        spawner.HasDeactivated(self);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "redAtk" && gameObject.tag == "redEnemy")
        {
            EnemyDie();
        } else if (col.tag == "blueAtk" && gameObject.tag == "blueEnemy") {
            EnemyDie();
        }
        else if (col.tag == "player")
        {
            col.GetComponent<PlayerController>().PlayerHit();
            spawner.HasDeactivated(self);
            gameObject.SetActive(false);
        }
    }
}
