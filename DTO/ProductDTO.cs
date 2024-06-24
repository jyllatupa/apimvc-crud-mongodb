namespace API_CRUD_MongoDB.DTO
{
    public class ProductDTO //DTO: Para el acceso de datos desde el interfaz del usuario
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
