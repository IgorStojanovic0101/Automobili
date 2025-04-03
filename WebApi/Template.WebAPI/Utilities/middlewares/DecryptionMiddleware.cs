using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Template.Application.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace ArchDepo.WebAPI.Utilities
{
    public class DecryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RsaHelper _decryptionService;

        public DecryptionMiddleware(RequestDelegate next, RsaHelper decryptionService)
        {
            _next = next;
            _decryptionService = decryptionService;
        }



        public async Task InvokeAsync(HttpContext context)
        {

            // Skip decryption if the URL path does not contain "/api"
            if (!context.Request.Path.Value.Contains("/api", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json"))
            {
                var originalBody = context.Request.Body;
                try
                {
                    // Read the entire request body as a string
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                    {
                        var encryptedBody = await reader.ReadToEndAsync();

                        // Decrypt the entire body using AES (or your preferred algorithm)
                        var decryptedBody = _decryptionService.Decrypt(encryptedBody);

                        // Replace the original body with the decrypted one
                        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(decryptedBody));
                        context.Request.Body = memoryStream;
                        memoryStream.Seek(0, SeekOrigin.Begin); // Rewind the stream to the beginning
                    }
                }
                catch (Exception ex)
                {
                    // Handle decryption errors and return BadRequest
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Decryption failed: " + ex.Message);
                    return;
                }
            }

            // Proceed with the request
            await _next(context);
        }

    }
}
