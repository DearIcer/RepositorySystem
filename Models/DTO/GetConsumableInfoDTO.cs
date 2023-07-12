using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class GetConsumableInfoDTO
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }

        public string ConsumableName { get; set; }
        public string Specification { get; set; }
        public int Num{ get; set; }
        public string Unit { get; set; }
        public decimal Money { get; set; }
        public int WarningNum { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DeleteTime { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
