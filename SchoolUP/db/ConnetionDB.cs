using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolUP.db
{
    class ConnetionDB : DbContext
    {
        public static UPEntities1 db = new UPEntities1();
        public static Sotrudnik Sotrudnik;
        public static Kafedra kafedra;
    }
}
