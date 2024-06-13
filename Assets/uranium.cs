using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uranium : MonoBehaviour
{
    public int damageTiming = 10;
    public int damageRadius = 5;

    private GameObject playerPrefab;
    private Player player;
    

    // Start is called before the first frame update
    void Start()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        if (playerPrefab != null)
        {
            player = playerPrefab.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPrefab != null  && player != null )
        {
            float distance = Vector3.Distance(transform.position, playerPrefab.transform.position);
            
            if (distance <= damageRadius)
            {
                player.TakeDamage(damageTiming * Time.deltaTime);
            }
        }
    }
}
