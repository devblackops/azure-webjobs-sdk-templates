#r "Microsoft.Azure.ApiHub.Sdk" 

using System;
using System.Net;
using Microsoft.Azure.ApiHub;

public class Contact
{
    public string Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
}


public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ITable<Contact> input, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    ContinuationToken continuationToken = null;
    do
    {
        var segment = await input.ListEntitiesAsync(continuationToken: continuationToken);

        foreach (var item in segment.Items)
        {
            log.Info(item.FirstName + " " + item.LastName);
        }

        continuationToken = segment.ContinuationToken; ;
    }
    while (continuationToken != null);

    return req.CreateResponse(HttpStatusCode.OK);
}
