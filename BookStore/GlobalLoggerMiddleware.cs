namespace BookStore
{
  public class GlobalLoggerMiddleware
  {
    private readonly RequestDelegate next;
    private readonly string logDirectory = "Logs";

    public GlobalLoggerMiddleware(RequestDelegate next)
    {
      this.next = next;
      Directory.CreateDirectory(logDirectory); 
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await next(context); 
      }
      catch (Exception ex)
      {
        LogException(ex);
        throw; 
      }
    }

    private void LogException(Exception ex)
    {
      var severity = GetSeverity(ex);
      var logFile = Path.Combine(logDirectory, $"{severity.ToString().ToLower()}.log");

      string logText = $"[{DateTime.Now}] [{severity}] {ex.Message}";

      File.AppendAllText(logFile, logText);
    }

    private LogLevel GetSeverity(Exception ex)
    {
      if (ex is ArgumentNullException || ex is ArgumentException ||  ex is NotImplementedException)
        return LogLevel.Warning;

      else if (ex is UnauthorizedAccessException || ex is NullReferenceException)
        return LogLevel.Critical; 

      else
        return LogLevel.Error;
    }
  }
}
