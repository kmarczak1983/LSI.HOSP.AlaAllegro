﻿using LSI.HOSP.AlaAllegro.Domain.Entities;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces
{
    public interface IAuctionRepository : IRepository<Auction>
    {
        Task<Auction> Get(Guid id, CancellationToken cancellationToken);
    }
}
