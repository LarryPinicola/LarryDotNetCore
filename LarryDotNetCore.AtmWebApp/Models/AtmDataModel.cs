using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LarryDotNetCore.AtmWebApp.Models
{
    [Table("tbl_atm")]
    public class AtmDataModel
    {
        [Key]
        [Column("card_Id")]
        public int CardId { get; set; }

        [Column("cardNum")]
        public double CardNum { get; set; }

        [Column("card_pin")]
        public int CardPin { get; set; }

        [Column("card_firstName")]
        public string FirstName { get; set; }

        [Column("card_lastName")]
        public string LastName { get; set; }

        [Column("card_balance")]
        public double Balance { get; set; }
    }
}
