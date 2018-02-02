using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeganCRMTool
{
    public class NoteViewModel : BaseViewModel
    {
        private int _personId;
        private int _noteId;
        public string Note { get; set; }

        public ICommand SaveCommand { get; set; }

        public NoteViewModel(int personId, int noteId)
        {
            _personId = personId;
            _noteId = noteId;
            SaveCommand = new RelayCommand(SaveCommandExecuted);

            if (noteId != 0)
            {
                Note = DataLayer.GetList<Note>().First(x => x.NoteId == noteId).Info;
            }
        }

        public void SaveCommandExecuted(object obj)
        {
            Note note = new Note();
            if (_noteId != 0) note.NoteId = _noteId;
            note.PersonId = _personId;
            note.Info = Note;
            note.AddDate = DateTime.Now;
            DataLayer.SaveItem(note);
        }
    }
}
