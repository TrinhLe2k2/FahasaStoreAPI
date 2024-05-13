using System;
using System.Collections.Generic;
using FahasaStoreAPI.Models.BasicModels;

namespace FahasaStoreAPI.Models.EntitiesModels
{
    public partial class AuthorEntities
    {
        public AuthorEntities()
        {
            Books = new HashSet<BookBasic>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<BookBasic> Books { get; set; }
    }
}
