

namespace Hotel.Models
{
    public class TypeDeChambre
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Prix { get; set; }
        public ICollection<Chambre> Chambres { get; set; }

    }

}
