using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleGameSolution.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int Level { get; set; }

        public string Email { get; set; }
    }
}
