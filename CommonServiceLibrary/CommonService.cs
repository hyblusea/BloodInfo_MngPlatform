using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PetaPoco;


namespace CommonServiceLibrary
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)] 
    public class CommonService : ICommonService
    { 
         Database  db;
         public List<T> GetDataForSQLServer<T>(string dbName, string sql, params object[] args)
         {
             db = new Database(dbName);
             return db.Fetch<T>(sql, args);
         }

        public string GetData(int value)
        {
            db = new Database("LIS");
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
