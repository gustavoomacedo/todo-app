using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool Feito { get; set; }
        public virtual List<Item> Item { get; set; }
    }
}