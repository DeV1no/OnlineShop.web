using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.web.Entities.Wallet
{
    public class WalletType
    {
        public WalletType()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required] public string TypeTitle { get; set; }

        //Reletion
        public virtual List<Wallet> Wallet { get; set; }
    }
}