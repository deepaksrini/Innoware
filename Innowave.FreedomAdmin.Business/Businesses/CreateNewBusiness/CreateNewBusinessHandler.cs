using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using MediatR;
using Innowave.FreedomAdmin.DataModel;
using Innowave.FreedomAdmin.Business.Model;
using Innowave.FreedomAdmin.DataModel.Model;
using Innowave.FreedomAdmin.Business.Security;
using System.Linq;
using Innowave.FreedomAdmin.Business.Utilities.Constants;

namespace Innowave.FreedomAdmin.Business.Businesses.CreateNewBusiness
{
    public class CreateNewBusinessHandler : IRequestHandler<CreateNewBusinessCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICryptographyHelper cryptographyHelper;

        public CreateNewBusinessHandler(
            IUnitOfWork unitOfWork,
            ICryptographyHelper cryptographyHelper)
        {
            this.unitOfWork = unitOfWork;
            this.cryptographyHelper = cryptographyHelper;
        }

        public async Task<HttpStatusCode> Handle(CreateNewBusinessCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();

            var business = new DataModel.Model.Businesses()
            {
                Id = id,
                Name = request.Name,
                IsActive = true,
                Logo = "",
                MobileTelephone = request.MobileTelephone,
                WorkTelephone = request.WorkTelephone,
                Fax = request.Fax,
                Website = request.Website,
                Email = request.Email,
                CreatedBy = request.UserId,
                LastUpdatedBy = request.UserId
            };

            var address = new DataModel.Model.Address()
            {
                Id = id,
                AddressLine1 = request.Address.AddressLine1,
                AddressLine2 = request.Address.AddressLine2,
                City = request.Address.City,
                State = request.Address.State,
                Country = request.Address.Country,
                PostalCode = request.Address.PostalCode
            };

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = request.Name,
                UserName = request.UserName,
                Password = cryptographyHelper.Encrypt(request.Password),
                DefaultProductId = request.Products.FirstOrDefault(),
                DefaultRoleId = Guid.Parse(Constant.Admin),
                BusinessId = id,
                IsActive = true,
                CreatedBy = request.UserId,
                LastUpdatedBy = request.UserId
            };

            var rows = await unitOfWork.BusinessRepository.CreateNewBusiness(business);

            foreach(var item in request.Products)
            {
                var productsByBusinesses = new ProductsByBusinesses()
                {
                    ProductId = item,
                    BusinessId = id,
                    AssignedBy = request.UserId,
                    AssignedDate = DateTime.Now
                };
                await unitOfWork.BusinessRepository.InsertProductsByBusinesses(productsByBusinesses);
            }
            await unitOfWork.UserRepository.CreateNewUser(user);
            await unitOfWork.AddressRepository.Insert(address);
            unitOfWork.Commit();
            return HttpStatusCode.OK;
        }
    }
}
