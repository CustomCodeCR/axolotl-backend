using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using MediatR;
using WatchDog;
using ProductPriceHistoryEntity = Axolotl.Domain.Entities.ProductPriceHistory;

namespace Axolotl.Application.UseCases.ProductPriceHistory.Commands.ChangePrice
{
    public sealed class ChangePriceCommandHandler
        : IRequestHandler<ChangePriceCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangePriceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<bool>> Handle(ChangePriceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();

            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var dto = request.Dto;

                var last = await _unitOfWork.ProductPriceHistory.GetLastAsync(dto.ProductId, cancellationToken);
                if (last is not null && last.ValidTo == DateTime.MaxValue && last.ValidFrom < dto.ValidFrom)
                {
                    last.ValidTo = dto.ValidFrom.AddTicks(-1);
                    await _unitOfWork.ProductPriceHistory.UpdateAsync(last, cancellationToken);
                    await _unitOfWork.SaveChangesAsync();
                }

                var row = new ProductPriceHistoryEntity
                {
                    ProductId = dto.ProductId,
                    ValidFrom = dto.ValidFrom,
                    ValidTo = DateTime.MaxValue,
                    Price = dto.Price,
                    Cost = dto.Cost,
                    Note = dto.Note
                };

                await _unitOfWork.ProductPriceHistory.AddAsync(row, cancellationToken);
                await _unitOfWork.SaveChangesAsync();

                transaction.Commit();

                response.IsSuccess = true;
                response.Data = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                response.IsSuccess = false;
                response.Data = false;
                response.Message = ex.Message;
                WatchLogger.LogError(ex.Message);
            }

            return response;
        }
    }
}
