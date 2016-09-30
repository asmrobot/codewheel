﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWheel.Model
{
    /// <summary>
    /// 模板基本信息
    /// </summary>
    public class TemplateInfo
    {
        public string TemplateFile{ get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 传递到模板的实体类型
        /// </summary>
        public Type ModelType { get; set; }


        /// <summary>
        /// 变量列表
        /// </summary>
        public List<VarInfo> Vars { get; set; }



    }
}
