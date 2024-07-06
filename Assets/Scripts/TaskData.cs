using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData
{
    public int[] taskIsDoneList;
    

    public TaskData(Player player)
    {
        taskIsDoneList[0] = player.isTask1done?1:0;
        taskIsDoneList[1] = player.isTask2done ? 1 : 0;
        taskIsDoneList[2] = player.isTask3done ? 1 : 0;
        taskIsDoneList[3] = player.isTask4done ? 1 : 0;

    }
}
