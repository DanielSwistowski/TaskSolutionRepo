using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Company
    {
        public Company()
        {
            SearchDetails = new HashSet<SearchDetail>();
        }

        [Key]
        public int ComanyId { get; set; }

        public string NIP { get; set; }

        public string REGON { get; set; }

        public string KRS { get; set; }

        public string CompanyName { get; set; }

        public virtual Address CompanyAddress { get; set; }

        public virtual ICollection<SearchDetail> SearchDetails { get; set; }
    }
}
