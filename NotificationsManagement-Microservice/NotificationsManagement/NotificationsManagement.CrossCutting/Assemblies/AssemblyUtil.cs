using System.Collections.Generic;
using System.Reflection;

namespace NotificationsManagement.CrossCutting.Assemblies
{
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("NotificationsManagement.Api"),
                Assembly.Load("NotificationsManagement.Application"),
                Assembly.Load("NotificationsManagement.Domain"),
                Assembly.Load("NotificationsManagement.Domain.Core"),
                Assembly.Load("NotificationsManagement.Infrastructure"),
                Assembly.Load("NotificationsManagement.CrossCutting")
            };
        }
    }
}