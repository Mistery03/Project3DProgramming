using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData
{
    public bool taskIsDone1;
    public bool taskIsDone2;
    public bool taskIsDone3;
    public bool taskIsDone4;
    

    public TaskData(Player player)
    {
        taskIsDone1 = player.isTask1done;
        taskIsDone2 = player.isTask2done;
        taskIsDone3 = player.isTask3done;
        taskIsDone4 = player.isTask4done;
       
    }
}
