using System.Collections.Generic;
using System.Reflection;

namespace WorkinghoursManagement.CrossCutting.Assemblies
{
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("WorkinghoursManagement.Api"),
                Assembly.Load("WorkinghoursManagement.Application"),
                Assembly.Load("WorkinghoursManagement.Domain"),
                Assembly.Load("WorkinghoursManagement.Domain.Core"),
                Assembly.Load("WorkinghoursManagement.Infrastructure"),
                Assembly.Load("WorkinghoursManagement.CrossCutting")
            };
        }
    }
}