using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using HeartRates.Business;
using HeartRates.Contract.Responses;

namespace HeartRates.WebApi.Controllers;

[Route("heart-rate")]
[ApiController]
public sealed class HeartRatesController(
    IHeartRateService heartRateService) : ControllerBase
{
    /// <summary>
    /// Get all instances where a patient's heart rate reading exceeded a threshold (e.g. 100 bpm),
    /// including the timestamps of those events
    /// </summary>
    [HttpGet("high/{patientId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Get high heart rates events", typeof(HighHeartRateEventResponse))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "patient details not found")]
    public async Task<IActionResult> GetHighRatesAsync(string patientId)
    {
        var result = await heartRateService.GetHighHeartRateEvents(patientId);
        
        return result == null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Get all instances where a patient's heart rate reading exceeded a threshold (e.g. 100 bpm),
    /// including the timestamps of those events
    /// </summary>
    [HttpGet("analytics/{patientId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Get high heart rates events", typeof(HeartRateAnalyticsResponse))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "patient details not found")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "aupplied arguments are invalid")]
    public async Task<IActionResult> GetAnalyticsAsync(string patientId, [FromQuery] DateTime from, [FromQuery] DateTime? to = null)
    {
        if (from > to)
            return BadRequest();
        
        var result = await heartRateService.GetHeartRateAnalytics(patientId, from, to);
        
        return result == null ? NotFound() : Ok(result);
    }
}