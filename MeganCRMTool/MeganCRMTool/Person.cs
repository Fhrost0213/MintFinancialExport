using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeganCRMTool
{
    public partial class Person
    {
        public string LatestNoteInfo
        {
            get
            {
                var note = Notes.OrderByDescending(x => x.AddDate).FirstOrDefault();
                if (note == null)
                {
                    return "";
                }

                return note.Info;
            }
        }

        public DateTime LatestNoteDate
        {
            get
            {
                var note = Notes.OrderByDescending(x => x.AddDate).FirstOrDefault();
                if (note == null)
                {
                    return DateTime.Now;
                }

                return note.AddDate ?? DateTime.Now;
            }
        }
    }
}
