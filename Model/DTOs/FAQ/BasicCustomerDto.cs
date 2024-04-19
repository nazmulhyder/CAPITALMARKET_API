using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FAQ
{
    public class BasicCustomerDto
    {
        [Key]
        public int indexId { get; set; }
        public string? memberName { get; set; }
        public string? memberCode { get; set; }
        public string? memberType { get; set; }
        public string? mobileNo { get; set; }
        public string? emailAddress { get; set; }
        public string? uniqueIDType { get; set; }
        public string? uniqueIDNo { get; set; }
        public string? defaultAddress { get; set; }
        public string? defaultBankAccount { get; set; }
        public string? approvalStatus { get; set; }
        public string? makeDate { get; set; }
        public string? maker { get; set; }
        public string? approvalDate { get; set; }
        public string? approver { get; set; }
        //age
        public int age { get; set; }
        public int TotalRowCount { get; set; }
        public string? accountNumber { get; set; }
        public string? productName { get; set; }
        public int ? productID { get; set; }
        public int? contractID { get; set; }
        public string?  accountType { get; set; }
    }

    public class GetBasicCustomersDTO
    {
        [Key]
        public string? resultStatus { get; set; }
        public string? resultMsg { get; set; }
        public List<BasicCustomerDto>? customers { get; set; }
    }
}
