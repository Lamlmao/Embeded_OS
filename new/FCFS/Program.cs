using System;
using System.Collections.Generic;
using System.Linq;

public class Task
{
    public string Name { get; set; }
    public int ArrivalTime { get; set; }
    public int BurstTime { get; set; }
    public int WaitingTime { get; set; }
    public int TurnaroundTime { get; set; }
}

public class FCFSScheduler
{
    private Queue<Task> taskQueue;

    private Queue<Task> secondTaskQueue;
    
    public FCFSScheduler(List<Task> tasks)
    {
        secondTaskQueue = new Queue<Task>(tasks);
        tasks.Sort((task1, task2) => task1.ArrivalTime.CompareTo(task2.ArrivalTime));
        taskQueue = new Queue<Task>(tasks);
    }

    public void ExecuteTasks()
    {
        Console.WriteLine("\n");
        int currentTime = 0;
        while (taskQueue.Count > 0)
        {
            Task task = taskQueue.Dequeue();
            if (currentTime < task.ArrivalTime)
            {
                currentTime = task.ArrivalTime;
            }

            task.WaitingTime = currentTime - task.ArrivalTime;
            task.TurnaroundTime = task.WaitingTime + task.BurstTime;
            
            Console.WriteLine($"Executing task '{task.Name}' from time {currentTime} to {currentTime + task.BurstTime}");
            currentTime += task.BurstTime;
        }
    }
        public void PrintTaskStats()
    {
        Console.WriteLine("\nTask execution statistics:\n");
        Console.WriteLine("Task\tArrival Time\tBurst Time\tWaiting Time\tTurnaround Time");
        foreach (Task task in secondTaskQueue)
        {
            Console.WriteLine($"{task.Name}\t{task.ArrivalTime}\t\t{task.BurstTime}\t\t{task.WaitingTime}\t\t{task.TurnaroundTime}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Random random = new Random();
        List<Task> tasks = new List<Task>();
        for (int i = 1; i <= 5; i++)
        {
            Task task = new Task
            {
                Name = $"Task {i}",
                ArrivalTime = random.Next(0, 10),
                BurstTime = random.Next(1, 10)
            };
            tasks.Add(task);
        }

        FCFSScheduler scheduler = new FCFSScheduler(tasks);
        scheduler.ExecuteTasks();
        scheduler.PrintTaskStats();
        Console.ReadLine();
    }
}
