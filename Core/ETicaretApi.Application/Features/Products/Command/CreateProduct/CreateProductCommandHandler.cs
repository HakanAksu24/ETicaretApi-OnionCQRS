using ETicaretApi.Application.Features.Products.Exceptions;
using ETicaretApi.Application.Features.Products.Rules;
using ETicaretApi.Application.Interfaces.UnitOfWorks;
using ETicaretApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest ,  Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ProductRules productRules;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, ProductRules productRules)
        {
            this.unitOfWork = unitOfWork;
            this.productRules = productRules;
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            IList<Product> products = await unitOfWork.GetReadRepository<Product>().GetAllAsync();

            await productRules.ProductTitleMustNotBeSame(products, request.Title);

            Product product = new(request.Title, request.Description, request.BrandId, request.Price, request.Discount);

            await unitOfWork.GetWriteRepository<Product>().AddAsync(product);

            //Productdaki birden fazla categoryId olduğu için burada productın ıd sini her seferinde çekip yanına categoryId ekliyoruz.
            if (await unitOfWork.SaveAsync() > 0)
            {
                foreach(var categoryId in request.CategoryIds)
                {
                    await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId
                    });
                }
                //Yukarıdaki ilk SaveAsync() işlemini ProductCategory içerisinde productıd yi eşlemek için productı oluşturmamız gerekiyor.
                await unitOfWork.SaveAsync();
            }

            return Unit.Value;

        }
    }
}
