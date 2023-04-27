using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Application.Common;


using LSI.HOSP.AlaAllegro.Domain.Exceptions;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using LSI.HOSP.AlaAllegro.Infrastructure.Services;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Commands
{    
    public class CreateUpdateAuctionCommand : IRequest<Unit>
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string InitialPrice { get; set; }

        private Guid Id { get; set; }

        public CreateUpdateAuctionCommand()
        {
        }

        public CreateUpdateAuctionCommand(Guid id)
        {
            Id = id;
        }
        
        public Guid GetGuid()
        { 
            return Id; 
        }
    }

    public class CreateUpdateAuctionCommandHandler : IRequestHandler<CreateUpdateAuctionCommand, Unit>
    {
        private readonly IRepository<Auction> repository;        
        private readonly ICurrentUserService _currentUserService;


        public CreateUpdateAuctionCommandHandler(IRepository<Auction> repository,                                         
                                                 ICurrentUserService currentUserService)
        {
            this.repository = repository;            
            _currentUserService = currentUserService;
        }


        public async Task<Unit> Handle(CreateUpdateAuctionCommand request, CancellationToken cancellationToken)
        {
            var auctionId = request.GetGuid();
            if (auctionId == Guid.Empty)
            {
                var auction = new Auction
                {
                    Title = request.Title,
                    Body = request.Body,
                    StartPrice = Convert.ToDecimal(request.InitialPrice),
                    AuthorId = (int)_currentUserService.GetUserId
                };

                await repository.AddAsync(auction, cancellationToken);
            }
            else
            {                
                var auction = await repository.GetFirstOrDefaultAsync(a => a.Id == auctionId, cancellationToken);

                if (auction == null)
                    throw new EntityNotFoundException(nameof(Auction));

                auction.Title = request.Title; 
                auction.Body = request.Body;
                auction.StartPrice = Convert.ToDecimal(request.InitialPrice);

                await repository.UpdateAsync(auction, cancellationToken);
            }

            return Unit.Value;
        }       
    }
}
