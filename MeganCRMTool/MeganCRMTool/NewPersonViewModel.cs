using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeganCRMTool
{
    public class NewPersonViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICommand SaveCommand { get; set; }

        public NewPersonViewModel()
        {
            SaveCommand = new RelayCommand(SaveCommandExecuted);
        }

        public void SaveCommandExecuted(object obj)
        {
            DataLayer.SaveItem(new Person
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            });
        }
    }
}
