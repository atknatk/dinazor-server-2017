using System.Configuration;
using System.Data;
using Dinazor.Core.Database.Context;
using Dinazor.Core.Logger.Interface;
using log4net;
using log4net.Appender;
using log4net.Core;
using Npgsql;

namespace Dinazor.Core.Logger.Manager
{
    public class BasicLoggerConfig : AppenderSkeleton, ILoggerConfig
    { 
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void Append(LoggingEvent loggingEvent)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings[DinazorContext.ConnectionString].ConnectionString))
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO dinazor.\"DinazorLog\" (\"LogDate\",\"Thread\",\"LogLevel\",\"Logger\",\"Message\",\"StackTrace\") VALUES (:log_date, :thread, :log_level, :logger, :message, :exception)", conn))
                {
                    var logDate = command.CreateParameter();
                    logDate.Direction = ParameterDirection.Input;
                    logDate.DbType = DbType.DateTime;
                    logDate.ParameterName = ":log_date";
                    logDate.Value = loggingEvent.TimeStamp;
                    command.Parameters.Add(logDate);

                    var thread = command.CreateParameter();
                    thread.Direction = ParameterDirection.Input;
                    thread.DbType = DbType.String;
                    thread.ParameterName = ":thread";
                    thread.Value = loggingEvent.ThreadName;
                    command.Parameters.Add(thread);

                    var logLevel = command.CreateParameter();
                    logLevel.Direction = ParameterDirection.Input;
                    logLevel.DbType = DbType.String;
                    logLevel.ParameterName = ":log_level";
                    logLevel.Value = loggingEvent.Level;
                    command.Parameters.Add(logLevel);

                    var logger = command.CreateParameter();
                    logger.Direction = ParameterDirection.Input;
                    logger.DbType = DbType.String;
                    logger.ParameterName = ":logger";
                    logger.Value = loggingEvent.LoggerName;
                    command.Parameters.Add(logger);

                    var message = command.CreateParameter();
                    message.Direction = ParameterDirection.Input;
                    message.DbType = DbType.String;
                    message.ParameterName = ":message";
                    message.Value = loggingEvent.RenderedMessage;
                    command.Parameters.Add(message);

                    var exception = command.CreateParameter();
                    exception.Direction = ParameterDirection.Input;
                    exception.DbType = DbType.String;
                    exception.ParameterName = ":exception";
                    exception.Value = loggingEvent.GetExceptionString();
                    command.Parameters.Add(exception);

                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
