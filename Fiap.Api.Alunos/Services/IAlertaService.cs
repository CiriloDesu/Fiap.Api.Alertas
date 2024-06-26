using Fiap.Api.Alunos.Models;

namespace Fiap.Api.Alunos.Services
{
    public interface IAlertaService
    {
        IEnumerable<AlertaModel> ListarAlertas();
        IEnumerable<AlertaModel> ListarAlertas(int pagina = 0, int tamanho = 10);
        IEnumerable<AlertaModel> ListarAlertasUltimaReferencia(int ultimoId = 0, int tamanho = 10);
        AlertaModel ObterAlertaPorId(int id);
        void CriarAlerta(AlertaModel alerta);
        void AtualizarAlerta(AlertaModel alerta);
        void DeletarAlerta(int id);
    }
}
