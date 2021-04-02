using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Models
{
    public class Entities
    {
        public class TableObject
        {
            public string columnname { get; set; }
            public object columnvalue { get; set; }
            public string columntype { get; set; }
            public string columnmaxlength { get; set; }
            public string columnclasses { get; set; }
            public string columndisplayname { get; set; }
            public bool columncheck { get; set; }
            public bool columnstatus { get; set; }
            public string columnpercent { get; set; }
            public string columncontrolgroup { get; set; }
        }
        public class Member
        {
            public int idd { get; set; }
            //public string accountid { get; set; }
            public int companyid { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            //public string phone { get; set; }
            public string username { get; set; }
            public string role { get; set; }
            public string password { get; set; }
            //public int status { get; set; }
            //public int? supervisorid { get; set; }
            //public string supervisorname { get; set; }
            //public DateTime? lastlogindate { get; set; }
            //public DateTime recorddate { get; set; }

            //public Guid userid { get; set; }

            public Member()
            {
                idd = 0;
            }
        }
        public class AjaxRequest
        {
            public int idd { get; set; }
            public string tablename { get; set; }
        }
        public class AjaxRequestObject
        {
            public List<TableObject> table { get; set; }
            public int idd { get; set; }
            public string tablename { get; set; }
            public List<Entities.DefaultColumns> defaults { get; set; }
        }
        public class DefaultColumns
        {
            public string source { get; set; }
            public string target { get; set; }
            public bool controlgroup { get; set; }
        }
        public class SelectOptionObject
        {
            public string tablename { get; set; }
            public List<string> columns { get; set; }
            public string kosul { get; set; }
            public string orderby { get; set; }
        }
    }
}
