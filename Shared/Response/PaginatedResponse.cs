namespace Shared.Dtos.Responses
{
    public class PaginatedResponse<T>
    {
        public int Page { get; set; } // Página atual
        public int Size { get; set; } // Quantidade por página
        public int Total { get; set; } // Total de registros
        public List<T> Content { get; set; } // Lista de registros

        public PaginatedResponse(int page, int size, int total, List<T> content)
        {
            Page = page;
            Size = size;
            Total = total;
            Content = content;
        }
    }

}