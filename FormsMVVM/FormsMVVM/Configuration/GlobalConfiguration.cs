using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsMVVM.Configuration
{
    public class GlobalConfiguration
    {
        public static void Configure()
        {
            ConfigureMapster();
        }

        private static void ConfigureMapster()
        {
            TypeAdapterConfig.GlobalSettings.Scan(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
