namespace web_gallery.Models
{
    public interface Model<TId>
    {
        public TId Id { get; set; }
    }
}
