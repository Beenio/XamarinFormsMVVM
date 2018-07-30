using FormsMVVM.Model.EntityViewModel;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsMVVM.Model.Entity
{
    public class UserEntity : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Age { get; set; }
    }
}
