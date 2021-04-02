using HatWorks.Tools;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HatWorks.Models
{
    public class helperServices
    {
        public static dynamic GetById(Entities.AjaxRequest requestobj)
        {
            if (new Helper().IsAuthenticate(0, (Helper.UserOperations)Enum.Parse(typeof(Helper.UserOperations), "goruntuleme")))
            {
                try
                {
                    return string.Format("SELECT * FROM {0} WHERE idd={1}", requestobj.tablename, requestobj.idd).GetDynamicQuery();

                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return false;
        }


        public static Result DeleteByIdandTableName(Entities.AjaxRequestObject requestobj)
        {

            Result result = new Result();
            if (new Helper().IsAuthenticate(0, (Helper.UserOperations)Enum.Parse(typeof(Helper.UserOperations), "goruntuleme")))
            {
                requestobj.tablename = requestobj.tablename.Replace("[", "").Replace("]", "");
                try
                {
                    dynamic model = string.Format("SELECT * FROM {0} WHERE idd={1}", requestobj.tablename, requestobj.idd).GetDynamicQuery();
                    bool hasassings = false;


                    if (!hasassings)
                    {
                        int logId = new Helper().AddLog(requestobj.idd, new Helper().currentmember.idd, requestobj.tablename, "Delete", JsonConvert.SerializeObject(model));
                        MySqlCommand cmd = GenericDataAccess.CreateCommand();
                        cmd.CommandText = string.Format("DELETE FROM {0} WHERE idd ={1}", requestobj.tablename, requestobj.idd);
                        int recid = new GenericDataAccess().ExecuteNonQuery(cmd);
                        result.status = true;
                    }
                    else
                    {
                        result.message = "İlişkili kayıtlar olduğundan silme işlemi gerçekleştirilemedi.";
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    result.status = false;
                    result.message = ex.Message;
                    return result;
                }
            }
            else
            {
                result.status = false;
                result.message = "İzniniz yok";
                return result;
            }


        }

        public static Result selectoptionvalues(Entities.SelectOptionObject request)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            Result resultObj = new Result();
            try
            {
                MySqlCommand cmd = GenericDataAccess.CreateCommand();
                string query = "";
                switch (request.tablename)
                {
                    default:
                        query = "SELECT " + string.Join(",", request.columns) + " FROM " + request.tablename + " WHERE " + (string.IsNullOrEmpty(request.kosul) ? "1=1" : request.kosul) + (string.IsNullOrEmpty(request.orderby) ? "" : " ORDER BY " + request.orderby);
                        query = new Helper().CleanTexts(query);
                        break;
                }
                System.Data.DataTable table = new System.Data.DataTable();
                cmd.CommandText = query;
                table = new GenericDataAccess().ExecuteSelectCommand(cmd);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    result.AppendFormat("<option value='{0}'>{1}</option>", table.Rows[i][request.columns[0]], table.Rows[i][request.columns[1]]);
                }

                resultObj.status = true;
                resultObj.message = result.ToString();
            }
            catch (Exception) { }
            return resultObj;
        }
    }
}
