using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using MySql.Data.MySqlClient;
using Demo.Models;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using Plugin.Media.Abstractions;

namespace Demo.Service {

public class Mysql_Result_For_GetUserToDoList {

    public string serial_no {
        get;
        set;
    }

    public string ichk_name {
        get;
        set;
    }

    public string ichk_rev {
        get;
        set;
    }

}

public class Mysql_Result_For_GetUserCheckItem {

    public string doc_item {
        get;
        set;
    }

    public string check_status_desc {
        get;
        set;
    }

    public string ichk_item_desc {
        get;
        set;
    }
    public string check_point {
        get;
        set;
    }
    public string check_result_desc {
        get;
        set;
    }
    public string check_comment {
        get;
        set;
    }
    public string photo_path {
        get;
        set;
    }
    public string ichk_group_desc {
        get;
        set;
    }
}

public class MysqlService {

    ConnectServerInfo cntInfo = new ConnectServerInfo();
    Dictionary<string, string> postData;
    ClientUser m_ClientUser;

    public MysqlService(ClientUser clientUser) {
        m_ClientUser = clientUser;
    }

    private void ConnectInitailInfo() {
        postData = new Dictionary<string, string>();
        postData.Add("username", m_ClientUser.Login_ID);
        postData.Add("password", m_ClientUser.Login_Password);
        postData.Add("dbip", cntInfo.db_Ip);
        postData.Add("dbname", cntInfo.db_Name);

    }

    private void PostDataToApi(string uri, string postJson, MediaFile file, string fileName, out string resultObj) {

        //   HttpClient client = new HttpClient();
        // HttpContent contentPost = new StringContent(postJson, Encoding.UTF8, "application/json");
        // HttpResponseMessage response = client.PostAsync(uri, contentPost).GetAwaiter().GetResult();
        //   resultObj = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        using (var client = new HttpClient()) {
            try {
                client.Timeout = TimeSpan.FromSeconds(10);
                //HttpContent contentPost = new StringContent(postJson, Encoding.UTF8, "application/json");
                MultipartFormDataContent content = new MultipartFormDataContent();
                StringContent jsonPart = new StringContent(postJson.ToString());
                //  jsonPart.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                //   jsonPart.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                content.Add(jsonPart, "json");

                if (file != null) {
                    byte[] bytes = StreamToBytes(file.GetStream());
                    content.Add(new ByteArrayContent(bytes, 0, bytes.Length), "ErrorImage", fileName);
                }

                HttpResponseMessage response = client.PostAsync(uri, content).GetAwaiter().GetResult();
                resultObj = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            } finally {
                client.CancelPendingRequests();
                client.Dispose();
            }
        }
    }
    public byte[] StreamToBytes(Stream stream) {
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);

