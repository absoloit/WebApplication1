namespace WebApplication1.Model
{
    public class Values
    {
        public int Id { get; set; }
        public string? PrimeiroNome { get; set; }
        public string? SobreNome { get;set; }
        public int Idade { get; set; }
        public string? OqueEmprestei { get; set; }


        private static List<Values> ListaValues = [
            new Values(1,"Vinicius","Rosalen",18,"PS5"),
            new Values(2,"joao","felipe",22,"ps4"),
            new Values(3,"vitor","dc",21,"ps2")
            ];



        public Values(int id, string primeiroNome,string sobreNome, int idade, string oqueEmprestei)
        {
            Id = id;
            PrimeiroNome = primeiroNome;
            SobreNome = sobreNome;
            Idade = idade;
            OqueEmprestei = oqueEmprestei;
        }

      
        public Values()
        {

        }



    }
}
