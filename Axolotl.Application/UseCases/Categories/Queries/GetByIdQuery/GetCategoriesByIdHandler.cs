using AutoMapper;
using MediatR;
using WatchDog;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Categories.Response;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using Axolotl.Application.UseCases.Categories.Queries.GetByIdQuery;

namespace Axolotl.Application.UseCases.Categories.Queries.GetByIdQuery;

public class GetCategoriesByIdHandler : IRequestHandler<GetCategoriesByIdQuery, BaseResponse<CategoriesByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoriesByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CategoriesByIdResponseDto>> Handle(GetCategoriesByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CategoriesByIdResponseDto>();

        try
        {
            var category = await _unitOfWork.Categories.GetByUUIDAsync(request.CategoriesId);

            if (category is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<CategoriesByIdResponseDto>(category);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}