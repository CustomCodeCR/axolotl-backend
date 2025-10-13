using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using WatchDog;

namespace Axolotl.Application.UseCases.Categories.Commands.DeleteCommand;

public class DeleteCategoriesHandler : IRequestHandler<DeleteCategoriesCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteCategoriesCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsCategory = await _unitOfWork.Categories.GetByUUIDAsync(request.CategoriesId);

            if (existsCategory is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Categories.DeleteAsync(request.CategoriesId);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}