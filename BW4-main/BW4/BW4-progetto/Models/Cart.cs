namespace BW4_progetto.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
