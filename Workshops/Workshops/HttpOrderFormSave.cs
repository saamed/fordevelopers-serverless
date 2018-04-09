
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;

namespace Workshops
{
  public static class HttpOrderFormSave
  {
    [FunctionName("HttpOrderFormSave")]
    public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
    {
      PhotoOrder order = null;
      try
      {
        string requestBody = new StreamReader(req.Body).ReadToEnd();
        order = JsonConvert.DeserializeObject<PhotoOrder>(requestBody);
      }
      catch (Exception e)
      {
        return new BadRequestObjectResult("Received data is invalid");
      }
      return (ActionResult)new OkObjectResult("Order processed");
    }
  }

  public class PhotoOrder
  {
    public string CustomerEmail { get; set; }
    public string FileName { get; set; }
    public int RequiredHeight { get; set; }
    public int RequiredWidth { get; set; }
  }
}
