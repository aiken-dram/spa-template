using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Sample.Commands.UpsertSample;

#warning SAMPLE, remove entire file in actual application
public class UpsertSampleChildDto
{
    /// <summary>
    /// Is new child
    /// </summary>
    public bool? IsNew { get; set; }

    /// <summary>
    /// Id of sample child in database
    /// </summary>
    public long IdChild { get; set; }

    /// <summary>
    /// Id of sample
    /// </summary>
    public long IdSample { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    public string? Text { get; set; }
}

public class UpsertSampleCommand : IRequest<long>
{
    /// <summary>
    /// Id of sample in database
    /// </summary>
    public long? IdSample { get; set; }

    /// <summary>
    /// Id of district
    /// </summary>
    public int? IdDistrict { get; set; }

    /// <summary>
    /// Id of sample type
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Id of sample dictionary
    /// </summary>
    public long IdDict { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Number field
    /// </summary>
    public long? Number { get; set; }

    /// <summary>
    /// Date field
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Time stamp field
    /// </summary>
    public DateTime? TimeStamp { get; set; }

    /// <summary>
    /// Decimal field
    /// </summary>
    public decimal? Sum { get; set; }

    /// <summary>
    /// List of sample children
    /// </summary>
    public IEnumerable<UpsertSampleChildDto> SampleChildren { get; set; } = null!;
}

public class UpsertSampleCommandHandler : IRequestHandler<UpsertSampleCommand, long>
{
    private readonly ISPADbContext _context;
    private readonly ILogger _logger;

    private IAuditBuilder _audit => _context.AuditBuilder;

    public UpsertSampleCommandHandler(
        ISPADbContext context,
        ILogger<UpsertSampleCommand> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<long> Handle(UpsertSampleCommand request, CancellationToken cancellationToken)
    {
        //check access
        _logger.JsonLogDebug("request", request);

        Domain.Entities.Sample? entity;
        Audit audit;

        if (request.IdSample.HasValue)
        {
            //edit
            entity = await _context.Samples
                .Include(p => p.SampleChildren)
                .GetAsync(p => p.IdSample == request.IdSample.Value, cancellationToken);

            audit = await _audit.EditAsync(entity, request);
        }
        else
        {
            //create
            entity = new Domain.Entities.Sample();
            _context.Samples.Add(entity);
            audit = await _audit.CreateAsync(entity, request);
        }

        //set fields
        entity.IdDistrict = request.IdDistrict;
        entity.IdType = (eSampleType)request.IdType;
        entity.IdDict = request.IdDict;
        entity.Text = request.Text;
        entity.Number = request.Number;
        entity.Date = request.Date;
        entity.Timestamp = request.TimeStamp;
        entity.Sum = request.Sum;

        //collection of child
        _context.SampleChildren.UpdateCollection(
            entity.SampleChildren,
            request.SampleChildren,
            (p, r) =>
            {
                p.Text = r.Text;
            },
            r => new SampleChild()
            {
                Text = r.Text,
            },
            e => (p => p.IdChild == e.IdChild),
            r => r.IsNew.HasValue && r.IsNew.Value);

        entity.Log(audit);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.IdSample;
    }
}