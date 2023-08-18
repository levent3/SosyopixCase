namespace WebWuı.Models
{
    public class PhotoDto
    {


        public int Id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
