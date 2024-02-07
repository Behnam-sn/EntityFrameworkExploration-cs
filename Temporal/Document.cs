namespace Domain
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<ParameterValue> ParameterValues { get; set; }
    }
}