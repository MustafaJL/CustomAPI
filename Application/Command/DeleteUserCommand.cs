using MediatR;


namespace Application.Command
{
    public record DeleteUserCommand(long userId) : IRequest<bool>;

}
