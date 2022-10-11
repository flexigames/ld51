using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task", order = 1)]
public class GameTask : ScriptableObject
{
    public string id;
    public string name;
    public int numberOfSteps;
}