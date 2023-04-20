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
    
    public class AuctionRepository : Repository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Auction> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Auctions
                .GetFiltered()
                .Include(a => a.Author)
                .GetFirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}
