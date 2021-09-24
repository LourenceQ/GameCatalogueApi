using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException()
            : base("Este jogo não está cadastrado")
        { }
    }
}
