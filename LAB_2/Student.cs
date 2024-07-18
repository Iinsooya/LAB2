using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_2
{
    internal class Student
    {
        private int id { get; set; }
        private string FIO;
        private int Number;

        public Student() { }
        public Student(string FIO, int Number)
        {
            this.FIO = FIO;
            this.Number = Number;
        }
    }

}
