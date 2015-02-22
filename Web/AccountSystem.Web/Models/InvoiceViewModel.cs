namespace AccountSystem.Web.Models
{
    using System;

    public class InvoiceViewModel
    {
        public string CompanyName { get; set; }

        public string FullName { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public DateTime CreatedOn { get; set; }

        public string InvoiceNumber { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string BillToName { get; set; }

        public string BillToStreetAddress { get; set; }

        public string BillToCity { get; set; }

        public string BillToCountry { get; set; }

        public string BillToPostcode { get; set; }

        public string BillToPhone { get; set; }

        public int RevenueId { get; set; }

        public string UserId { get; set; }
    }
}