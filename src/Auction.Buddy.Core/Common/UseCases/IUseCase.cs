using System;
using System.Threading.Tasks;

namespace Harvest.Home.Core.Common.UseCases
{
    public interface IUseCase<TInput, TOutput>
    {
        Task<UseCaseResponse<TInput, TOutput>> ExecuteAsync(UseCaseRequest<TInput, TOutput> request);
    }

    public class UseCaseRequest<TInput, TOutput>
    {
        public Guid Id { get; }
        public TInput Input { get; }

        public UseCaseRequest(TInput input)
        {
            Id = Guid.NewGuid();
            Input = input;
        }

        public UseCaseResponse<TInput, TOutput> CreateResponse(TOutput output)
        {
            return new UseCaseResponse<TInput, TOutput>(this, output);
        }
    }

    public class UseCaseResponse<TInput, TOutput>
    {
        public UseCaseRequest<TInput, TOutput> Request { get; }
        public TOutput Output { get; }

        public UseCaseResponse(UseCaseRequest<TInput,TOutput> request, TOutput output)
        {
            Request = request;
            Output = output;
        }
    }
}