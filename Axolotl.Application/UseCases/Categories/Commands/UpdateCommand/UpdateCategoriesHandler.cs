using AutoMapper;
using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using WatchDog;
using Entity = Axolotl.Domain.Entities;

namespace Axolotl.Application.UseCases.Categories.Commands.UpdateCommand;

public class UpdateCategoriesHandler : IRequestHandler<UpdateCategoriesCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateCategoriesCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var category = _mapper.Map<Entity.Categories>(request);
            category.UUID = request.CategoriesId;
            _unitOfWork.Categories.UpdateAsync(category);
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