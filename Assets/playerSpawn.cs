using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetPlayerSpawnPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
