﻿
using System.Collections.ObjectModel;
using ToDo.App.Interfaces;
using ToDo.Contracts.Dtos;

namespace ToDo.App.ViewModels
{
    public class ToDosViewModel : BaseViewModel
    {
        private string title;
        public string Title 
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        public ObservableCollection<ToDoDto> ToDos { get; }

        public Command GetToDos { get; }

        public ToDosViewModel(IToDoApiClient apiClient)
        {
            title = "To Do's";
            ToDos = new ();

            GetToDos = new(async () =>
            {
                ToDos.Clear();

                IReadOnlyCollection<ToDoDto> toDos = await apiClient.Get();
                foreach (ToDoDto toDo in toDos) ToDos.Add(toDo);

                IsRefreshing = false;
            });
        }
    }
}