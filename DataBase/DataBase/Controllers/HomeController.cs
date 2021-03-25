using Aspose.Words;
using Aspose.Words.Tables;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DataBase.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //string fileName = @"F:\文档归整\数据库文档\SIS数据表_v1.3.1.2.doc";
            //var list = ReadWord(fileName);
            ViewBag.Text = BathDataBaseSql();
            return View();
        }



        public string BathDataBaseSql()
        {
            string fileName = @"D:数据库表.doc";
            var wordList = ReadWord(fileName);
            string hostname = "192.168.0.1";
            string port = "3306";
            string username = "dev";
            string pwd = "dev";
            string database = "dev_db";
            var dataList = ReadDataBase(hostname, port, username, pwd, database);
            string fileUrl = string.Format("D:\\{0}.txt", "word文档表与数据库表差距" + DateTime.Now.ToString("yyyyMMdd"));
            string differenceInfoStr = string.Empty;
            foreach (var item in dataList)
            {
                var wordDto = wordList.Where(p => p.TableName == item.Table_Name).FirstOrDefault();
                if (wordDto != null)
                {
                    var wordDataInfo = wordDto.TableInfo.Where(p => p.List.Contains(item.Column_Name)).FirstOrDefault();
                    if (wordDataInfo != null)
                    {
                        //校验默认值是否相同
                        if (!wordDataInfo.List.Contains(item.Column_Default))
                        {
                            differenceInfoStr += $"{item.Table_Name} 表 {item.Column_Name} 字段:默认值不相同; \r\n";
                        }
                        //校验可空是否相同
                        if (!wordDataInfo.List.Contains(item.Is_Nullable))
                        {
                            differenceInfoStr += $"{item.Table_Name} 表 {item.Column_Name} 字段:可空不相同; \r\n";
                        }
                        //校验数据类型是否相同
                        if (!wordDataInfo.List.Contains(item.Data_Type))
                        {
                            differenceInfoStr += $"{item.Table_Name} 表 {item.Column_Name} 字段:数据类型不相同; \r\n";
                        }
                        //校验备注是否相同
                        if (!wordDataInfo.List.Contains(item.Column_Comment))
                        {
                            differenceInfoStr += $"{item.Table_Name} 表 {item.Column_Name} 字段:备注不相同; \r\n";
                        }
                        item.Column_Comment = item.Column_Comment + "^" + wordDataInfo.List[5];

                    }
                    else
                    {
                        differenceInfoStr += $"{item.Table_Name} 表 {item.Column_Name} 字段:数据库存在,word文档不存在; \r\n";
                    }
                }
                else
                {
                    differenceInfoStr += $"{item.Table_Name} 表:数据库存在,word文档不存在; \r\n";
                }
            }
            var batchGenerationSql = BatchGenerationSql(dataList);
            StreamWriter sw = new StreamWriter(fileUrl);
            sw.Write(differenceInfoStr);
            sw.Close();
            return batchGenerationSql;
        }


        /// <summary>
        /// 根据指定word文件路径读取word文件信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<TableModel> ReadWord(string path)
        {
            Document doc = new Document(path);
            var list = new List<TableModel>();
            //获取word文档中body部分
            var bodys = doc.GetChildNodes(NodeType.Body, true)[0] as Aspose.Words.CompositeNode;
            //获取word文档中表格部分
            NodeCollection tables = doc.GetChildNodes(NodeType.Table, true);
            //遍历表格
            foreach (Table item in tables)
            {
                int tableIndex = item.ParentNode.ChildNodes.IndexOf(item);
                //获取body相对应坐标下的文本
                var bodyText = bodys.ChildNodes[tableIndex - 1].Range.Text;
                var dataBase = new TableModel();
                var newBodyText = bodyText.Substring(0, bodyText.IndexOf(':'));
                var tableName = newBodyText.Substring(newBodyText.IndexOf(' ')).Trim();
                dataBase.TableName = tableName;
                //遍历每行数据
                foreach (Row row in item.Rows)
                {
                    int rowIndex = item.Rows.IndexOf(row);
                    if (rowIndex != 0)
                    {
                        var info = new TableInfo();
                        //遍历每列数据
                        foreach (Cell cell in row.Cells)
                        {
                            string cellText = cell.ToString(SaveFormat.Text).Replace("\r\n", "").Trim();
                            info.List.Add(cellText);
                        }
                        dataBase.TableInfo.Add(info);
                    }
                }
                list.Add(dataBase);
            }
            return list;
        }

        /// <summary>
        /// 读取数据库信息
        /// </summary>
        /// <param name="hostname">域名</param>
        /// <param name="port">端口号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="database">数据库</param>
        private List<DataBaseTableInfoModel> ReadDataBase(string hostname, string port, string username, string pwd, string database)
        {
            string myth = $@"server={hostname};port={port};User Id={username};Pwd={pwd};Persist Security Info=True;database={database}";
            string mycon = System.Configuration.ConfigurationManager.ConnectionStrings["myth"].ToString();
            MySqlConnection mysqlcon = new MySqlConnection(mycon);
            mysqlcon.Open();
            //指定数据库，查询数据库内所有表信息
            string queryDataBaseTableInfoSql = @"SELECT 
                                                    column_name ,
                                                    COLUMN_TYPE data_type ,
                                                    character_maximum_length ,
                                                    numeric_precision ,
                                                    case is_nullable when 'NO' THEN '否' ELSE '是' END is_nullable ,
                                                    column_default,
                                                    column_comment,
                                                    TABLE_NAME,EXTRA
                                                FROM
                                                    Information_schema.columns
                                                WHERE
                                                    table_schema = 'myth_dev3'";

            ////指定数据库，查询数据库内所有表名称
            //string queryDataBaseTableSql = @"SELECT 
            //                                    table_name
            //                                FROM
            //                                    information_schema.tables
            //                                WHERE
            //                                    table_schema = 'myth_dev3'";
            //查询数据库表
            //查询数据库内所有表信息
            var list = new List<DataBaseTableInfoModel>();
            var dataList = MySqlDataAdapter(queryDataBaseTableInfoSql, mysqlcon);
            foreach (DataRow dr in dataList.Tables[0].Rows)
            {
                var dbt = new DataBaseTableInfoModel()
                {
                    Column_Comment = dr["column_comment"].ToString(),
                    Column_Default = dr["column_default"].ToString(),
                    Column_Name = dr["column_name"].ToString(),
                    Is_Nullable = dr["is_nullable"].ToString(),
                    Table_Name = dr["TABLE_NAME"].ToString(),
                    Character_Maximum_Length = dr["character_maximum_length"].ToString(),
                    Data_Type = dr["data_type"].ToString(),
                    Numeric_Precision = dr["numeric_precision"].ToString(),
                    EXTRA = dr["EXTRA"].ToString()
                };
                list.Add(dbt);
            }
            return list;
        }

        public static DataSet MySqlDataAdapter(string sql, MySqlConnection mysqlcon)
        {
            MySqlCommand cmd = new MySqlCommand(sql, mysqlcon);
            cmd.CommandTimeout = 10000;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet myds = new DataSet();
            da.Fill(myds);
            return myds;
        }

        public static bool GetSqlCommand(string sql, MySqlConnection mysql)
        {
            var tran = mysql.BeginTransaction();
            try
            {
                MySqlCommand mysqlCommand = new MySqlCommand(sql, mysql);
                var count = mysqlCommand.ExecuteNonQuery();
                tran.Commit();
                return count > 0;
            }
            catch (Exception ex)
            {
                string fileUrl = string.Format("D:\\{0}.txt", "word文档表与数据库表差距" + DateTime.Now.ToString("yyyyMMdd"));
                StreamWriter sw = new StreamWriter(fileUrl);
                sw.Write(ex.Message);
                sw.Close();
                tran.Rollback();
                return false;
            }
        }

        public static string BatchGenerationSql(List<DataBaseTableInfoModel> siList)
        {
            var updateSql = "";
            foreach (var item in siList)
            {
                string is_Nullable = string.Empty;
                string defaultText = string.Empty;
                if (item.Is_Nullable == "否")
                {
                    is_Nullable = "NOT NULL";
                }
                else
                {
                    is_Nullable = "NULL";
                }

                if (!string.IsNullOrEmpty(item.EXTRA))
                {
                    defaultText = item.EXTRA;
                }
                else
                {
                    if (string.IsNullOrEmpty(item.Column_Default))
                    {
                        defaultText = $" DEFAULT ''";
                    }
                    else
                    {
                        if (item.Column_Default == "0001-01-01 00:00:00")
                        {
                            defaultText = $" DEFAULT '{item.Column_Default}' ";
                        }
                        else if (item.Column_Default == "b'0'")
                        {
                            defaultText = $" DEFAULT 0 ";
                        }
                        else if (item.Column_Default == "b'1'")
                        {
                            defaultText = $" DEFAULT 1 ";
                        }
                        else
                        {
                            defaultText = $" DEFAULT {item.Column_Default} ";
                        }

                    }
                }

                updateSql += $@"ALTER TABLE `{item.Table_Name}` CHANGE COLUMN `{item.Column_Name}` `{item.Column_Name}` {item.Data_Type} {is_Nullable} {defaultText}  COMMENT '{item.Column_Comment}' ;";
            }
            return updateSql;

        }

        /// <summary>
        /// 表格模型
        /// </summary>
        public class TableModel
        {
            /// <summary>
            /// 表格名称
            /// </summary>
            public string TableName { get; set; }
            /// <summary>
            /// 表格信息集合，默认为空表格
            /// </summary>
            public List<TableInfo> TableInfo { get; set; } = new List<TableInfo>();

        }
        /// <summary>
        /// 表格信息
        /// </summary>
        public class TableInfo
        {
            /// <summary>
            /// 表格信息详情，默认为空字符串数组
            /// </summary>
            public List<string> List { get; set; } = new List<string>();
        }

        /// <summary>
        /// 数据库表模型
        /// </summary>
        public class DatabaseTableModel
        {
            public string Table_Name { get; set; }
        }
        /// <summary>
        /// 数据库表信息模型
        /// </summary>
        public class DataBaseTableInfoModel
        {
            /// <summary>
            /// 列名
            /// </summary>
            public string Column_Name { get; set; }
            /// <summary>
            /// 数据类型
            /// </summary>
            public string Data_Type { get; set; }
            /// <summary>
            /// 字符长度
            /// </summary>
            public string Character_Maximum_Length { get; set; }
            /// <summary>
            /// 数字长度
            /// </summary>
            public string Numeric_Precision { get; set; }
            /// <summary>
            /// 是否允许非空
            /// </summary>
            public string Is_Nullable { get; set; }
            /// <summary>
            /// 默认值
            /// </summary>
            public string Column_Default { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string Column_Comment { get; set; }
            /// <summary>
            /// 所属数据库表
            /// </summary>
            public string Table_Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string EXTRA { get; set; }
        }
    }
}