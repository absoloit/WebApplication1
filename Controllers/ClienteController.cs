using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private static List<Cliente> clientes = new();

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Cliente cliente)
        {
            clientes.Add(cliente);
            return Ok($"Cliente {cliente.Nickname} registrado com plano {cliente.Plano}.");
        }

        [HttpGet("mensagem/{nickname}")]
        public IActionResult ObterMensagem(string nickname)
        {
            var cliente = clientes.Find(c => c.Nickname == nickname);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            var mensagensBicho = new[]
            {
                $"Bem-vindo, {nickname}! Eleve seu cosmo!",
                $"Bem-vindo, {nickname}! Eleve seu cosmo!",
                $"Bem-vindo, {nickname}! Eleve seu cosmo!"
            };

            var mensagensSorte = new[]
            {
                "Hoje é um bom dia para jogar no bicho. Jogue com seu nickname e tente sua Sorte!",
                "Confie na sua intuição hoje! Jogue com seu nickname e tente sua Sorte!",
                "Evite decisões impulsivas, mas aproveite as boas oportunidades. Jogue com seu nickname e tente sua Sorte!"
            };

            var random = new Random();
            var mensagemBicho = mensagensBicho[random.Next(mensagensBicho.Length)];
            var mensagemSorte = mensagensSorte[random.Next(mensagensSorte.Length)];
            var saudacaoSigno = $"Saudações, {cliente.Signo}!";

            if (cliente.Plano == "cavaleiro de bronze")
            {
                return Ok(new
                {
                    Signo = saudacaoSigno,
                    Mensagem = mensagemBicho
                });
            }
            else if (cliente.Plano == "cavaleiro de prata")
            {
                return Ok(new
                {
                    Signo = saudacaoSigno,
                    Mensagem = mensagemBicho,
                    Sorte = mensagemSorte
                });
            }
            else if (cliente.Plano == "cavaleiro de ouro")
            {
                string horoscopo = ObterHoroscopoPorSigno(cliente.Signo);

                return Ok(new
                {
                    Signo = saudacaoSigno,
                    Mensagem = mensagemBicho,
                    Sorte = mensagemSorte,
                    HoroscopoDoDia = horoscopo
                });
            }

            return BadRequest("Plano inválido.");
        }

        private string ObterHoroscopoPorSigno(string signo)
        {
            signo = signo.ToLower();

            return signo switch
            {
                "aries" => "Coragem e liderança te guiam hoje.",
                "touro" => "Valorize o conforto, mas esteja aberto a mudanças. GRANDE CHIFRE!",
                "gemeos" => "A comunicação será sua melhor aliada.",
                "cancer" => "A sensibilidade trará conexões valiosas.",
                "leao" => "Seu brilho natural atrairá boas energias.",
                "virgem" => "Organize seus pensamentos para tomar boas decisões.",
                "libra" => "Busque equilíbrio nas suas relações.",
                "escorpiao" => "Intensidade emocional pode ser sua força hoje.",
                "sagitario" => "O desejo de liberdade te levará a boas descobertas.",
                "capricornio" => "Seu foco e disciplina trarão frutos.",
                "aquario" => "Inovação e originalidade te destacam.",
                "peixes" => "Sonhe alto, mas mantenha os pés no chão.",
                _ => "Signo não reconhecido."
            };
        }

        [HttpDelete("remover/{nickname}")]
        public IActionResult RemoverCliente(string nickname)
        {
            var cliente = clientes.Find(c => c.Nickname == nickname);
            if (cliente == null)
            {
                return NotFound($"Cliente com nickname '{nickname}' não foi encontrado.");
            }

            clientes.Remove(cliente);
            return Ok($"Cliente '{nickname}' removido com sucesso.");
        }

        [HttpGet("testar-sorte/{nickname}")]
        public IActionResult TestarSuaSorte(string nickname)
        {
            var cliente = clientes.Find(c => c.Nickname == nickname);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            if (cliente.Plano != "cavaleiro de prata" && cliente.Plano != "cavaleiro de ouro")
            {
                return BadRequest("Este recurso está disponível apenas para planos a partir de 'cavaleiro de prata'.");
            }

            var numerosDoBicho = new[]
            {
                (1, "Avestruz"), (2, "Águia"), (3, "Burro"), (4, "Borboleta"),
                (5, "Cachorro"), (6, "Cabra"), (7, "Carneiro"), (8, "Camelo"),
                (9, "Cobra"), (10, "Coelho"), (11, "Cavalo"), (12, "Elefante"),
                (13, "Galo"), (14, "Gato"), (15, "Jacaré"), (16, "Leão"),
                (17, "Macaco"), (18, "Porco"), (19, "Pavão"), (20, "Peru"),
                (21, "Touro"), (22, "Tigre"), (23, "Urso"), (24, "Veado")
            };

            var random = new Random();
            var numeroUsuario = numerosDoBicho[random.Next(numerosDoBicho.Length)];
            var numeroDesafio = numerosDoBicho[random.Next(numerosDoBicho.Length)];

            var ganhou = numeroUsuario.Item1 == numeroDesafio.Item1;

            return Ok(new
            {
                Cliente = nickname,
                NumeroDoBichoUsuario = $"{numeroUsuario.Item1} ({numeroUsuario.Item2})",
                NumeroDoBichoDesafio = $"{numeroDesafio.Item1} ({numeroDesafio.Item2})",
                Resultado = ganhou
                    ? "Parabéns! Você ganhou um cupom de desconto especial!"
                    : "Não foi dessa vez. Continue tentando, cavaleiro!"
            });
        }
    }
}
