using AirWatch.Domain.Common;

namespace AirWatch.Domain.Entities;

public class IntegrationLog : BaseEntity
{
    public Guid? CityId { get; private set; }
    public string ApiName { get; private set; }
    public string Endpoint { get; private set; }
    public string HttpMethod { get; private set; }
    public int? HttpStatus { get; private set; }
    public int? RecordsCount { get; private set; }
    public string Result { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime RequestedAt { get; private set; }
    public int? ResponseMs { get; private set; }

    
    public City? City { get; private set; }

    public IntegrationLog(Guid? cityId, string apiName, string endpoint, string httpMethod,
        int? httpStatus, int? recordsCount, string result,
        string? errorMessage, int? responseMs)
    {
        if (string.IsNullOrWhiteSpace(apiName))
            throw new InvalidOperationException("O nome da API é obrigatório.");
        if (string.IsNullOrWhiteSpace(endpoint))
            throw new InvalidOperationException("O endpoint é obrigatório.");
        if (string.IsNullOrWhiteSpace(httpMethod))
            throw new InvalidOperationException("O método HTTP é obrigatório.");
        if (string.IsNullOrWhiteSpace(result))
            throw new InvalidOperationException("O resultado é obrigatório.");

        CityId = cityId;
        ApiName = apiName;
        Endpoint = endpoint;
        HttpMethod = httpMethod;
        HttpStatus = httpStatus;
        RecordsCount = recordsCount;
        Result = result;
        ErrorMessage = errorMessage;
        RequestedAt = DateTime.UtcNow;
        ResponseMs = responseMs;
    }
}