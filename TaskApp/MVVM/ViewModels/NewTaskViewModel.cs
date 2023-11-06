using System.Collections.ObjectModel;
using TaskApp.MVVM.Models;
using PropertyChanged;
using System;

public class NewTaskViewModel
{
    public event EventHandler<TaskAddedEventArgs> TaskAdded;

    public string Task { get; set; }
    public ObservableCollection<MyTask> Tasks { get; set; }
    public ObservableCollection<Category> Categories { get; set; }
    public Category SelectedCategory { get; set; }

    public NewTaskViewModel()
        //here is the colors for the category tasks
    {
        Categories = new ObservableCollection<Category>()
        {
            new Category
            {
                CId = 1,
                CName = "Games Finised",
                Color = "#84B94A"
            },
            new Category
            {
                CId = 2, CName = "Work/Uni",
                Color = "#E09735"
            },
            new Category
            {
                CId = 3, CName = "What to do",
                Color = "#D935E0"
            }
        };

        //here i am adding the tasks to fill out the page and also to demonstrate the app
        Tasks = new ObservableCollection<MyTask>()
        {
            new MyTask
            {
                TName = "ACC Mirage",
                TCompleted = false,
                CId = 1,
            },
            new MyTask
            {
                TName = "Warframe",
                TCompleted = false,
                CId = 1,
            },
            new MyTask
            {
                TName = "Study for Task App exam",
                TCompleted = false,
                CId = 2,
            },
            new MyTask
            {
                TName = "Create documents for the Task App",
                TCompleted = false,
                CId = 2,
            },
            new MyTask
            {
                TName = "Touch Some Grass",
                TCompleted = false,
                CId = 3,
            },
            new MyTask
            {TName = "Fresh Air Needed!", TCompleted = false, CId = 3}
        };
    }
    //here i have the new task add function
    public class TaskAddedEventArgs : EventArgs
    {
        public MyTask NewTask { get; }

        public TaskAddedEventArgs(MyTask newTask)
        {
            NewTask = newTask;
        }   
    }

    //here i am carrying on with the add task function
   public void AddTask()
    {
        if (SelectedCategory != null)
        {
            var newTask = new MyTask { TName = Task, TCompleted = false, CId = SelectedCategory.CId };
            SelectedCategory.AddTask(newTask);
            TaskAdded?.Invoke(this, new TaskAddedEventArgs(newTask));
            Task = string.Empty;
        }
    }
}