        // 設置當前流的位置為流的開始
        stream.Seek(0, SeekOrigin.Begin);
        return bytes;
    }

    public bool UserVerificationService() {

        ConnectInitailInfo();
        try {

            string URI = "http://" + cntInfo.server_Ip + "/api/VerificationUser.php";
            string postJson = JsonConvert.SerializeObject(postData);
            string result;

            PostDataToApi(URI, postJson, null, "", out result);

            if ((string)result == "true") {
                m_ClientUser.Verification = true;
            } else {
                m_ClientUser.Verification = false ;
            }

            Debug.WriteLine("\r\nUserVerificationService Result = " + result);
        } catch(Exception ex) {
            Debug.WriteLine("UserVerificationService Fail = " + ex);
        } finally {

        }



        return true;
    }

    public void GetUserToDoList() {
        ConnectInitailInfo();

        try {
            string URI = "http://" + cntInfo.server_Ip + "/api/GetClientUserWorkList.php";
            postData.Add("applicant_id", m_ClientUser.Login_ID);
            string postJson = JsonConvert.SerializeObject(postData);
            string result;
            PostDataToApi(URI, postJson, null, "", out result);
            Debug.WriteLine(result);
            List<Mysql_Result_For_GetUserToDoList> mysql_Result = JsonConvert.DeserializeObject<List<Mysql_Result_For_GetUserToDoList>>(result);
            m_ClientUser.User_ToDo_List = new List<CheckList>();

            foreach (Mysql_Result_For_GetUserToDoList item in mysql_Result) {
                m_ClientUser.User_ToDo_List.Add(
                new CheckList {
                    Serial_No = item.serial_no,
                    Check_Name = item.ichk_name,
                    CheckVersion = item.ichk_rev
                }
                );
            }



        } catch(Exception ex) {
            Debug.WriteLine("GetUserToDoList Fail = " + ex);
        }



    }

    public void GetUserCheckItem() {
        ConnectInitailInfo();

        try {
            string URI = "http://" + cntInfo.server_Ip + "/api/GetUserCheckItems.php";
            postData.Add("serial_no", m_ClientUser.Selected_CheckList.Serial_No);
            string postJson = JsonConvert.SerializeObject(postData);
            string result;
            PostDataToApi(URI, postJson, null, "", out result);
            Debug.WriteLine(result);
            List<Mysql_Result_For_GetUserCheckItem> mysql_Result = JsonConvert.DeserializeObject<List<Mysql_Result_For_GetUserCheckItem>>(result);
            m_ClientUser.Selected_CheckList.MCheckList = new List<CheckItem>();

            foreach (Mysql_Result_For_GetUserCheckItem item in mysql_Result) {
                m_ClientUser.Selected_CheckList.MCheckList.Add(
                new CheckItem {
                    Doc_Item = item.doc_item,
                    Check_Status_Desc = item.check_status_desc,
                    IChk_Item_Desc = item.ichk_item_desc,
                    Check_Point = item.check_point,
                    Check_Result_Desc = item.check_result_desc,
                    Check_Comment = item.check_comment,
                    Photo_Path = item.photo_path,
                    Ichk_Group_Desc = item.ichk_group_desc
                }
                );
            }

        } catch (Exception ex) {
            Debug.WriteLine("GetUserToDoList Fail = " + ex);
        }

    }

    public bool Update_Doc_Item(MediaFile file) {
        ConnectInitailInfo();

        try {
            string URI = "http://" + cntInfo.server_Ip + "/api/Update_Doc_Item.php";

            postData.Add("serial_no", m_ClientUser.Selected_CheckList.Serial_No);
            postData.Add("doc_item", m_ClientUser.Selected_CheckItem.Doc_Item);

            if (m_ClientUser.Selected_CheckItem.Check_Result_Desc == "堪用") {
                postData.Add("check_result", "1");
                postData.Add("photo_path", "1");
            } else if (m_ClientUser.Selected_CheckItem.Check_Result_Desc == "正常") {
                postData.Add("check_result", "0");
                postData.Add("photo_path", "0");
            } else if (m_ClientUser.Selected_CheckItem.Check_Result_Desc == "異常") {
                postData.Add("check_result", "2");
                postData.Add("photo_path", "2");
            }

            string fileName =  m_ClientUser.Login_ID + "_" + m_ClientUser.Selected_CheckList.Serial_No + "_" + m_ClientUser.Selected_CheckItem.Doc_Item + "_" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".jpg";


            string postJson = JsonConvert.SerializeObject(postData);
            string result;

            Console.WriteLine("SQL = " + postJson);

            PostDataToApi(URI, postJson, file, fileName, out result);


            Console.WriteLine("Result = " + result);

            if (result != "Success") {
                return false;
            }


            //List<Mysql_Result_For_GetUserCheckItem> mysql_Result = JsonConvert.DeserializeObject<List<Mysql_Result_For_GetUserCheckItem>>(result);
            //m_ClientUser.Selected_CheckList.MCheckList = new List<CheckItem>();

            //foreach (Mysql_Result_For_GetUserCheckItem item in mysql_Result) {
            //    m_ClientUser.Selected_CheckList.MCheckList.Add(
            //    new CheckItem {
            //        Doc_Item = item.doc_item,
            //        Check_Status_Desc = item.check_status_desc,
            //        IChk_Item_Desc = item.ichk_item_desc,
            //        Check_Point = item.check_point,
            //        Check_Result_Desc = item.check_result_desc,
            //        Check_Comment = item.check_comment,
            //        Photo_Path = item.photo_path,
            //        Ichk_Group_Desc = item.ichk_group_desc
            //    }
            //    );
            //}

        } catch (Exception ex) {
            Debug.WriteLine("GetUserToDoList Fail = " + ex);
            return false;
        }
        return true;
    }

    public bool Update_Doc_Header() {
        ConnectInitailInfo();

        try {
            string URI = "http://" + cntInfo.server_Ip + "/api/Update_Doc_Header.php";

            postData.Add("serial_no", m_ClientUser.Selected_CheckList.Serial_No);


            string postJson = JsonConvert.SerializeObject(postData);
            string result;

            Console.WriteLine("SQL = " + postJson);

            PostDataToApi(URI, postJson, null, "", out result);


            Console.WriteLine("Result = " + result);

            if (result != "Success") {
                return false;
            }



        } catch (Exception ex) {
            Debug.WriteLine("Update Header Fail = " + ex);
            return false;
        }
        return true;
    }
}
}