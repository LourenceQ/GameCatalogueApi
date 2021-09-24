using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public double Price { get; set; }
    }
}
