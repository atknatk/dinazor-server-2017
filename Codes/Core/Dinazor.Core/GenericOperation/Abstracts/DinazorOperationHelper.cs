using System.Data.Entity;
using log4net;
using log4net.Core; 

namespace Dinazor.Core.GenericOperation.Abstracts
{
    public abstract class DinazorOperationHelper
    {
        // protected readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly Level LogLevel = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level;

        protected void ShowSql(DbContext ctx)
        {
           /* if (LogLevel != Level.Debug) return;
            var serviceProvider = ctx.GetInfrastructure<IServiceProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new MyFilteredLoggerProvider());*/
        }
    }
}
