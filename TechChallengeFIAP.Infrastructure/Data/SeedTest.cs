using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Infrastracture.Data
{
    public static class SeedTest
    {
        public static void Add(FiapDbContext fiapContext, IDDDRegionService ddd_service)
        {
            var testeContato1 = new Contato
            {
                Id = 123,
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Telefone = new Telefone { DDD = "11", Numero = "982598878"}
            };
            var dddinfo1 = ddd_service.GetInfo("11").Result;
            testeContato1.Telefone.UF = dddinfo1.state;
            fiapContext.Add(testeContato1);

            var testeContato2 = new Contato
            {
                Id = 456,
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Telefone = new Telefone { DDD = "21", Numero = "982599979", UF = "RJ" }
            };
            var dddinfo2 = ddd_service.GetInfo("21").Result;
            testeContato2.Telefone.UF = dddinfo2.state;
            fiapContext.Add(testeContato2);

            fiapContext.SaveChanges();
        }

    }


}
