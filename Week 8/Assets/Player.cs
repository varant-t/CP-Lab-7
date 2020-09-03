using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public int health;
    public int level;
    public int lives;
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement code - DON'T MOVE CHARACTERS LIKE THIS - THIS IS JUST AN EXAMPLE
        float moveValue = Input.GetAxis("Horizontal");
        if (moveValue != 0)
        {
            transform.Translate(new Vector3(moveValue, transform.position.y, transform.position.z));
        }

        //Call the Save System's Save Player function when you press 1. Pass it the current Player script component.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveSystem.SavePlayer(this);
            Debug.Log("Saved");
        }

        //Call the Save System's Load Player function
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Load player returns type PlayerData
            PlayerData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                //grab the Health, Level and Position in our saved data and update our player
                health = data.health;
                level = data.level;
                

                transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            lives -= 1;
        }

        if(lives <= 0)
        {
            PlayerData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                //grab the Health, Level and Position in our saved data and update our player
                health = data.health;
                level = data.level;
                lives = 3;
             
                transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            }
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CheckPoint")
        {
           // SaveSystem.SavePlayer(this);
            Debug.Log("Saved at CheckPoint");
        }   
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            Debug.Log("Saved at CheckPoint");
        }
    }
}
