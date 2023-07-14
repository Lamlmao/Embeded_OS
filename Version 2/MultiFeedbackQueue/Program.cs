using System;
using System.Collections.Generic;


public class Task
{
   public string Name { get; set; }
   public int ArrivalTime { get; set; }
   public int BurstTime { get; set; }
   public int Priority { get; set; }
}


public class MultilevelFeedbackQueueScheduler
{
   private List<Queue<Task>> taskQueues;
   private int currentTime;


   public MultilevelFeedbackQueueScheduler()
   {
       taskQueues = new List<Queue<Task>>();
   }


   public void AddTask(Task task)
   {
       if (task.Priority >= taskQueues.Count)
       {
           while (task.Priority >= taskQueues.Count)
           {
               taskQueues.Add(new Queue<Task>());
           }
       }


       taskQueues[task.Priority].Enqueue(task);
   }


   public void ExecuteTasks()
   {
       currentTime = 0;


       while (true)
       {
           bool allQueuesEmpty = true;


           for (int i = 0; i < taskQueues.Count; i++)
           {
               Queue<Task> currentTaskQueue = taskQueues[i];


               if (currentTaskQueue.Count > 0)
               {
                   Task currentTask = currentTaskQueue.Dequeue();


                   if (currentTime < currentTask.ArrivalTime)
                   {
                       currentTime = currentTask.ArrivalTime;
                   }


                   Console.WriteLine($"Executing task '{currentTask.Name}' from queue {i} from time {currentTime} to {currentTime + currentTask.BurstTime}");
                   currentTime += currentTask.BurstTime;


                   if (currentTask.BurstTime > 0)
                   {
                       currentTask.Priority++;
                       if (currentTask.Priority >= taskQueues.Count)
                       {
                           currentTask.Priority = taskQueues.Count - 1;
                       }
                       taskQueues[currentTask.Priority].Enqueue(currentTask);
                   }
               }


               if (currentTaskQueue.Count > 0)
               {
                   allQueuesEmpty = false;
               }
           }


           if (allQueuesEmpty)
           {
               break;
           }
       }
   }
}


public class Program
{
   public static void Main(string[] args)
   {
       MultilevelFeedbackQueueScheduler scheduler = new MultilevelFeedbackQueueScheduler();


       scheduler.AddTask(new Task { Name = "Task 1", ArrivalTime = 0, BurstTime = 5, Priority = 0 });
       scheduler.AddTask(new Task { Name = "Task 2", ArrivalTime = 6, BurstTime = 3, Priority = 1 });
       scheduler.AddTask(new Task { Name = "Task 3", ArrivalTime = 4, BurstTime = 2, Priority = 0 });
       scheduler.AddTask(new Task { Name = "Task 4", ArrivalTime = 1, BurstTime = 4, Priority = 0 });


       scheduler.ExecuteTasks();
   }
}



