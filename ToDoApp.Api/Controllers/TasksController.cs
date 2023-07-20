namespace ToDoApp.Api.Controllers;

[Route("api/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IObjectMapper<TasksController> _mapper;

    public TasksController(ISender mediator, IObjectMapper<TasksController> mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id}", Name = nameof(GetTaskAsync))]
    public async Task<ActionResult<TaskResponse>> GetTaskAsync(Guid id)
    {
        var result = await _mediator.Send(new GetTaskQuery(id));
        var response = _mapper.Map<TaskResponse>(result);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> CreateTask([FromBody] CreateTaskRequest requestModel)
    {
        var command = _mapper.Map<CreateTaskCommand>(requestModel);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<TaskResponse>(result);

        return CreatedAtRoute(nameof(GetTaskAsync), new { response.Id }, response);
    }
}
