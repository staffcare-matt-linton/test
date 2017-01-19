using System;

namespace ClassLibrary1
{
    public interface IPatient
    {
        DateTime patientDOB { get; set; }
        string patientId { get; set; }
        string patientName { get; set; }
    }
}