using System;
using System.Collections.Generic;


public class Task
{
   public string Name { get; set; }
   public int ArrivalTime { get; set; }
   public int BurstTime { get; set; }
   public int Priority { get; set; }
}


public class MultilevelQueueScheduler
{
   private List<Queue<Task>> taskQueues;
   private int numQueues;


   public MultilevelQueueScheduler(int numQueues)
   {
       this.numQueues = numQueues;
       taskQueues = new List<Queue<Task>>();


       for (int i = 0; i < numQueues; i++)
       {
           taskQueues.Add(new Queue<Task>());
       }
   }


   public void AddTask(Task task)
   {
       int queueIndex = Math.Min(task.Priority, numQueues - 1);
       taskQueues[queueIndex].Enqueue(task);
   }
   public void ExecuteTasks()
   {
       int currentTime = 0;


       for (int currentQueue = 0; currentQueue < numQueues; currentQueue++)
       {
           Queue<Task> currentTaskQueue = taskQueues[currentQueue];


           while (currentTaskQueue.Count > 0)
           {
               Task currentTask = currentTaskQueue.Dequeue();


               if (currentTime < currentTask.ArrivalTime)
               {
                   currentTime = currentTask.ArrivalTime;
               }


               Console.WriteLine($"Executing task '{currentTask.Name}' from queue {currentQueue} from time {currentTime} to {currentTime + currentTask.BurstTime}");
               currentTime += currentTask.BurstTime;
           }
       }
   }


}


public class Program
{
   public static void Main(string[] args)
   {
       MultilevelQueueScheduler scheduler = new MultilevelQueueScheduler(3);


       scheduler.AddTask(new Task { Name = "Task 1", ArrivalTime = 0, BurstTime = 5, Priority = 0 });
       scheduler.AddTask(new Task { Name = "Task 2", ArrivalTime = 13, BurstTime = 3, Priority = 1 });
       scheduler.AddTask(new Task { Name = "Task 3", ArrivalTime = 4, BurstTime = 2, Priority = 2 });
       scheduler.AddTask(new Task { Name = "Task 4", ArrivalTime = 6, BurstTime = 4, Priority = 0 });


       scheduler.ExecuteTasks();


       Console.ReadLine();
   }
}



