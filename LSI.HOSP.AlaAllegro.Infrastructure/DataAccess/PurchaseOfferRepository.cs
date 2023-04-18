using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Domain.Exceptions;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure.DataAccess
{
    
    public class PurchaseOfferRepository : Repository<PurchaseOffer>, IPurchaseOfferRepository
    {
        public PurchaseOfferRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PurchaseOffer> GetLast(Guid auctionId, CancellationToken cancellationToken)
        {
            return await _dbContext.PurchaseOffers
                .GetFiltered()
                .Include(po => po.User)
                .OrderBy(po => po.LastModifiedDate)
                .LastOrDefaultAsync(po => po.AuctionId == auctionId, cancellationToken);               
        }
    }
}
