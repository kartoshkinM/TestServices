using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHelper
{
    public class MESSAGE
    {
        [Key] [Column(Order = 0)] public Guid ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NUM { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime SEND_TIME { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime RECEIPT_TIME { get; set; }

        [Key] [Column(Order = 4)] public string MESSAGE_TEXT { get; set; }

        public Guid? LAST_MESSAGE_ID { get; set; }
    }
}