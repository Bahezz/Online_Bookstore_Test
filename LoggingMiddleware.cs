namespace Online_Bookstore
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LoggingMiddleware> _logger;

		public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			_logger.LogInformation("HTTP Request: {method} {url} at {time}",
				context.Request.Method,
				context.Request.Path,
				DateTime.UtcNow);

			await _next(context);

			_logger.LogInformation("HTTP Response: {statusCode} at {time}",
				context.Response.StatusCode,
				DateTime.UtcNow);
		}
	}
}
