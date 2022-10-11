using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskText;

    private List<CompletableTask> tasks = new List<CompletableTask>() {
        new CompletableTask("books", "Put all books in the shelf", "book"),
        new CompletableTask("kitchen", "Do the dishes", "plate"),
        new CompletableTask("laundry", "Pick up laundry from the floor", "clothing")
    };

    void Start() {
        CalculateNumberOfSteps();
        UpdateText();
    }

    void CalculateNumberOfSteps() {
        PickupItem[] pickupItems = FindObjectsOfType<PickupItem>();
        foreach (PickupItem pickupItem in pickupItems) {
            CompletableTask task = tasks.Find(t => t.itemType == pickupItem.itemType);
            if (task != null) {
                task.totalSteps++;
            }
        }

    }

    void CheckAllStepsDone() {
        bool allDone = true;
        foreach (CompletableTask task in tasks) {
            if (!task.IsDone()) {
                allDone = false;
                break;
            }
        }

        if (allDone) {
            GlobalState.time = FindObjectOfType<GameTimer>().timeSpent;
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void IncreaseSteps(string taskId)
    {
        CompletableTask task = tasks.Find(t => t.id == taskId);
        task.completedSteps++;

        UpdateText();
        CheckAllStepsDone();
    }

    public void UpdateText()
    {
        string text = "";
        foreach (CompletableTask task in tasks) {
            text += task.name + " " + task.completedSteps + "/" + task.totalSteps + "\n";
        }
        taskText.text = text;
    }
}

public class CompletableTask
{
    public string id;
    public string name;
    public string itemType;
    public int totalSteps = 0;
    public int completedSteps = 0;

    public CompletableTask(string id, string name, string itemType)
    {
        this.id = id;
        this.name = name;
        this.itemType = itemType;
    }

    public bool IsDone() {
        return completedSteps >= totalSteps;
    }
}