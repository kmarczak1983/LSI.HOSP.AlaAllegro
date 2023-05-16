using AutoFixture;
using LSI.HOSP.AlaAllegro.Domain.Entities.Auctions;
using LSI.HOSP.AlaAllegro.Domain.Entities.Users;
using LSI.HOSP.AlaAllegro.Infrastructure;
using LSI.HOSP.AlaAllegro.Infrastructure.DataAccess.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Tests.Unit.Users.Handlers
{
    [TestFixture]
    public class CreateUpdateUserCommandHandlerTest
    {
        private Mock<AppDbContext> _mockedDbContext;
        private Mock<IRepository<User>> _userRepositoryMock;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _userRepositoryMock = new Mock<IRepository<User>>();
            _mockedDbContext = new Mock<AppDbContext>();
            _fixture.Customize<User>(u => u.Without(a => a.Auctions).Without(po => po.PurchaseOffers));                        
        }

        [Test]

    }
}
