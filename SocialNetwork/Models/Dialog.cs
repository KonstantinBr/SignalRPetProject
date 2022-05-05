using System.Collections.Generic;

namespace SocialNetwork.Models
{    
    public partial class Dialog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dialog()
        {
            this.MessageList = new HashSet<MessageList>();
            this.User_Dialog = new HashSet<User_Dialog>();
        }
    
        public int Id { get; set; }
        public string DialogName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MessageList> MessageList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Dialog> User_Dialog { get; set; }
    }
}
