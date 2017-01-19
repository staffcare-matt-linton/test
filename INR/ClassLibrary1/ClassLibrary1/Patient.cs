using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Patient : IPatient
    {
        public string patientId { get; set; }

        public string patientName { get; set; }

        public DateTime patientDOB { get; set; }


    }
}
