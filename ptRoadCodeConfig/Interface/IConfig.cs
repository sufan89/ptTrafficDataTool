using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ptRoadCodeConfig
{
    interface IConfig
    {
        /// <summary>
        /// 初始化配置信息
        /// </summary>
        void InitializeConfig();
        /// <summary>
        /// 获取图层配置信息
        /// </summary>
        void GetLayerConfig();
        /// <summary>
        /// 获取编码配置信息
        /// </summary>
        void GetCodeRuleConfig();
        /// <summary>
        /// 根据表名获取表数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        DataTable GetTableByName(string TableName, string StrWhere="");
    }
}
