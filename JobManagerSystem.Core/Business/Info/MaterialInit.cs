using JobManagerSystem.Core.Business.Info;
using JobManagerSystem.Core.Business.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagerSystem.Core
{
    public class MaterialInit
    {

        /// <summary>
        /// 加载物料内外基础数据
        /// </summary>
        public static List<MaterialInterandext> MaterialInit_List { get; set; } = new List<MaterialInterandext>();

        public static void Init()
        {
            try
            {
                MaterialInit_List = MaterialInitManager.GeAllMaterialInterandextList();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
