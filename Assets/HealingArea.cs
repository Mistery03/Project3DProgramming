using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingArea : MonoBehaviour
{
      
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent("Player") is Player)
        {
            Player player = other.GetComponent("Player") as Player;
            player.currentHP += 1 * Time.deltaTime;
            UIManager.Instance.UpdateHP(player.currentHP, player.maxHP); // Update HP text
        }
    }
}
