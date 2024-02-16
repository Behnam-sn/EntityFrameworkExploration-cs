namespace Temporal
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<ParameterValue> ParameterValues { get; set; } = [];
    }
}