using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeganCRMTool
{
    public class MainViewModel : BaseViewModel
    {
        private List<Person> _people;

        public List<Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged("People");
            }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand AddNewPersonCommand { get; set; }

        public ICommand OpenPersonCommand { get; set; }
        

        public MainViewModel()
        {
            People = new List<Person>();
            People = DataLayer.GetList<Person>();

            RefreshCommand = new RelayCommand(RefreshCommandExecuted);
            AddNewPersonCommand = new RelayCommand(AddNewPersonCommandExecuted);
            OpenPersonCommand = new RelayCommand(OpenPersonCommandExecuted);
        }

        public void AddNewPersonCommandExecuted(object obj)
        {
            NewPersonViewModel vm = new NewPersonViewModel();
            NewPersonView view = new NewPersonView();

            view.DataContext = vm;
            view.Show();
        }

        public void RefreshCommandExecuted(object obj)
        {
            People.Clear();
            People.AddRange(DataLayer.GetList<Person>());
        }

        public void OpenPersonCommandExecuted(object obj)
        {
            if (obj != null)
            {
                var person = (Person)obj;


                PersonView view = new PersonView();
                PersonViewModel vm = new PersonViewModel(person.PersonId);

                view.DataContext = vm;
                view.Show();
            }
        }
    }
}
