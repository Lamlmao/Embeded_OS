using System;
using System.Collections.Generic;


public class Task
{
   public string Name { get; set; }
   public int ArrivalTime { get; set; }
   public int BurstTime { get; set; }
}


public class RoundRobinScheduler
{
   private Queue<Task> taskQueue;
   private int timeQuantum;


   public RoundRobinScheduler(List<Task> tasks, int quantum)
   {
       tasks.Sort((task1, task2) => task1.ArrivalTime.CompareTo(task2.ArrivalTime));


       taskQueue = new Queue<Task>(tasks);
       timeQuantum = quantum;
   }


   public void ExecuteTasks()
   {
       int currentTime = 0;
       while (taskQueue.Count > 0)
       {
           Task currentTask = taskQueue.Dequeue();


           if (currentTime < currentTask.ArrivalTime)
           {
               currentTime = currentTask.ArrivalTime;
           }


           Console.WriteLine($"Executing task '{currentTask.Name}' from time {currentTime} to {currentTime + Math.Min(timeQuantum, currentTask.BurstTime)}");


           currentTask.BurstTime -= timeQuantum;
           currentTime += timeQuantum;


           if (currentTask.BurstTime > 0)
           {
               taskQueue.Enqueue(currentTask);
           }
       }
   }
}


public class Program
{
   public static void Main(string[] args)
   {
       List<Task> tasks = new List<Task>()
       {
           new Task { Name = "Task 1", ArrivalTime = 0, BurstTime = 8 },
           new Task { Name = "Task 2", ArrivalTime = 1, BurstTime = 4 },
           new Task { Name = "Task 3", ArrivalTime = 2, BurstTime = 10 },
           new Task { Name = "Task 4", ArrivalTime = 3, BurstTime = 6 }
       };


       int timeQuantum = 3;


       RoundRobinScheduler scheduler = new RoundRobinScheduler(tasks, timeQuantum);


       scheduler.ExecuteTasks();


       Console.ReadLine();
   }
}






