using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeganCRMTool
{
    public partial class Person
    {
        private string _latestNote;

        public string LatestNote
        {
            get
            {
                return Notes.OrderByDescending(x => x.AddDate).First().Info;
            }
        }

    }
}
