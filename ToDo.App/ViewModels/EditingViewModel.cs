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

        public ObservableCollection<UserDto> Users { get; }
        public Command SaveTask { get; }

        public EditingViewModel(
            IToDoApiClient toDoApiClient,
            IToDoHolder toDoHolder,
            IUserApiClient userApiClient,
            ObservableCollection<ToDoDto> toDos)
        {
            Users = new();

            new Command(async () =>
            {
                IReadOnlyCollection<UserDto> users = await userApiClient.Get();
                foreach (UserDto user in users) Users.Add(user);
            }).Execute(this);

            if (toDoHolder.SelectedToDo is null)
            {
                title = "Create Task";

                SaveTask = new(async () =>
                {
                    SaveToDoDto dto = new(Topic, Deadline, Assignee?.Id);
                    ToDoDto toDo = await toDoApiClient.Save(dto);

                    if(Assignee is null) toDos.Add(toDo);

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
                    SaveToDoDto dto = new(Topic, Deadline, Assignee?.Id);
                    ToDoDto toDo = await toDoApiClient.Save(dto, toDoHolder.SelectedToDo.Id);

                    int index = toDos.IndexOf(toDoHolder.SelectedToDo);
                    toDos.RemoveAt(index);
                    if(Assignee is null) toDos.Insert(index, toDo);

                    await Shell.Current.GoToAsync("..");
                });
            }
        }

        private string topic = default!;
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

        private UserDto? assignee;
        public UserDto? Assignee
        {
            get { return assignee; }
            set { SetProperty(ref assignee, value); }
        }
    }
}
