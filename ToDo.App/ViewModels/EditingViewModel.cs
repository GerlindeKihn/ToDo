using System.Collections.ObjectModel;
using ToDo.App.Interfaces;
using ToDo.App.Views;
using ToDo.Contracts.Dtos;

namespace ToDo.App.ViewModels
{
    public class EditingViewModel : BaseViewModel
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public Command SaveTask { get; }

        public EditingViewModel(IToDoApiClient apiClient, ObservableCollection<ToDoDto> toDos, IToDoHolder toDoHolder)
        {
            if (toDoHolder.SelectedToDo is null)
            {
                title = "Create Task";

                SaveTask = new(async () =>
                {
                    SaveToDoDto dto = new(Topic, Deadline);
                    ToDoDto toDo = await apiClient.Save(dto);

                    toDos.Add(toDo);

                    await Shell.Current.GoToAsync("..");
                });
            }
            else
            {
                title = "Edit Task";
                topic = toDoHolder.SelectedToDo.Topic;
                deadline = toDoHolder.SelectedToDo.Deadline;

                SaveTask = new(async () =>
                {
                    SaveToDoDto dto = new(Topic, Deadline);
                    ToDoDto toDo = await apiClient.Save(dto, toDoHolder.SelectedToDo.Id);

                    int index = toDos.IndexOf(toDoHolder.SelectedToDo);
                    toDos.RemoveAt(index);
                    toDos.Insert(index, toDo);

                    await Shell.Current.GoToAsync("..");
                });
            }
        }

        private string topic;
        public string Topic
        {
            get { return topic; }
            set { SetProperty(ref topic, value); }
        }

        private DateTime deadline = DateTime.Today;
        public DateTime Deadline
        {
            get { return deadline; }
            set { SetProperty(ref deadline, value); }
        }
    }
}
