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

        public Command SaveNewTask { get; }

        public EditingViewModel(IToDoApiClient apiClient)
        {
            title = "Create Task";

            SaveNewTask = new(async () =>
            {
                SaveToDoDto dto = new(Topic, Deadline);
                await apiClient.Save(dto);

                await Shell.Current.GoToAsync("..");

            });
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
