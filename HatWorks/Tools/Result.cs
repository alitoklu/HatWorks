using HatWorks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Tools
{
    public class Result
    {
        public bool status { get; set; }
        public ErrorType errortype { get; set; }
        public string title { get; set; }
        public string message { get; set; }

        public Result()
        {
            errortype = ErrorType.Success;
            title = "Dikkat!";
            message = "";
        }

        public enum ErrorType
        {
            Success = 0,
            Information = 1,
            UserError = 2,
            SystemError = 3
        }
    }
    public class Results
    {
        public class Info
        {
            public Result result { get; set; }
            public Info()
            {
                result = new Result();
            }
        }

        public class DataResult : Result
        {
            public object data { get; set; }
        }

        public class StaticDatas
        {
            public List<DataItems> DataList { get; set; }
            public object Data { get; set; }
            public Result result { get; set; }
            public class DataItems
            {
                public string id { get; set; }
                public string text { get; set; }
                public string custom1 { get; set; }
                public string custom2 { get; set; }
            }
            public StaticDatas()
            {
                result = new Result();
            }
        }

        public class Member
        {
            public Result result { get; set; }
            public Entities.Member member { get; set; }

            public Member()
            {
                result = new Result();
            }
        }

    }
}
