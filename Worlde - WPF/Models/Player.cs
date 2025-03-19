using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worlde___WPF.Models
{
    internal class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Win { get; set; }
        public int Lost { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | Score: {Score} | Win/Lost Ratio {Win}:{Lost}";
        }

    }
}
