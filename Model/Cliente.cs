namespace WebApplication1.Model
{
    public class Cliente
    {
    

        public required string Nickname { get; set; }
        public required string Plano { get; set; } // "basico" ou "avancado"
        public string? Signo { get; set; }
    }
}