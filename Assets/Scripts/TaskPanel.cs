using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanel : MonoBehaviour
{
    //public GameObject taskUI;

    public Player player;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject taskComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isTask1done == true)
        {
            task1.SetActive(true);
        }
        else if(player.isTask2done == true)
        {
            task2.SetActive(true);
        }else if(player.isTask3done == true)
        {
            task3.SetActive(true);
        }
        else if(player.isTask1done==true && player.isTask2done == true && player.isTask3done == true)
        {
            taskComplete.SetActive(true);
        }
    }
}
