namespace BookStore
{
    public class GlobalLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logDirectory = "Logs";

        public GlobalLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
            Directory.CreateDirectory(_logDirectory);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.Now;
            try
            {
                await _next(context);
                LogRequest(context, startTime);
            }
            catch (Exception ex)
            {
                LogException(ex, context, startTime);
                throw;
            }
        }

        private void LogRequest(HttpContext context, DateTime startTime)
        {
            var duration = DateTime.Now - startTime;
            string logText = $"[{DateTime.Now}] [INFO] {context.Request.Method} {context.Request.Path} responded {context.Response.StatusCode} in {duration.TotalMilliseconds}ms";

            File.AppendAllText(Path.Combine(_logDirectory, "requests.log"), logText + Environment.NewLine);
        }

        private void LogException(Exception ex, HttpContext context, DateTime startTime)
        {
            var severity = GetSeverity(ex);
            string logFile = Path.Combine(_logDirectory, $"{severity.ToString().ToLower()}.log");

            var duration = DateTime.Now - startTime;
            string logText = $"[{DateTime.Now}] [{severity}] {context.Request.Method} {context.Request.Path} in {duration.TotalMilliseconds}ms - {ex.Message}";

            File.AppendAllText(logFile, logText + Environment.NewLine);
        }

        private LogLevel GetSeverity(Exception ex)
        {
            if (ex is ArgumentNullException || ex is ArgumentException || ex is NotImplementedException)
                return LogLevel.Warning;
            else if (ex is UnauthorizedAccessException || ex is NullReferenceException)
                return LogLevel.Critical;
            else
                return LogLevel.Error;
        }
    }
}
