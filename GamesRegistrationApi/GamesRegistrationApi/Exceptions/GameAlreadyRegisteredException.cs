using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRegistrationApi.Exceptions
{
    public class GameAlreadyRegisteredException : Exception
    {
        public GameAlreadyRegisteredException()
            : base("Este já jogo está cadastrado")
        { }
    }
}
