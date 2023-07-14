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
        
        int timeQuantum = 3;

        RoundRobinScheduler scheduler = new RoundRobinScheduler(tasks, timeQuantum);

        scheduler.ExecuteTasks();

        Console.ReadLine();
   }
}






