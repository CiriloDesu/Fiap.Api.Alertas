using Fiap.Api.Alunos.Data.Repository;
using Fiap.Api.Alunos.Models;

namespace Fiap.Api.Alunos.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly IAlertaRepository _repository;

        public AlertaService(IAlertaRepository repository)
        {
            _repository = repository;
        }


        public void AtualizarAlerta(AlertaModel alerta) => _repository.Update(alerta);

        public void CriarAlerta(AlertaModel alerta) => _repository.Add(alerta);


        public void DeletarAlerta(int id)
        {
            var alerta = _repository.GetAlertaById(id);
            if (alerta == null) {
                _repository.Delete(alerta);
            }
        }


        public IEnumerable<AlertaModel> ListarAlertas()
        {
            return _repository.GetAllAlerts();
        }

        public IEnumerable<AlertaModel> ListarAlertas(int pagina = 0, int tamanho = 10)
        {
            return _repository.GetAllAlerts(pagina, tamanho);
        }

        public IEnumerable<AlertaModel> ListarAlertasUltimaReferencia(int ultimoId = 0, int tamanho = 10)
        {
            return _repository.GetAllReference(ultimoId, tamanho);
        }

        public AlertaModel ObterAlertaPorId(int id) => _repository.GetAlertaById(id);
        
    }
}
