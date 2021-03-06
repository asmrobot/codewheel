﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using CodeWheel.Infrastructure;
using CodeWheel.Utils;
using CodeWheel.Infrastructure.DB;
using System.IO;

namespace CodeWheel.ViewModels
{
    public class MainWindowViewModel:NotificationObject
    {

        public MainWindowViewModel()
        {
            //保存路径
            this.saveDir = ApplicationGlobal.Instance.States.GetValue("SaveDir");
            this.dbTypeSelectIndex = TypeConverter.StringToInt(ApplicationGlobal.Instance.States.GetValue("DBTypeSelectIndex"),0);
            this.connectionString = ApplicationGlobal.Instance.States.GetValue("ConnectionString");
            
            //初始化模板列表
            foreach (var template in ApplicationGlobal.Instance.TemplateProvider.Templates)
            {
                this.templates.Add(template);
            }


        }

        private ObservableCollection<TemplateBase> templates= new ObservableCollection<TemplateBase>();

        /// <summary>
        /// 模板列表
        /// </summary>
        public ObservableCollection<TemplateBase> Templates
        {
            get
            {
                return templates;
            }
            set
            {
                if (templates != value)
                {
                    templates = value;
                    this.RaisePropertyChanged("Templates");
                }
            }
        }


        private int templateSelectIndex =-1;
        /// <summary>
        /// 选择模板索引
        /// </summary>
        public int TemplateSelectIndex
        {
            get
            {
                return templateSelectIndex ;
            }

            set
            {
                if (templateSelectIndex  != value)
                {
                    templateSelectIndex  = value;
                    this.RaisePropertyChanged("TemplateSelectIndex");
                }
            }
        }

        private ObservableCollection<TableSelectModel> tables = new ObservableCollection<TableSelectModel>();
        /// <summary>
        /// 表列表
        /// </summary>
        public ObservableCollection<TableSelectModel> Tables
        {
            get
            {
                return tables;
            }

            set
            {
                if (tables != value)
                {
                    tables = value;
                    this.RaisePropertyChanged("Tables");
                }
            }
        }


        /// <summary>
        /// 获取所有选中的表
        /// </summary>
        /// <returns></returns>
        public List<TableMeta> GetSelectedTables()
        {
            List<TableMeta> tables = new List<TableMeta>();
            foreach (var item in Tables)
            {
                if (item.IsSelected)
                {
                    tables.Add(item.Meta);
                }
            }
            return tables;
        }




        private string connectionString = "Server=127.0.0.1;charset=utf8;Database=test;Port=3306;Uid=root;Pwd=root;default command timeout=10;";
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                if (connectionString != value)
                {
                    ApplicationGlobal.Instance.States.SetValue("ConnectionString", value);
                    connectionString = value;
                    this.RaisePropertyChanged("ConnectionString");
                }
            }
        }

        private string saveDir=string.Empty;
        /// <summary>
        /// 代码保存路径
        /// </summary>
        public string SaveDir
        {
            get
            {
                return saveDir;
            }

            set
            {
                if (saveDir != value)
                {
                    ApplicationGlobal.Instance.States.SetValue("SaveDir", value);
                    saveDir = value;
                    this.RaisePropertyChanged("SaveDir");
                }
            }
        }



        private int dbTypeSelectIndex = 0;
        /// <summary>
        /// 数据库类型选择索引
        /// </summary>
        public int DBTypeSelectIndex
        {
            get
            {
                return dbTypeSelectIndex;
            }
            set
            {
                if (this.dbTypeSelectIndex != value)
                {
                    ApplicationGlobal.Instance.States.SetValue("DBTypeSelectIndex", value.ToString());
                    this.dbTypeSelectIndex = value;
                    this.RaisePropertyChanged("DBTypeSelectIndex");
                }
            }
        }

        /// <summary>
        /// 是否能连接
        /// </summary>
        /// <returns></returns>
        public bool TestConnectionion()
        {
            System.Data.Common.DbConnection connection = null;
            try
            {
                switch (this.DBTypeSelectIndex)
                {
                    case 0:
                        connection = new MySql.Data.MySqlClient.MySqlConnection(this.ConnectionString);
                        break;
                    case 1:
                        connection = new System.Data.SqlClient.SqlConnection(this.ConnectionString);
                        break;
                    case 2:
                        connection = new System.Data.SQLite.SQLiteConnection(this.ConnectionString);
                        break;
                }

                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

        }


    }

    /// <summary>
    /// 表选择模型
    /// </summary>
    public class TableSelectModel : NotificationObject
    {
        private bool isSelected = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    this.RaisePropertyChanged("IsSelected");
                }
            }
        }


        private string tableName = string.Empty;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }

            set
            {
                if (tableName != value)
                {
                    tableName = value;
                    this.RaisePropertyChanged("TableName");
                }
            }
        }

        /// <summary>
        /// 元数据
        /// </summary>
        public TableMeta Meta
        {
            get;
            set;
        }
    }
}
