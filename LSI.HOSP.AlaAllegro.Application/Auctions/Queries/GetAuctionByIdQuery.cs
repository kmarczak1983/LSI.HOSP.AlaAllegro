using LSI.HOSP.AlaAllegro.Application.Common.Commands;
using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class GetAuctionByIdQuery : BaseCommand<Guid>, IRequest<AuctionViewModel>
    {
        public GetAuctionByIdQuery(Guid id)
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
                var auction = await _context
                    .Auctions
                    .GetFiltered()                    
                    .Include(a => a.Author)
                    .Select(a => new { 
                        a.Id, a.Title, a.Body, a.CreatedDate, a.LastModifiedDate, a.StartPrice,
                        FullName = a.Author.FirstName + " " + a.Author.LastName, a.Author.Email, a.Author.PhoneNumber })
                    .GetFirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

                var lastPurchaseOffer = await _context
                    .PurchaseOffers
                    .GetFiltered()
                    .Include(po => po.User)
                    .OrderByDescending(po => po.CreatedDate)
                    .Select(po => new { po.Price, po.AuctionId, FullName = po.User.FirstName + " " + po.User.LastName })
                    
                    .GetFirstOrDefaultAsync(po => po.AuctionId == auction.Id, cancellationToken, false);

                return new AuctionViewModel(
                        auction.Id.ToString(), 
                        auction.Title, 
                        new AuctionViewModel.UserCreatedBy(auction.FullName, auction.Email, auction.PhoneNumber), 
                        auction.Body,
                        auction.LastModifiedDate,
                        auction.CreatedDate,
                        lastPurchaseOffer is null ? auction.StartPrice.ToString() : lastPurchaseOffer.Price.ToString(),
                        lastPurchaseOffer is null ? null : lastPurchaseOffer.FullName);
            }
        }
    }
}
