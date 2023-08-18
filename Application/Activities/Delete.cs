using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
                
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var actiity = await _context.Activities.FindAsync(request.Id);

                // TODO: Handle null object

                // Removed just from EF Change Tracking in memory.
                _context.Remove(actiity);

                // Commit to DB
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}