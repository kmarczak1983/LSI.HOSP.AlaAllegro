using LSI.HOSP.AlaAllegro.Application.Common.Commands;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class GetAuctionByIdQuery : BaseCommand, IRequest<AuctionViewModel>
    {
        public GetAuctionByIdQuery(int id)
        {
            Id = id;
        }

        public class GetAuctionByIdQueryHandler : IRequestHandler<GetAuctionByIdQuery, AuctionViewModel>
        {
            private readonly AppDbContext _context;
            
            public GetAuctionByIdQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<AuctionViewModel> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
            {
                var auction = await _context.Auctions.GetFiltered().GetFirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                return new AuctionViewModel(auction.Id, auction.Title, auction.Body);
            }
        }
    }
}
