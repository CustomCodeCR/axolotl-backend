using AutoMapper;
using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using WatchDog;
using Entity = Axolotl.Domain.Entities;

namespace Axolotl.Application.UseCases.Category.Commands.UpdateCommand;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var category = _mapper.Map<Entity.Category>(request);
            category.UUID = request.CategoryId;
            _unitOfWork.Category.UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}