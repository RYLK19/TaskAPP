using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TaskApp.MVVM.Models;
using PropertyChanged;
using System.Linq;

namespace TaskApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModel()
        {
            FileData();
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            // Initialize the Tasks collection within each Category
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<MyTask>(Tasks.Where(task => task.CId == category.CId));
                category.UpdateTotalTasks();
            }
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Handle tasks collection changes
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MyTask task in e.NewItems)
                {
                    Category category = Categories.FirstOrDefault(c => c.CId == task.CId);
                    if (category != null)
                    {
                        category.Tasks.Add(task);
                        category.Pending = category.Tasks.Count(t => !t.TCompleted);
                        category.Percentage = (float)category.Tasks.Count(t => t.TCompleted) / category.Tasks.Count;
                        category.UpdateTotalTasks();
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MyTask task in e.OldItems)
                {
                    Category category = Categories.FirstOrDefault(c => c.CId == task.CId);
                    if (category != null)
                    {
                        category.Tasks.Remove(task);
                        category.Pending = category.Tasks.Count(t => !t.TCompleted);
                        category.Percentage = (float)category.Tasks.Count(t => t.TCompleted) / category.Tasks.Count;
                        category.UpdateTotalTasks();
                    }
                }
            }
        }

        //here is the colors for the category names
        private void FileData()
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
            UpdateData();
        }

        //here the data is updating for the categories
        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CId == c.CId
                            select t;

                var completed = from t in tasks
                                where t.TCompleted == true
                                select t;

                var noCompleted = from t in tasks
                                  where t.TCompleted == false
                                  select t;

                c.Pending = noCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                var catColor =
                    (
                        from c in Categories
                        where c.CId == t.CId
                        select c.Color
                    ).FirstOrDefault();
                t.TColor = catColor;
            }
        }
    }
}