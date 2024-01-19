using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LarryDotNetCore.ATMWebApp.Models
{
    [Table("Tbl_Atm")]
    public class AtmDataModel
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("CardNumber")]
        public string CardNumber { get; set; }

        [Column("Pin")]
        public int Pin { get; set; }

        [Column("Balance")]
        public double Balance { get; set; }
    }

    public class AtmMessageModel
    {
        public AtmMessageModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
