using Fiap.Api.Alunos.Data.Context;
using Fiap.Api.Alunos.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Api.Alunos.Data.Repository
{
    public class AlertaRepository : IAlertaRepository
    {

        private readonly DatabaseContext _context;

        public AlertaRepository(DatabaseContext context)
        {
            _context = context;
        }


        public void Add(AlertaModel alerta)
        {
            _context.Alertas.Add(alerta);
            _context.SaveChanges();
        }

        public void Delete(AlertaModel alerta)
        {
            _context.Alertas.Remove(alerta);
            _context.SaveChanges();
        }

        public AlertaModel GetAlertaById(int alertaId) => _context.Alertas.Find(alertaId);

        public IEnumerable<AlertaModel> GetAllAlerts()
        {
            return _context.Alertas.ToList();
        }

        public void Update(AlertaModel alerta)
        {
            _context.Update(alerta);
            _context.SaveChanges();
        }


        IEnumerable<AlertaModel> IAlertaRepository.GetAllAlerts(int pagina, int tamanho)
        {
            return _context.Alertas
                .Skip((pagina - 1) * tamanho)
                .Take(tamanho)
                .ToList();
        }

        IEnumerable<AlertaModel> GetAllReference(int ultimaRef, int tamanho)
        {
            return _context.Alertas
                .Where(a => a.Id > ultimaRef)
                .Take(tamanho)
                .ToList();
        }

        IEnumerable<AlertaModel> IAlertaRepository.GetAllReference(int ultimaRef, int tamanho)
        {
            throw new NotImplementedException();
        }
    }
}
