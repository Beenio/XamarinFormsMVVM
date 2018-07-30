using FormsMVVM.Model.Entity;
using FormsMVVM.Model.EntityViewModel;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsMVVM.Model.Mapper
{
    public class UserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserEntity, UserViewModel>();
            config.NewConfig<UserViewModel, UserEntity>();
        }
    }
}
