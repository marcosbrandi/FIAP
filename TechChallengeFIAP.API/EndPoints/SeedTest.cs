using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.API.EndPoints
{
    public static class SeedTest
    {
        public static void Add(IContatoRepository repository)
        {
            var ret = repository.GetAllAsync(null).Result;
            if (ret.Count() > 0) return;

            var testeContato1 = new Contato
            {
                Id = 123,
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Telefone = new Telefone { DDD = "11", Numero = "982598878" }
            };
            repository.AddAsync(testeContato1);

            var testeContato2 = new Contato
            {
                Id = 456,
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Telefone = new Telefone { DDD = "21", Numero = "982599979" }
            };
            repository.AddAsync(testeContato2);
        }

    }


}
