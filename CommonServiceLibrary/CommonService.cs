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
        public List<Lis_List> GetDataLisList(string sql, params object[] args)
        {
            Database db = new Database("LISDB");
            return db.Fetch<Lis_List>(sql, args);
        }

        public List<Ris_List> GetDataRisList(string sql, params object[] args)
        {
            Database db = new Database("RISDB");
            return db.Fetch<Ris_List>(sql, args);
        }

        public List<Lis_Result> GetDataLisResult(string sql, params object[] args)
        {
            Database db = new Database("LISDB");
            return db.Fetch<Lis_Result>(sql, args);
        }

        public string GetData(int value)
        {
            Database db = new Database("LISDB");
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
