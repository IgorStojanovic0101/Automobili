using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Template.Application.Utilities;

namespace ArchDepo.WebAPI.Utilities
{
    public class EncryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AesHelper _encryptionService;

        public EncryptionMiddleware(RequestDelegate next, AesHelper encryptionService)
        {
            _next = next;
            _encryptionService = encryptionService;
        }

        public async Task InvokeAsync(HttpContext context)
        {

          
            var originalBodyStream = context.Response.Body;

            using (var newBodyStream = new MemoryStream())
            {
                // Replace the response body stream with our new stream
                context.Response.Body = newBodyStream;

                // Invoke the rest of the middleware pipeline
                await _next(context);

                // Rewind the new stream to the beginning
                newBodyStream.Seek(0, SeekOrigin.Begin);

                // Read the response body from the new stream
                var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();



                try
                {

                 
                    var encryptedBody = !string.IsNullOrEmpty(responseBody) ? _encryptionService.Encrypt(responseBody) : "\n";

                    context.Response.Body = originalBodyStream;

                    var responseBytes = Encoding.UTF8.GetBytes(encryptedBody);
                    await originalBodyStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
                catch (Exception ex)
                {
                    // Handle encryption errors and return an appropriate status code
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Encryption failed: " + ex.Message);
                    return;
                }
            }
        }

        private string EncryptProperties(string json)
        {
            try
            {
                // Parse the JSON into a dictionary
                var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                // Recursively encrypt properties
                var encryptedObject = EncryptPropertiesRecursive(jsonObject);

                // Convert the dictionary back to a JSON string
                return JsonConvert.SerializeObject(encryptedObject);
            }
            catch (Exception ex)
            {
                // Handle encryption errors
                throw new InvalidOperationException("Error encrypting properties", ex);
            }
        }

        private object EncryptPropertiesRecursive(object value)
        {
            if (value is string str)
            {
                // Encrypt string values
                try
                {
                    return _encryptionService.Encrypt(str);
                }
                catch (Exception ex)
                {
                    // Log the error and rethrow
                    throw new InvalidOperationException("Error encrypting string value", ex);
                }
            }

            else if (value is bool boolValue)
            {
                try
                {
                    return _encryptionService.EncryptBool(boolValue);
                }
                catch (Exception ex)
                {
                    // Log the error and rethrow
                    throw new InvalidOperationException("Error encrypting string value", ex);
                }
            }

            else if (value is int intValue)
            {
                // Encrypt integer values
                try
                {
                    return _encryptionService.EncryptInt(intValue);
                }
                catch (Exception ex)
                {
                    // Log the error and rethrow
                    throw new InvalidOperationException("Error encrypting integer value", ex);
                }
            }
            else if (value is DateTime dateTimeValue)
            {
                // Encrypt DateTime values
                try
                {
                    return _encryptionService.EncryptDateTime(dateTimeValue);
                }
                catch (Exception ex)
                {
                    // Log the error and rethrow
                    throw new InvalidOperationException("Error encrypting DateTime value", ex);
                }
            }

            else if (value is Dictionary<string, object> dictionary)
            {
                // Recursively encrypt dictionary values
                var result = new Dictionary<string, object>();
                foreach (var kvp in dictionary)
                {
                    result[kvp.Key] = EncryptPropertiesRecursive(kvp.Value);
                }
                return result;
            }
            else if (value is List<object> list)
            {
                // Recursively encrypt list values
                var result = new List<object>();
                foreach (var item in list)
                {
                    result.Add(EncryptPropertiesRecursive(item));
                }
                return result;
            }
            else if (value is JObject jObject)
            {
                // Handle JObject directly if using Newtonsoft.Json
                var result = new Dictionary<string, object>();
                foreach (var property in jObject.Properties())
                {
                    result[property.Name] = EncryptPropertiesRecursive(property.Value.ToObject<object>());
                }
                return result;
            }
            else
            {
                // Copy non-string values as is
                return value;
            }
        }
    }
}
