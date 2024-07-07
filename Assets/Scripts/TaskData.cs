using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData
{
    public bool[] taskIsDoneList;
    

    public TaskData(Player player)
    {
        taskIsDoneList[0] = player.isTask1done;
        taskIsDoneList[1] = player.isTask2done;
        taskIsDoneList[2] = player.isTask3done;
        taskIsDoneList[3] = player.isTask4done;

    }
}
