using LSI.HOSP.AlaAllegro.Application.Common.Queries;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using MathNet.Numerics.Statistics.Mcmc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace LSI.HOSP.AlaAllegro.Application.Auctions.Queries
{
    public class GetAuctionsQuery : IRequest<List<AuctionListViewModel>>
    {

    }

    public class GetAuctionsQueryHandler : BaseQueryListHandler<GetAuctionsQuery, AuctionListViewModel, Auction>
    {
        public GetAuctionsQueryHandler(IRepository<Auction> repository) : base(repository)
        {
        }

        protected override Expression<Func<Auction, AuctionListViewModel>> Map() =>
            a => new AuctionListViewModel(
                a.Id.ToString(),               
                a.Title,
                a.Author.FirstName + " " + a.Author.LastName,
                a.LastModifiedDate.ToString(),
                a.PurchaseOffers != null && a.PurchaseOffers.Any() ? 
                    a.PurchaseOffers.OrderByDescending(po => po.CreatedDate).Last().Price.ToString() : a.StartPrice.ToString()
                );

        /*

        public GetAuctionsQueryHandler(IRepository<Auction> repository) : base(repository)
        {
        }

        public override async Task<List<AuctionListViewModel>> Handle(GetAuctionsQuery request, CancellationToken cancellationToken)
        {
            var result = await Query(request).ToListAsync(cancellationToken);
            return result.Select(MapAuction).ToList();
        }

        protected override IQueryable<Auction> Query(GetAuctionsQuery request)
        {
            return base.Query(request).Include(a => a.Author);
                
                //.Select<Auction, User>(a => new { a.Id, a.Title, a.LastModifiedDate, a.StartPrice, a.Author.FirstName, a.Author.LastName })
                
                //.OrderBy(a => a.Title);
                //.Select(a => new { a.Id, a.Title, a.LastModifiedDate, a.StartPrice })
            ;
            //.Include(c => c.RoomType).Include(c => c.HousekeepingStatusRooms.Where(c => !c.IsDeleted)).ThenInclude(c => c.HousekeepingRoom).OrderBy(c => c.Name);
        }

        private AuctionListViewModel MapAuction(Auction auction) => 
            new AuctionListViewModel(
                auction.Id.ToString(), 
                auction.Title,
                auction.Author.FirstName + " " + auction.Author.LastName,
                auction.LastModifiedDate.ToString(), 
                auction.StartPrice.ToString());


        protected override Expression<Func<Auction, AuctionListViewModel>> Map() => auction =>
            new AuctionListViewModel(
                auction.Id.ToString(), 
                auction.Title, 
                auction.Author.FirstName + " " + auction.Author.LastName, 
                auction.LastModifiedDate.ToString(), 
                auction.StartPrice.ToString());
        */
    }
}
