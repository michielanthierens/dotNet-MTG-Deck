using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Models
{
    public class Type
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public DateOnly? Created_at { get; set; }
        public DateOnly? Updated_at { get; set; }
    }
}
