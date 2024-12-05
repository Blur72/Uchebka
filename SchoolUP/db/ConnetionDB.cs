using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolUP.db
{
    class ConnetionDB 
    {
        public static aeEntities1 db = new aeEntities1();
        public static Disciplines disciplines;
        public static Exam exam;
        public static Faculty faculty;
        public static Engineer engineer;
        public static Department department;
        public static Teacher teacher;
        public static Employee employee;
        public static Specialities specialities;
        public static Student student;
        public static Zav_Kaf zav_Kaf;
        public static Request request;
    }
}
