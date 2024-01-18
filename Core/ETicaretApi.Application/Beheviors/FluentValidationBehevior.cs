using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Beheviors
{
    public class FluentValidationBehevior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validator;

        public FluentValidationBehevior(IEnumerable<IValidator<TRequest>> validator)
        {
            this.validator = validator;
        }


        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failtures = validator
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .GroupBy(x => x.ErrorMessage)
                .Select(x => x.First())
                .Where(f => f != null)
                .ToList();

            //Request nesnemizle beraber gelen yapıyı (context i) ValidationContext ile yakalıyoruz
            //Request nesnemizi validate ediyoruz. kullanacağımız alanları, hataları ve boşları listeden kaldırdık. 

            if(failtures.Any())
             throw new ValidationException(failtures);
            

            return next();
        }

    }
}
