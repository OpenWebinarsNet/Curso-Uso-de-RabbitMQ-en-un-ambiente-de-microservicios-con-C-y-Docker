using System.Collections.Generic;
using System.Reflection;

namespace UsersManagement.CrossCutting.Assemblies
{
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("UsersManagement.Api"),
                Assembly.Load("UsersManagement.Application"),
                Assembly.Load("UsersManagement.Domain"),
                Assembly.Load("UsersManagement.Domain.Core"),
                Assembly.Load("UsersManagement.Infrastructure"),
                Assembly.Load("UsersManagement.CrossCutting")
            };
        }
    }
}