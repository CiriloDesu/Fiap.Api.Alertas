using Fiap.Api.Alunos.Models;

namespace Fiap.Api.Alunos.Data.Repository
{
    public interface IAlertaRepository
    {
        IEnumerable<AlertaModel> GetAllAlerts();

        IEnumerable<AlertaModel> GetAllReference(int ultimaRef, int tamanho);


        IEnumerable<AlertaModel> GetAllAlerts(int pagina, int tamanho);
        AlertaModel GetAlertaById(int alertaId);
        void Add(AlertaModel alerta);
        void Update(AlertaModel alerta);
        void Delete(AlertaModel alerta);
    }
}
