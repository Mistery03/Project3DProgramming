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
    public GameObject task4;
    public GameObject taskComplete;
    public GameObject mark;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        task1.SetActive(player.isTask1done);
        task2.SetActive(player.isTask2done);
        task3.SetActive(player.isTask3done);
        task4.SetActive(player.isTask4done);
        
        if (player.isTask1done == true && player.isTask2done == true && player.isTask3done == true && player.isTask4done)
        {
            taskComplete.SetActive(true);
            mark.SetActive(true);
        }
    }

    public void refreshCompletedLines()
    {
        task1.SetActive(!player.isTask1done);
        task2.SetActive(!player.isTask1done);
        task3.SetActive(!player.isTask1done);
        task4.SetActive(!player.isTask1done);

    }
}
