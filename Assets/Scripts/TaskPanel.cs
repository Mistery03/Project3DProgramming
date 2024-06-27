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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isTask1done == true)
        {
            task1.SetActive(!task1.activeSelf);
        }
        else if(player.isTask2done == true)
        {
            task2.SetActive(!task2.activeSelf);
        }else if(player.isTask3done == true)
        {
            task3.SetActive(!task3.activeSelf);
        }
    }
}
