using System.Runtime.InteropServices;

namespace Fiap.Api.Alunos.Models
{
    public class AlertaModel
    {
        public int Id { get ; set; }
        public string Nome { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }

        public DateTime Data { get; set; }

    }
}
