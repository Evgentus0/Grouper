namespace Grouper.Api.Web.Models
{
    public class FormModel
    {
        public int Id { get; set; }
        public string Content {get;set;}
        public UserModel User { get; set; }
    }
}
