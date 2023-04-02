using LSI.HOSP.AlaAllegro.Infrastructure;
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
    public class CreateUserCommand
    {

    }

    public class CreateGuestCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; } = null;
        
        public string DocumentNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        
    }

    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, int>
    {
        //private readonly IRepository<Guest> repository;
       // private readonly ICustomerBaseFieldsService customerBaseFields;
       // private readonly IValidationProvider<Guest> _validationProvider;
        private readonly AppDbContext _appDbContext;

       
        public CreateGuestCommandHandler(IRepository<Guest> repository,
                                         ICustomerBaseFieldsService customerBaseFields,
                                         IValidationProvider<Guest> validationProvider,
                                         AppDbContext appDbContext)
        {
            this.repository = repository;
            this.customerBaseFields = customerBaseFields;
            _validationProvider = validationProvider;
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var marketingConsents = await customerBaseFields.GetMarketingConsents(request.MarketingConsents, cancellationToken);
            if (await repository.GetQueryable()
                .AnyAsync(g =>
                        g.DocumentNumber.Equals(request.DocumentNumber) && g.DocumentType == request.DocumentType,
                    cancellationToken)) throw new ElementNotUniqueException(nameof(Guest));
            City city = null;
            Country country = null;

            if (request.CityId.HasValue)
                city = await _appDbContext.Set<City>().FirstOrDefaultAsync(c => request.CityId == c.Id, cancellationToken);
            if (request.CountryId.HasValue)
                country = await _appDbContext.Set<Country>().FirstOrDefaultAsync(c => request.CountryId == c.Id, cancellationToken);

            var guest = new Guest
            {
                Birthday = request.Birthday,
                CityId = city?.Id,
                CountryId = country?.Id,
                Pin = request.Pin,
                Sex = request.Sex,
                Street = request.Street,
                Zip = request.Zip,
                DocumentNumber = request.DocumentNumber,
                DocumentType = request.DocumentType,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MarketingConsents = marketingConsents,
                PhoneNumber = request.Phone,
            };
            await guest.InternalValidate(_validationProvider, cancellationToken);
            await repository.AddAsync(guest, cancellationToken);

            return guest.Id;
        }

        protected async Task CheckUniqueness(Expression<Func<Guest, bool>> expression)
        {
            var exists = await repository.AnyAsync(expression);
            if (exists)
                throw new ElementNotUniqueException(typeof(Guest).Name); // nameof doesn't work with generic
        }
       
    }
}
