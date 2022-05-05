using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Models
{
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.User_Dialog = new HashSet<User_Dialog>();
        }
    
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public Nullable<System.DateTime> DayOfBirth { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string PhotoURL { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        public int? Age
        {
            get
            {
                if (DayOfBirth.HasValue)
                {
                    DateTime newDate = new DateTime(DateTime.Today.Subtract(DayOfBirth.Value).Ticks);
                    return newDate.Year - 1;
                }
                else
                    return null;
            }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        public List<Country> CountryList
        {
            get
            {
                SocialEntitiesContext db = new SocialEntitiesContext();
                //List<Country> list = db.Country.ToList();
                List<Country> list = new List<Country>
                {
                  new Country
                  {
                    Id = 1,
                    СountryName = "Belarus",
                    Users = new List<Users>()
                  }
                };
                return list;
            }
        }
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Dialog> User_Dialog { get; set; }
    }
}
