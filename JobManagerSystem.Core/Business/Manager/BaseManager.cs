using SqlSugar;
using System;

namespace JobManagerSystem.Core.Business.Manager
{
    public class BaseManager
    {
        public  SqlSugarClient db
        {
            get
            {
                return new DbManager().db;
            }
        }
    }
}
