using System.Collections.Generic;
using MediatR;

namespace Innowave.FreedomAdmin.Business.Businesses.GetAllBusiness
{
    public class GetAllBusinessQuery : IRequest<List<DataModel.Model.Businesses>>
    {
    }
}
