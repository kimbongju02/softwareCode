using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace WindowsFormsApp1
{

    public enum Allergic
    {
        None = 0,
        Egg = 1,//난류
        Milk = 1 << 1,//우유
        Flour = 1 << 2,//곡류
        Crab = 1 << 3,//갑각류
        Almond = 1 << 4,//견과류
        Fish = 1 << 5,//생선류
        Molluscs = 1 << 6,//연체류
        Beef = 1 << 7,//육류
        Soybean = 1 << 8,//대두류
        /*Chicken = 1 << 8
        Pork = 1 << 4,*/
    }
    public class FoodDB
    {
        public string ReportNo { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public Allergic? Allergy { get; set; }
        public Dictionary<string, string> Comment;

        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=softwareeng.kro.kr)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));;User Id=SoftEng;Password=1";
        public static List<FoodDB> getter(string searchName = "")
        {
            List<FoodDB> products = new List<FoodDB>();
            products = SearchDB(searchName);
            if (products.Count == 0)
            {
                products = GetApiData(searchName);
                InsertDataToDB(products);
            }
            return products;
        }
        private static List<FoodDB> SearchDB(string searchName)
        {

            List<FoodDB> Results = new List<FoodDB>();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                // 데이터베이스 연결
                connection.Open();
                string sql;
                if (searchName != "")
                    sql = $"SELECT foodinfo.foodindex,foodname,foodmanu,foodallergy FROM FoodInfo WHERE foodname LIKE '%{searchName}%'";
                else
                    sql = "SELECT foodinfo.foodindex,foodname,foodmanu,foodallergy FROM FoodInfo";
                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)// 결과가 비어있으면 빈리스트 반환
                            return new List<FoodDB>();
                        while (reader.Read())
                        { // 결과 데이터 처리
                            FoodDB product = new FoodDB();
                            product.ReportNo = reader.GetString(0);
                            product.Name = reader.GetString(1);
                            product.Manufacturer = reader.GetString(2);
                            product.Allergy = (Allergic)reader.GetInt32(3);
                            product.Comment=GetCommentData(product.ReportNo);
                            Results.Add(product);
                        }
                    }
                }
                connection.Close();
            }
            return Results;
        }
        private static Dictionary<string, string> GetCommentData(string FoodIndex)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
                using (OracleConnection connection = new OracleConnection(connectionString))
                {    // 데이터베이스 연결
                    connection.Open();
                    string sql;
                    sql = $"SELECT nickname,commentdetail FROM FoodComment WHERE foodcomment.foodindex='{FoodIndex}'";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return new Dictionary<string, string>();
                            while (reader.Read())
                            {   // 결과 데이터 처리
                                Dictionary<string, string> temp = new Dictionary<string, string>();
                                result[reader.GetString(0)] = reader.GetString(1);
                                //try
                                //{
                                //    result[reader.GetString(0)] = reader.GetString(1);
                                //}
                                //catch
                                //{
                                //    return result;
                                //}

                            }
                        }
                    }
                    connection.Close();
                }
                return result;
            }

        public static bool AddComment(string NickName, string FoodIndex, string Comment)
        {

            //try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {   // 데이터베이스 연결
                    connection.Open();
                    string sql = $"INSERT INTO foodcomment (nickname,foodindex,commentdetail) VALUES ('{NickName}','{FoodIndex}','{Comment}')";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                return false;
            }
            //catch
            //{
            //    return true;
            //}
        }
        public static bool ModifyComment(string NickName, string FoodIndex, string Comment)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {  // 데이터베이스 연결
                    connection.Open();
                    string sql;
                    if (Comment == "")
                        sql = $"DELETE FROM foodcomment WHERE nickname = '{NickName}' AND foodindex ='{FoodIndex}')";
                    else
                        sql = $"UPDATE foodcomment SET commentdetail = '{Comment}' WHERE nickname = '{NickName}' AND foodindex ='{FoodIndex}')";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        public static bool DeleteComment(string NickName, string FoodIndex, string Comment)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                { // 데이터베이스 연결
                    connection.Open();
                    string sql = $"DELETE FROM foodcomment WHERE foodindex = '{FoodIndex}'AND nickname = '{NickName}'";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        private static void InsertDataToDB(List<FoodDB> products)
        {

            using (OracleConnection connection = new OracleConnection(connectionString))
            { // 데이터베이스 연결
                connection.Open();
                foreach (FoodDB product in products)
                {
                    int allergy = (int)product.Allergy;
                    string sql = $"INSERT INTO foodinfo (foodindex, foodname,foodmanu,foodallergy) VALUES ('{product.ReportNo}','{product.Name}','{product.Manufacturer}',{allergy})";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }
        private static List<FoodDB> GetApiData(string FoodName)
        {
            HttpClient client = new HttpClient();
            string url = "http://apis.data.go.kr/B553748/CertImgListService/getCertImgListService"; // URL
            url += "?ServiceKey=" + "bqnGvjKcGKv%2FCc6HQprcv%2B%2FBKy99RJR9ObJ4ANNy6rZXJswchzZxP0rA9el6g4N8GpTMOO%2Blg%2F5h3qWK%2Be%2BRBA%3D%3D"; // Service Key
            if (FoodName != "")
                url += $"&prdlstNm={FoodName}";
            url += "&returnType=xml";
            url += "&pageNo=1";
            url += "&numOfRows=100";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string results;
            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }
            return ParseXmlResponse(results);
        }

        private static List<FoodDB> ParseXmlResponse(string xmlResponse)
        {
            List<FoodDB> products = new List<FoodDB>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlResponse);

            XmlNodeList itemNodes = xmlDoc.SelectNodes("//item");

            foreach (XmlNode itemNode in itemNodes)//API의 xml을 객체로 변환
            {
                FoodDB product = new FoodDB();
                product.ReportNo = itemNode.SelectSingleNode("prdlstReportNo").InnerText;
                product.Name = itemNode.SelectSingleNode("prdlstNm").InnerText;
                product.Manufacturer = itemNode.SelectSingleNode("manufacture").InnerText;
                product.Allergy = FindAllergic(itemNode.SelectSingleNode("allergy").InnerText);
                products.Add(product);
            }

            return products;
        }
        private static Allergic FindAllergic(string input)//API에서 알레르기 칸
        {
            Allergic result = 0;
            if (input == "없음")
                return Allergic.None;
            if (input.Contains("아몬드") || input.Contains("땅콩") || input.Contains("호두"))
                result = result | Allergic.Almond;
            if (input.Contains("우유"))
                result = result | Allergic.Milk;
            if (input.Contains("닭고기"))
                result = result | Allergic.Beef;
            if (input.Contains("소고기"))
                result = result | Allergic.Beef;
            if (input.Contains("돼지고기"))
                result = result | Allergic.Beef;
            if (input.Contains("밀가루") || input.Contains("밀"))
                result = result | Allergic.Flour;
            if (input.Contains("대두"))
                result = result | Allergic.Soybean;
            if (input.Contains("달걀") || input.Contains("난류") || input.Contains("계란"))
                result = result | Allergic.Egg;
            if (input.Contains("게") || input.Contains("새우"))
                result = result | Allergic.Crab;
            if (input.Contains("문어") || input.Contains("조개") || input.Contains("오징어"))
                result = result | Allergic.Molluscs;
            return result;
        }
    }
    public class UserDB
    {
        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=softwareeng.kro.kr)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));;User Id=SoftEng;Password=1";
        public string nickName;
        public Allergic allergy;
        public Dictionary<string, string> Symptoms;

        public static UserDB getter(string nickName = "김봉주")
        {
            UserDB userDB = new UserDB();
            using (OracleConnection connection = new OracleConnection(connectionString))
            { // 데이터베이스 연결
                connection.Open();
                string sql;
                sql = $"SELECT UserInfo.NickName,userallergy FROM UserInfo WHERE UserInfo.NickName = '{nickName}'";
                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    { // 결과가 비어있으면 빈유저 반환
                        if (!reader.HasRows)
                            return new UserDB();
                        while (reader.Read())
                        {
                            userDB.nickName = reader.GetString(0);
                            userDB.allergy = (Allergic)reader.GetInt32(1);
                        }
                    }
                }
                connection.Close();
            }
            return userDB;

        }
        public static bool CheckNickName(string nickName)
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                { // 데이터베이스 연결
                    connection.Open();
                    string sql = $"SELECT UserInfo.NickName FROM UserInfo";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (nickName == reader.GetString(0))
                                    return false;
                            }
                        }
                    }
                    connection.Close();
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        public bool AddUserInfo(String name, Allergic allergy)
        {
            this.allergy = allergy;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                { // 데이터베이스 연결
                    connection.Open();
                    string sql = $"INSERT INTO userinfo (nickname,userallergy) VALUES ('{name}',{(int)allergy})";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                nickName = name;
                return false;
            }
            catch
            {
                return true;
            }
        }

        public bool AddSymptoms(string symptoms, string foodname) //증상기록
        {
            this.Symptoms[foodname] = symptoms;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                { // 데이터베이스 연결
                    connection.Open();
                    string sql = $"INSERT INTO usersymptoms (nickname,foodname,symptoms) VALUES ('{this.nickName}','{foodname}','{symptoms}')";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        public bool ModifySymptoms(string symptoms, string foodname) //증상수정
        {
            this.Symptoms[foodname] = symptoms;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                { // 데이터베이스 연결
                    connection.Open();
                    string sql = $"UPDATE usersymptoms SET symptoms = '{symptoms}' WHERE nickname = {this.nickName} AND foodname = {foodname}";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
        public bool DeleteSymptoms(string foodname)//증상기록 삭제
        {
            this.Symptoms.Remove(foodname);

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                { // 데이터베이스 연결
                    connection.Open();
                    string sql = $"DELETE FROM usersymptoms WHERE nickname = '{this.nickName}' AND foodname = '{foodname}'";
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

    }
}