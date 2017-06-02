using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Idtodo { get; set; }
        public string Descricao { get; set; }
        public bool Feito { get; set; }
    }
}
