using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSC.CM.XaSh.MobileModelData
{
    [Table("LastUpdated")]
    public class LastUpdated
    {
        public DateTime LastUpdatedUTC { get; set; }
        public string TableName { get; set; }
    }
}