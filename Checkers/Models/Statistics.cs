using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    class Statistics
    {
        public int RedPlayers { get; set; }
        public int WhitePlayers { get; set; }

        public Statistics()
        {
            RedPlayers = 0;
            WhitePlayers = 0;
        }
    }
}
