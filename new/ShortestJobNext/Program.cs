using System;
using System.Collections.Generic;


public class Task
{
   public string Name { get; set; }
   public int ArrivalTime { get; set; }
   public int BurstTime { get; set; }
}


public class SJNScheduler
{
   private PriorityQueue<Task> taskQueue;


   public SJNScheduler(List<Task> tasks)
   {
       tasks.Sort((task1, task2) => task1.ArrivalTime.CompareTo(task2.ArrivalTime));
       taskQueue = new PriorityQueue<Task>(tasks, Comparer<Task>.Create((task1, task2) => task1.BurstTime.CompareTo(task2.BurstTime)));
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


public class PriorityQueue<T>
{
   private List<T> items;
   private IComparer<T> comparer;


   public int Count => items.Count;


   public PriorityQueue(IEnumerable<T> collection, IComparer<T> comparer)
   {
       this.comparer = comparer;
       items = new List<T>(collection);
       items.Sort(comparer);
   }


   public T Dequeue()
   {
       T item = items[0];
       items.RemoveAt(0);
       return item;
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


       SJNScheduler scheduler = new SJNScheduler(tasks);


       scheduler.ExecuteTasks();


       Console.ReadLine();
   }
}



