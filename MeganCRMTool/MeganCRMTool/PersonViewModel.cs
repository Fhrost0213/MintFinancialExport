using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeganCRMTool
{
    public class PersonViewModel : BaseViewModel
    {
        private int _personId;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public ICommand NoteCommand { get; set; }

        public PersonViewModel(int personId)
        {
            _personId = personId;
            NoteCommand = new RelayCommand(NoteCommandExecuted);
            Notes = new ObservableCollection<Note>();

            Refresh();
        }

        public void NoteCommandExecuted(object obj)
        {
            var noteId = 0;

            if (obj != null)
            {
                // We're opening a note
                var note = (Note) obj;
                noteId = note.NoteId;
            }

            NoteView view = new NoteView();
            NoteViewModel vm = new NoteViewModel(_personId, noteId);

            view.DataContext = vm;
            view.ShowDialog();

            Refresh();
        }

        private void Refresh()
        {
            var person = DataLayer.GetList<Person>().First(x => x.PersonId == _personId);

            Notes.Clear();

            foreach (var note in person.Notes)
            {
                Notes.Add(note);
            }

            FirstName = person.FirstName;
            LastName = person.LastName;
        }
    }
}
