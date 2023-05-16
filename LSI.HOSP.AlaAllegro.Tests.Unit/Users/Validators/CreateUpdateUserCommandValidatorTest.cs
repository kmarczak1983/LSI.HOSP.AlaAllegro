using LSI.HOSP.AlaAllegro.Application.Users.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Tests.Unit.Users.Validators
{
    [TestFixture]
    public class CreateUpdateUserCommandValidatorTest
    {
        private CreateUpdateUserCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateUpdateUserCommandValidator();
        }
    }
}
