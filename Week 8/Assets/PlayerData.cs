using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data class that will be saved. It needs the System.Serializable attribute to be used by the Binary Formatter
[System.Serializable]
public class PlayerData
{
    public int health;
    public int level;
    public int lives;
    public float[] playerPosition;

    //Constructor to create the default player data class
    public PlayerData(Player player)
    {
        health = player.health;
        level = player.level;
        lives = player.lives;

        playerPosition = new float[3];

        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
    }
}
