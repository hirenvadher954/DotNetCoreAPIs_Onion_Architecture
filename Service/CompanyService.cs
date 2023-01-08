using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        try
        {
            IEnumerable<Company> companies = _repository.Company.GetAllCompanies(trackChanges);
            IEnumerable<CompanyDto>? companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} action {ex}");
            throw;
        }
    }

    public CompanyDto GetCompany(Guid id, bool trackChanges)
    {
        Company company = _repository.Company.GetCompany(id, trackChanges);
        
        if (company is null)
            throw new CompanyNotFoundException(id);
        CompanyDto? companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }

    public CompanyDto CreateCompany(CompanyForCreationDto company)
    {
        Company? companyEntity = _mapper.Map<Company>(company);
        _repository.Company.CreateCompany(companyEntity);
        _repository.Save();
        CompanyDto? companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
    }
}