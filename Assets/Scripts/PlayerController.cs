using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    public int playerHealth;
    public int points;
    public float speed;
    public float jumpHeight;

    public Image healthBar;
    public TextMeshProUGUI pointDisplay;

    private Rigidbody2D rb;

    [SerializeField] private GameObject leftAttack;
    [SerializeField] private GameObject rightAttack;

    [SerializeField] private Toggle blueSwitch;
    [SerializeField] private Toggle redSwitch;
    private string atkColor;

    [SerializeField] private Animator ani;

    public bool touchingGround;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI endScore;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerHealth = 100;
        points = 0;
        speed = 0.1f;
        jumpHeight = 10f;

        gameOverPanel.SetActive(false);

        rb = gameObject.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        ColorSwitch("red");
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //rb.velocity.x += speed;
            rb.velocity += new Vector2(speed, 0);
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            rb.velocity += new Vector2(-speed, 0);
        }

        //jump + groundcheck
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround) {
            rb.velocity += new Vector2(0, jumpHeight);
            touchingGround = false;
        } else if (Input.GetKeyDown(KeyCode.W)&&touchingGround) {
            rb.velocity += new Vector2(0, jumpHeight);
            touchingGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.F)) {
            ani.SetBool("atk", true);
        }

        if(transform.rotation.z != 0)
        {
            //transform.rotation = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        
    }


    //if the player is hit
    public void PlayerHit()
    {
        //lower health
        playerHealth = playerHealth - 10;
        Debug.Log(playerHealth * .01f);
        healthBar.rectTransform.localScale = new Vector3(.01f * playerHealth,1,1);

        //check that health is more than zero
        if (playerHealth <= 0)
        {
            Time.timeScale = 0;
            
            gameOverPanel.SetActive(true);
            endScore.text = "Game Over!\nScore: "+points;
            //end game
        }
    }

    //controls for toggles/attack color
    public void ColorSwitch(string cl)
    {
        if(atkColor == cl)
        {
            atkColor = null;
        }
        else
        {
            if (cl == "red")
            {
                leftAttack.GetComponent<SpriteRenderer>().color = Color.red;
                rightAttack.GetComponent<SpriteRenderer>().color = Color.red;
                atkColor = "red";
                leftAttack.tag = "redAtk";
                rightAttack.tag = "redAtk";
            }
            else
            {
                leftAttack.tag = "blueAtk";
                rightAttack.tag = "blueAtk";
                leftAttack.GetComponent<SpriteRenderer>().color = Color.cyan;
                rightAttack.GetComponent<SpriteRenderer>().color = Color.cyan;
                atkColor = "blue";
            }
        }

        //control the toggles
        if (atkColor == "red")
        {
            blueSwitch.interactable = false;
            redSwitch.interactable = true;
        }
        else if (atkColor == "blue")
        {
            blueSwitch.interactable = true;
            redSwitch.interactable = false;
        }
        else
        {
            blueSwitch.interactable = true;
            redSwitch.interactable = true;
        }
    }

    //controls attack animation
    public void atkDone()
    {
        ani.SetBool("atk", false);
    }

    //updates score counter
    public void ScoreUp()
    {
        points++;
        pointDisplay.text = "Score: " + points;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;


        if (col.tag == "ground")
        {
            touchingGround = true;
        }
    }

    //referenced from https://answers.unity.com/questions/1040238/select-ui-slider-to-change-float-value.html

    public void AdjustSpeed(GameObject slider)
    {
        float adjust = slider.GetComponent<Slider>().value;
        speed = adjust;
    }

    public void AdjustJump(GameObject slider)
    {
        float adjust = slider.GetComponent<Slider>().value;
        jumpHeight = adjust;
    }

    //checked against https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
