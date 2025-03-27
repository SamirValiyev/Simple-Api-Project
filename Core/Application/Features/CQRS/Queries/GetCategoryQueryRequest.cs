using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest : IRequest<CategoryDTO>
    {
        public int Id { get; set; }

        public GetCategoryQueryRequest(int id)
        {
            Id = id;   
        }
    }
}
