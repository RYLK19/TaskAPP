using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using PropertyChanged;

namespace TaskApp.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Category
    {
        public string CName { get; set; }
        public int Pending { get; set; }
        public float Percentage { get; set; }
        public bool IsSelected { get; set; }
        public string Color { get; set; }
        public int CId { get; set; }


        public ObservableCollection<MyTask> Tasks { get; set; }

        private int totalTasks;

        public int TotalTasks
        {
            get { return totalTasks; }
            set
            {
                if (totalTasks != value)
                {
                    totalTasks = value;
                }
            }
        }

        public Category()
        {
            Tasks = new ObservableCollection<MyTask>();
            UpdateTotalTasks();
        }

        public void AddTask(MyTask task)
        {
            Tasks.Add(task);
            UpdateTotalTasks();
        }

        public void RemoveTask(MyTask task)
        {
            Tasks.Remove(task);
            UpdateTotalTasks();
        }

        public void UpdateTotalTasks()
        {
            TotalTasks = Tasks.Count;
        }
    }
}
