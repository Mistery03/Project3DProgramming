using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int task;
    public float health;
    public float[] positions;

    public PlayerData(Player player)
    {
        health = player.currentHP;
        positions = new float[3];
        positions[0] = player.transform.position.x;
        positions[1] = player.transform.position.y;
        positions[2] = player.transform.position.z;
    }
}
