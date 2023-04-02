using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LSI.HOSP.AlaAllegro.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<int>
    {       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }
    }

    public class CreateUserCommandHandler //: IRequestHandler<CreateUserCommand, int>
    {
        //private readonly IRepository<User> repository;
       // private readonly ICustomerBaseFieldsService customerBaseFields;
       // private readonly IValidationProvider<Guest> _validationProvider;
        //private readonly AppDbContext _appDbContext;

       
       // public CreateUserCommandHandler(IRepository<User> repository,
                                         //ICustomerBaseFieldsService customerBaseFields,
                                         //IValidationProvider<Guest> validationProvider,
         //                                AppDbContext appDbContext)
        //{
          //  this.repository = repository;
            //this.customerBaseFields = customerBaseFields;
            //_validationProvider = validationProvider;
            //_appDbContext = appDbContext;
        //}

        /*
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            

            var user = new User
            {
                FirstName = request.FirstName               
            };
            //await guest.InternalValidate(_validationProvider, cancellationToken);
            await repository.AddAsync(user, cancellationToken);

            return user.Id;
        }*/

        //protected async Task CheckUniqueness(Expression<Func<Guest, bool>> expression)
       // {
        //    var exists = await repository.AnyAsync(expression);
         //   if (exists)
          //      throw new ElementNotUniqueException(typeof(Guest).Name); // nameof doesn't work with generic
        //}
       
    }
}
