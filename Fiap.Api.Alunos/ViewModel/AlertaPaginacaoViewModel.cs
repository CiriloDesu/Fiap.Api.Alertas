using System.Reflection.Metadata;

namespace Fiap.Api.Alunos.ViewModel
{
    public class AlertaPaginacaoViewModel
    {
        public IEnumerable<AlertaViewModel> Alerta { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Alerta.Count() == PageSize;
        public string PreviousPageUrl => HasPreviousPage ? $"/Alerta?pagina={CurrentPage - 1}&tamanho={PageSize}" : "";
        public string NextPageUrl => HasNextPage ? $"/Alerta?pagina={CurrentPage + 1}&tamanho={PageSize}" : "";

    }
}
