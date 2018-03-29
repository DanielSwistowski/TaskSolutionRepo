namespace DataAccessLayer.Models
{
    public class SearchDetail
    {
        public int SearchDetailId { get; set; }
        public string Number { get; set; }
        public string HeaderValues { get; set; }
        public NumberType NumberType { get; set; }

        public int ComanyId { get; set; }
        public virtual Company Company { get; set; }
    }

    public enum NumberType
    {
        NIP,
        REGON,
        KRS,
        Unrecognized
    }
}