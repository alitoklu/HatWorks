using HatWorks.Models;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HatWorks.Tools
{
    public class Helper
    {



        public static string dateFormatWithDay { get { return "dd.MM.yyyy dddd"; } }
        public static string cultureKey { get { return "tr-TR"; } }
        public static string dateFormat { get { return "dd.MM.yyyy"; } }
        public static string dateTimeFormat { get { return "dd.MM.yyyy HH:mm"; } }
        public static string dateTimeWithSecondsFormat { get { return "dd.MM.yyyy HH:mm:ss"; } }

        public static string localization { get; set; }

        public int Addassignnotificationsettings(string assignkey, int assigntype, int before_x_day, int before_x_day_time, int on_assigned, int before_x_day_one_time)
        {
            MySqlCommand cmd = GenericDataAccess.CreateCommand();
            cmd.CommandText = "INSERT INTO assign_notification_settings(assignkey, assigntype, before_x_day, before_x_day_time, on_assigned,before_x_day_one_time) " +
                " VALUES(@assignkey, @assigntype, @before_x_day, @before_x_day_time, @on_assigned,@before_x_day_one_time)";

            cmd.Parameters.Add(new MySqlParameter("@assignkey", assignkey));
            cmd.Parameters.Add(new MySqlParameter("@assigntype", assigntype));
            cmd.Parameters.Add(new MySqlParameter("@before_x_day", before_x_day));
            cmd.Parameters.Add(new MySqlParameter("@before_x_day_time", before_x_day_time));
            cmd.Parameters.Add(new MySqlParameter("@on_assigned", on_assigned));
            cmd.Parameters.Add(new MySqlParameter("@before_x_day_one_time", before_x_day_one_time));

            int returnId = 0;
            try
            {
                returnId = new GenericDataAccess().ExecuteNonQuery(cmd);
                //returnId = (int)new GenericDataAccess().ExecuteNonQuery(cmd);
                //returnId = Convert.ToInt32(cmd.LastInsertedId);
            }
            catch (Exception)
            {

            }

            return returnId;
        }



        //private readonly IHttpContextAccessor HttpContextAccessor;
        /// <summary>
        /// Email validate
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool isEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool SendEmail(string to, string subject, string msg)
        {
            bool result = false;
            string logourl = ConfigurationManager.AppSettings["siteurl"].ToString() + ConfigurationManager.AppSettings["sitelogo"].ToString();
            msg = @"<font face='Verdana'><table cellpadding='5' border='0'  style='width:100%;font-size:12px; color:#777;border-collapse:collapse;'>
			<tbody><tr>
				<td style='padding:10px;background:#004183;'>
					<table cellpadding='5' border='0' style='width:100%;border-collapse:collapse;font-size:12px;color:#46b1d5;'>
						<tbody><tr>
							<td style='width:250px;color:#fff;'><img src='" + logourl + @"' alt='" + ConfigurationManager.AppSettings["Mail:SenderName"].ToString() + @"' style='height:40px'></td>
							<td style='font-size:16px; text-align:right;'>
                               
							</td>
						</tr>
					</tbody></table>
				</td>
			</tr>
		<tr>
			<td style='padding:25px 15px;'>" + msg + @"</td>
		</tr>
		<tr>
			<td style='padding:10px;text-align:center;'><a href='" + ConfigurationManager.AppSettings["siteurl"].ToString() + @"' style='text-decoration:none;' title='' target='_blank'>" + ConfigurationManager.AppSettings["Mail:SenderName"].ToString() + @"</a></td>
		</tr>
		<tr>
				<td style='padding:10px;'>
				</td>
			</tr>
			<tr>
				<td style='padding:10px;'>
					<table cellpadding='5' border='0' width='100%' style='font-size:12px;color:#333;'>
						<tbody><tr>
							<td style='background:#eee;padding:10px; font-weight:bold;'>Bu e-postaya gönderilen yanıtlar alınmaz. Lütfen,bu e-postaya cevap vermeyiniz. Her türlü sorunuz için " + ConfigurationManager.AppSettings["Mail:ReceiverAddress"].ToString() + @" adresine yazınız.</td>
						</tr>
					</tbody></table>
				</td>
			</tr>
	</tbody></table></font>";

            //System.Threading.Tasks.Task.Run(() =>
            //{
            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Mail:Hosting"].ToString());
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Mail:UserName"].ToString(), ConfigurationManager.AppSettings["Mail:PassWord"].ToString());
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Mail:Port"]);

                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["Mail:SSL"]);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Mail:UserName"].ToString(), ConfigurationManager.AppSettings["Mail:SenderName"].ToString());
                mailMessage.To.Add(to);
                mailMessage.Body = msg;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                client.SendCompleted += (s, e) =>
                {
                    client.Dispose();
                    mailMessage.Dispose();
                };

                client.SendAsync(mailMessage, null);

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            //});

            return result;

        }

        //public static string ClearHtml(string input)
        //{
        //    string text = "";
        //    if (!string.IsNullOrEmpty(input))
        //    {
        //        text = System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", String.Empty);
        //    }
        //    return text;
        //}
        public static string ClearHtml(string input, int length)
        {
            string text = "";
            if (!string.IsNullOrEmpty(input))
            {
                text = System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", String.Empty);
                if (text.Length > length)
                    text = text.Substring(0, length) + "...";
            }
            return text;
        }


        //public Entities.Member currentuser //DECLARE @memberID as varchar(50)='37F0E033-F649-4AB4-A9F5-A33EE03AFE4A';
        //{
        //    get
        //    {
        //        Entities.Member result = new Entities.Member();
        //        try
        //        {
        //            result = Context._current().Session.GetObject<Entities.Member>("user");

        //        }
        //        catch (Exception ex)
        //        {
        //            result.id = 0;
        //        }


        //        return result;
        //    }
        //}


        public static string Translator(string key, string defaultvalue)
        {
            string result = "";
            //bool tryfind = new Helper().dictList.TryGetValue(key, out result);

            //string defaultlang = Helper.localization;
            //Entities.Language aktifdil = new Entities.Language();

            //aktifdil = GetLanguage;

            //if (!tryfind && (defaultlang == aktifdil.languagecode.ToLower()))
            //{

            //    Task.Run(() =>
            //    {

            //        Entities.LanguageTranslate translate = new Entities.LanguageTranslate();
            //        translate.keyword = key;
            //        translate.languagecode = defaultlang;
            //        translate.translate = defaultvalue;

            //        bool rid = new languageServices().addTranslate(translate);

            //    });
            //}

            return string.IsNullOrEmpty(result) ? defaultvalue : result;
        }

        //public static Entities.Language GetLanguage
        //{
        //    get
        //    {

        //        List<Entities.Language> itemList = new List<Entities.Language>();
        //        itemList = new languageServices().GetLanguages();

        //        return itemList.FirstOrDefault(res => res.languagecode == localization);
        //    }
        //}

        public Dictionary<string, string> dictList
        {
            get
            {
                Dictionary<string, string> myDict = new Dictionary<string, string>();

                //if (HttpContextAccessor.HttpContext.Session.GetString("dictionary") != null)
                //{
                //    string dil = "tr";
                //    //dil = wg2WebLibrary.general.getConfig("varsayilan_dil");

                //    SqlCommand cmd = new GenericDataAccess().CreateCommand();
                //    try
                //    {
                //        //if (Utilities.kullanici != null)
                //        //{
                //        //    cmd.CommandText = "SELECT * FROM sistem_dil_cevirileri WHERE dil_kodu = @dil_kodu";
                //        //    cmd.Parameters.Add(new SqlParameter("@dil_kodu", Utilities.kullanici.dil));
                //        //}
                //        //else
                //        //{
                //        cmd.CommandText = "SELECT lt.* FROM languages_translates lt JOIN languages l ON lt.languagecode= l.languagecode WHERE l.languagecode=@dil_kodu";
                //        cmd.Parameters.Add(new GenericDataAccess().CreateParameter("@dil_kodu", dil));
                //        //}

                //    }
                //    catch (Exception ex)
                //    {
                //        string c = ex.Message;
                //        cmd.CommandText = "SELECT lt.* FROM languages_translates lt JOIN languages l ON lt.languagecode= l.languagecode WHERE l.languagecode=@dil_kodu";
                //        cmd.Parameters.Add(new GenericDataAccess().CreateParameter("@dil_kodu", dil));
                //    }

                //    DataTable table = new DataTable();
                //    table = new GenericDataAccess().ExecuteSelectCommand(cmd);

                //    for (int i = 0; i < table.Rows.Count; i++)
                //    {
                //        myDict.Add(table.Rows[i]["keyword"].ToString(), table.Rows[i]["translate"].ToString());
                //    }

                //    HttpContextAccessor.HttpContext.Session.SetObject("dictionary", myDict);
                //}
                //else
                //    myDict = (Dictionary<string, string>)HttpContextAccessor.HttpContext.Session.GetObject<Dictionary<string, string>>("dictionary");
                return myDict;
            }
        }


        public string DataTableToJSON(DataTable table)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in table.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(rows);
        }

        public static bool IsGuid(string value)
        {
            Guid x;
            return Guid.TryParse(value, out x);
        }

        //public string ImageToBase64(string Path, int width)
        //{
        //    using (System.Drawing.Image image = System.Drawing.Image.FromFile(System.Net.WebUtility.MapPath(Path)))
        //    {
        //        using (System.IO.MemoryStream m = new System.IO.MemoryStream())
        //        {
        //            image.Save(m, image.RawFormat);
        //            System.Drawing.Image result = ResizeImage(image, width);

        //            using (var ms = new System.IO.MemoryStream())
        //            {
        //                result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                string base64String = Convert.ToBase64String(ms.ToArray());
        //                return base64String;
        //            }

        //        }
        //    }
        //}

        //private System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, int width)
        //{
        //    double clc = ((double)width / (double)imgToResize.Width);
        //    double height = Math.Round((clc * (double)imgToResize.Height), 0, MidpointRounding.AwayFromZero);
        //    return (System.Drawing.Image)(new System.Drawing.Bitmap(imgToResize, new System.Drawing.Size(width, Convert.ToInt32(height))));
        //}


        public bool IsAuthenticate(int islem_kodu, UserOperations op)
        {
            //kullanici_yetkileriEntity ky = new kullanici_yetkileriEntity();
            //ky = aktif_kullanici_yetkileri.Find(res => res.islem_kodu == islem_kodu);
            //bool result = false;
            //if (ky != null)
            //    result = (bool)ky.GetType().GetProperty(op.ToString()).GetValue(ky, null);

            //return result;
            try
            {
                if (currentmember != null && currentmember.idd > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public Result GeneralUpdate(List<Entities.TableObject> tableObj, int idd, string tablename, params object[] methodsAndParams)
        {
            Result result = new Result();

            List<string> tables = new List<string>();



            int maxlength;
            string[] classes;

            bool isAuth = false;
            isAuth = IsAuthenticate(1, idd > 0 ? Helper.UserOperations.duzenleme : Helper.UserOperations.ekleme);

            if (isAuth)
            {
                Cipher cipher = new Cipher();

                List<Entities.TableObject> controlgroup = tableObj.Where(a => a.columncontrolgroup != null && bool.Parse(a.columncontrolgroup)).ToList();
                if (controlgroup.Count > 0)
                {
                    string checkStr = "";// "isdeleted=0";
                    for (int i = 0; i < controlgroup.Count; i++)
                    {
                        if (i > 0)
                            checkStr += " AND ";
                        checkStr += controlgroup[i].columnname + "='" + controlgroup[i].columnvalue + "' ";
                    }
                    if (Helper.IsExistValueInDB(tablename, checkStr, new int[] { idd }))
                    {

                        result.status = false;
                        result.title = "-2";
                        result.message = string.Join(", ", controlgroup.Select(res => res.columndisplayname)) + " kullanılmaktadır"; //Helper.Translator("seri_sira_mevcut", "Bu seri sıra no kullanılmaktadır");

                        return result;

                    }
                }

                for (int i = 0; i < tableObj.Count; i++)
                {
                    if (tableObj[i].columnname == "password")
                    {
                        tableObj[i].columnvalue = cipher.Encrypt(tableObj[i].columnvalue.ToString(), ConfigurationManager.AppSettings["cipherpassword"].ToString());
                    }

                    if (tableObj[i].columncheck)
                    {
                        if (tableObj[i].columnclasses.Contains("bitValue") && tableObj[i].columnvalue.ToString() == "0")
                        {

                        }
                        else
                        {
                            string checkValueString = string.Empty;
                            if (tableObj[i].columntype == "string")
                            {
                                checkValueString = tableObj[i].columnname + "='" + tableObj[i].columnvalue + "' ";//AND isdeleted = 0
                            }
                            else
                            {
                                checkValueString = tableObj[i].columnname + "=" + tableObj[i].columnvalue + " ";//AND isdeleted = 0
                            }
                            if (Helper.IsExistValueInDB(tablename, checkValueString, new int[] { idd }))
                            {
                                result.status = false;
                                result.title = "-2";
                                result.message = Helper.Translator("zaten_mevcut", tableObj[i].columndisplayname + " mevcut.");

                                return result;
                            }
                        }
                    }

                    if (tableObj[i].columntype == "string" && tableObj[i].columnmaxlength != "")
                    {
                        try
                        {
                            maxlength = int.Parse(tableObj[i].columnmaxlength);
                            if (System.Net.WebUtility.UrlDecode(tableObj[i].columnvalue.ToString()).Length > maxlength)
                            {
                                result.status = false;
                                result.message = tableObj[i].columndisplayname + " " + "maksimum karakter sayısı :" + maxlength;
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            result.status = false;
                            result.message = ex.Message;
                            break;
                        }
                    }


                    if (tableObj[i].columnclasses != null && tableObj[i].columnclasses != "")
                    {
                        classes = tableObj[i].columnclasses.Split(' ');
                        bool isExitLoop = false;

                        for (int j = 0; j < classes.Length; j++)
                        {
                            switch (classes[j])
                            {
                                case "notnull":
                                    if (tableObj[i].columnvalue == "")
                                    {
                                        result.status = false;
                                        result.message = tableObj[i].columndisplayname + " " + "boş geçilemez";
                                        isExitLoop = true;
                                    }
                                    break;
                                case "numeric":
                                    foreach (char item in tableObj[i].columnvalue.ToString())
                                    {
                                        if (!Char.IsDigit(item))
                                        {
                                            result.status = false;
                                            result.message = tableObj[i].columndisplayname + " " + "numerik karakter içermeli";
                                            isExitLoop = true;
                                        }
                                    }
                                    break;
                            }
                            if (isExitLoop)
                            {
                                break;
                            }
                        }
                    }
                }

                if (!result.status)
                {
                    try
                    {

                        System.Text.StringBuilder query = new System.Text.StringBuilder();
                        string[] columns = new string[tableObj.Count];

                        for (int i = 0; i < tableObj.Count; i++)
                        {
                            //if (!string.IsNullOrEmpty(tableObj[i].columnvalue))
                            columns[i] = new Helper().CleanTexts(tableObj[i].columnname);
                        }

                        //columns = columns.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                        if (idd > 0)
                        {
                            string[] uquery = new string[columns.Length];
                            for (int i = 0; i < columns.Length; i++)
                            {
                                // uquery[i] = columns[i] + "=" + columns[i];
                                uquery[i] = columns[i] + "=@" + columns[i];
                            }
                            query.AppendFormat("UPDATE {0} SET {1} WHERE idd={2}{3}", tablename, string.Join(",", uquery), idd, "");
                        }
                        else
                        {
                            query.AppendFormat("INSERT INTO {0}({1}) VALUES({2}); SELECT LAST_INSERT_ID();", tablename, string.Join(",", columns), "@" + string.Join(",@", columns));
                        }


                        MySqlCommand cmd = GenericDataAccess.CreateCommand();
                        cmd.CommandText = query.ToString();
                        cmd.CommandType = CommandType.Text;
                        for (int i = 0; i < columns.Length; i++)
                        {
                            switch (tableObj[i].columntype)
                            {
                                case "date":
                                    if (string.IsNullOrEmpty(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()))
                                    {
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], System.Data.SqlTypes.SqlDateTime.Null));
                                    }
                                    else
                                    {
                                        DateTime ndate = DateTime.ParseExact(new Helper().CleanTexts(System.Net.WebUtility.UrlDecode(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString())), Helper.dateFormat, null);
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], ndate));
                                    }

                                    break;
                                case "datetime":
                                    if (string.IsNullOrEmpty(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()))
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], System.Data.SqlTypes.SqlDateTime.Null));
                                    else
                                    {
                                        DateTime ndate = DateTime.ParseExact(new Helper().CleanTexts(System.Net.WebUtility.UrlDecode(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString())), Helper.dateTimeFormat, null);
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], ndate));
                                    }

                                    break;
                                case "int":
                                    if (string.IsNullOrEmpty(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()))
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], System.Data.SqlTypes.SqlInt32.Null));
                                    else
                                    {
                                        int value = int.Parse(new Helper().CleanTexts(System.Net.WebUtility.UrlDecode(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString())));
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], value));
                                    }
                                    break;
                                case "decimal":
                                    if (string.IsNullOrEmpty(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()))
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], System.Data.SqlTypes.SqlDecimal.Null));
                                    else
                                    {
                                        decimal value = decimal.Parse(new Helper().CleanTexts(System.Net.WebUtility.UrlDecode(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString())), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], value));
                                    }
                                    break;
                                case "float":
                                    if (string.IsNullOrEmpty(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()))
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], System.Data.SqlTypes.SqlDecimal.Null));
                                    else
                                    {
                                        float value = float.Parse(new Helper().CleanTexts(System.Net.WebUtility.UrlDecode(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString())), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], value));
                                    }
                                    break;
                                default:
                                    if (string.IsNullOrEmpty(tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()))
                                    {
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], DBNull.Value));
                                    }
                                    else
                                    {
                                        cmd.Parameters.Add(new MySqlParameter("@" + columns[i], tableObj.Find(res => res.columnname == columns[i]).columnvalue.ToString()));
                                    }
                                    break;
                            }
                        }

                        int itemid = 0;
                        if (idd > 0)
                        {
                            Dictionary<string, string> newItem = new Dictionary<string, string>();
                            for (int i = 0; i < tableObj.Count; i++)
                            {
                                newItem.Add(tableObj[i].columnname, tableObj[i].columnvalue.ToString());
                            }


                            MySqlCommand cmdex = GenericDataAccess.CreateCommand();
                            cmdex.CommandText = string.Format("SELECT * FROM {0} WHERE idd={1}", tablename, idd);

                            DataTable table = new DataTable();
                            table = new GenericDataAccess().ExecuteSelectCommand(cmdex);


                            itemid = new GenericDataAccess().ExecuteNonQuery(cmd);


                            // Log
                            string json = "{\"eski\":" + new Helper().DataTableToJSON(table).TrimStart('[').TrimEnd(']') + ",\"yeni\":" + JsonConvert.SerializeObject(newItem) + "}";
                            int logId = new Helper().AddLog(idd, currentmember.idd, tablename, "Düzenlendi", json);
                        }
                        else
                        {
                            var snc = new GenericDataAccess().ExecuteScalar(cmd);
                            itemid = int.Parse(snc.ToString());

                            new Helper().AddLog(itemid, currentmember.idd, tablename, "Yeni İşlem", string.Empty);
                        }

                        if (itemid > 0)
                        {
                            result.status = true;
                            result.title = idd > 0 ? idd.ToString() : itemid.ToString();
                            result.message = "";
                        }
                        else
                        {
                            result.status = false;
                            result.message = "Kayıt başarısız.";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.status = false;
                        result.message = ex.Message;
                    }
                }
            }
            else
            {
                result.status = false;
                result.message = "Bu işlem için yetkiniz yok.";
            }

            return result;
        }

        public static int[] ConvertToIntArray(object[] inputArray)
        {
            int[] outputArray = new int[inputArray.Length];

            for (int i = 0; i < inputArray.Length; i++)
            {
                outputArray[i] = (int)Convert.ChangeType(inputArray[i], typeof(int));
            }

            return outputArray;
        }

        public string CleanTexts(string txtResponse)
        {
            txtResponse = txtResponse.Replace("'", "&#39;");
            //txtResponse = txtResponse.Replace("=", "&#61;");
            txtResponse = txtResponse.Replace("-", "&minus;");
            txtResponse = txtResponse.Replace("<", "&lt;");
            txtResponse = txtResponse.Replace(">", "&gt;");

            return txtResponse;
        }

        public int AddLog(int recordId, int userId, string tableName, string operation, string info)
        {
            MySqlCommand cmd = GenericDataAccess.CreateCommand();
            cmd.CommandText = "INSERT INTO logs(recordid, memberid, processname, processtype, information) OUTPUT INSERTED.id VALUES(_recordId, _userId, _processname, _processtype, _information)";

            cmd.Parameters.Add(new MySqlParameter("_recordId", recordId));
            cmd.Parameters.Add(new MySqlParameter("_userId", userId));
            cmd.Parameters.Add(new MySqlParameter("_processname", operation));
            cmd.Parameters.Add(new MySqlParameter("_information", info));
            cmd.Parameters.Add(new MySqlParameter("_processtype", tableName));

            int returnId = 0;
            try
            {
                returnId = (int)new GenericDataAccess().ExecuteScalar(cmd);
                //returnId = (int)new GenericDataAccess().ExecuteNonQuery(cmd);
                //returnId = Convert.ToInt32(cmd.LastInsertedId);
            }
            catch (Exception ex)
            {
                string hta = ex.Message;
            }

            return returnId;
        }

        public static bool IsExistValueInDB(string tableName, string filters, int[] IDs)
        {
            MySqlCommand cmd = GenericDataAccess.CreateCommand();
            cmd.CommandText = string.Format("SELECT * FROM {0} WHERE {1}", tableName, filters);

            //cmd.Parameters.Add(new SqlParameter("@tableName", tableName));
            //cmd.Parameters.Add(new SqlParameter("@filters", filters));


            DataTable table = new DataTable();
            table = new GenericDataAccess().ExecuteSelectCommand(cmd);

            bool result = table.Rows.Count > 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (IDs.Contains((int)table.Rows[i]["idd"]))
                    result = false;
            }

            return result;

        }

        public enum UserOperations
        {
            goruntuleme,
            ekleme,
            silme,
            duzenleme,
            geri_alma,
            yazdirma,
            seri_degistirme,
            fiyat_degistirme,
            tarih_degistirme,
            onay_verme
        }


        public static int CurrentId
        {
            get
            {

                return System.Convert.ToInt32(Context._current().User.Identity.NameIdentifier());

            }
        }

        public Entities.Member currentmember
        {
            get
            {
                Entities.Member _currentmember = new Entities.Member();
                try
                {
                    string currentuser = Context._current().Session.GetString("currentuser");
                    if (string.IsNullOrEmpty(currentuser))
                    {
                        _currentmember = string.Format("SELECT * FROM members WHERE idd=" + CurrentId + " AND status=1").GetQuery<Entities.Member>()[0];
                        Context._current().Session.SetObject("currentuser", _currentmember);
                    }
                    else
                    {
                        _currentmember = Context._current().Session.GetObject<Entities.Member>("currentuser");
                    }
                }
                catch { }

                if (_currentmember != null && _currentmember.idd > 0)
                    return _currentmember;
                else
                    return new Entities.Member();
            }
        }

        public static List<string> Roles
        {
            get
            {
                return new Helper().currentmember.role.Split(',').ToList();
            }
        }

        public static bool HasAuthority(string[] authnames)
        {
            bool result = false;
            for (int i = 0; i < authnames.Length; i++)
            {
                if (Roles.Contains(authnames[i]))
                    result = true;
            }

            return result;
        }


        public static string edutypeurl(int t)
        {
            string a = "";
            switch (t)
            {
                case 0:
                    a = "/egitim";

                    break;
                case 1:
                    a = "/sinifegitimleri/siniflar";
                    break;
                case 2:
                    a = "/egitim/video";
                    break;
            }
            return a;
        }


        public static string edutype(int t)
        {
            string a = "";
            switch (t)
            {
                case 0:
                    a = "Scorm ";

                    break;
                case 1:
                    a = "Sınıf Eğitimi";
                    break;
                case 2:
                    a = "Video";
                    break;
                case 3:
                    a = "Sınav";
                    break;
                case 4:
                    a = "Anket";
                    break;
                case 5:
                    a = "Dosya";
                    break;
            }
            return a;
        }


    }
}
