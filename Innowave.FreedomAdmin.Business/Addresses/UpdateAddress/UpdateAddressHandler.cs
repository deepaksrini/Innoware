using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innowave.FreedomAdmin.DataContext;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Addresses.UpdateAddress
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand, HttpStatusCode>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateAddressHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<HttpStatusCode> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = new DataModel.Model.Address()
            {
                Id = request.Id,
                AddressLine1 = request.Address.AddressLine1,
                AddressLine2 = request.Address.AddressLine2,
                City = request.Address.City,
                State = request.Address.State,
                Country = request.Address.Country,
                PostalCode = request.Address.PostalCode
            };

            await unitOfWork.AddressRepository.Update(address);
            unitOfWork.Commit();
            return HttpStatusCode.OK;
        }
    }
}
