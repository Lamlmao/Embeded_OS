using System;
using System.Collections.Generic;


public class Task
{
   public string Name { get; set; }
   public int ArrivalTime { get; set; }
   public int BurstTime { get; set; }
}


public class FCFSScheduler
{
   private Queue<Task> taskQueue;


   public FCFSScheduler(List<Task> tasks)
   {
       tasks.Sort((task1, task2) => task1.ArrivalTime.CompareTo(task2.ArrivalTime));


       taskQueue = new Queue<Task>(tasks);
   }


   public void ExecuteTasks()
   {
       int currentTime = 0;
       while (taskQueue.Count > 0)
       {
           Task task = taskQueue.Dequeue();


           if (currentTime < task.ArrivalTime)
           {
               currentTime = task.ArrivalTime;
           }


           Console.WriteLine($"Executing task '{task.Name}' from time {currentTime} to {currentTime + task.BurstTime}");
           currentTime += task.BurstTime;
       }
   }
}


public class Program
{
   public static void Main(string[] args)
   {
       List<Task> tasks = new List<Task>()
       {
           new Task { Name = "Task 1", ArrivalTime = 0, BurstTime = 5 },
           new Task { Name = "Task 2", ArrivalTime = 6, BurstTime = 3 },
           new Task { Name = "Task 3", ArrivalTime = 4, BurstTime = 2 },
           new Task { Name = "Task 4", ArrivalTime = 1, BurstTime = 4 }
       };


       FCFSScheduler scheduler = new FCFSScheduler(tasks);


       scheduler.ExecuteTasks();


       Console.ReadLine();
   }
}



