using CodeLicker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLicker.Infrastructure
{
    public static class DBManager
    {
        public static CLDBEntities Context { get; set; }
    }
}
