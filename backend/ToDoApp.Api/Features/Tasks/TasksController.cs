namespace ToDoApp.Api.Features.Tasks;

[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ITaskMapper _mapper;

    public TasksController(ISender mediator, ITaskMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet($"{{{nameof(id)}}}", Name = nameof(GetTaskAsync))]
    public async Task<ActionResult<TaskResponse>> GetTaskAsync(Guid id)
    {
        var foundTask = await _mediator.Send(new GetTaskQuery(id));

        return _mapper.MapToResponse(foundTask);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(CreateTaskRequest requestModel)
    {
        var createdTask = await _mediator.Send(_mapper.MapToCommand(requestModel));
        var response = _mapper.MapToResponse(createdTask);

        return CreatedAtRoute(nameof(GetTaskAsync), new { response.Id }, response);
    }
}
