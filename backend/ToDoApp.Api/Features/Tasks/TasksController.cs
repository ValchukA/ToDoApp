using ToDoApp.Api.Features.Tasks.Contracts;
using ToDoApp.Api.Features.Tasks.Mapping;

namespace ToDoApp.Api.Features.Tasks;

[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IObjectMapper _mapper;

    public TasksController(ISender mediator, IObjectMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet($"{{{nameof(id)}}}", Name = nameof(GetTaskAsync))]
    public async Task<ActionResult<TaskResponse>> GetTaskAsync(Guid id)
    {
        var result = await _mediator.Send(new GetTaskQuery(id));

        return _mapper.MapToResponse(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(CreateTaskRequest requestModel)
    {
        var result = await _mediator.Send(_mapper.MapToCommand(requestModel));
        var response = _mapper.MapToResponse(result);

        return CreatedAtRoute(nameof(GetTaskAsync), new { response.Id }, response);
    }
}
