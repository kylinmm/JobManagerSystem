using JobManagerSystem.Core.Business.Info;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace JobManagerSystem.Core.Business.Manager
{
    public class MaterialInitManager : BaseManager
    {
        /// <summary>
        /// 获取内外物料集合
        /// </summary>
        /// <returns></returns>
        public static List<MaterialInterandext> GeAllMaterialInterandextList()
        {
            var db = SqlDbManager.GetInstance();

            List<MaterialInterandext> list = db.Queryable<MaterialInterandext>().ToList();
            return list;
        }
    }
}