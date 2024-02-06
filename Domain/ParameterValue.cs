namespace Domain
{
    public class ParameterValue
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Code { get; set; }
        public int Value { get; set; }
    }
}