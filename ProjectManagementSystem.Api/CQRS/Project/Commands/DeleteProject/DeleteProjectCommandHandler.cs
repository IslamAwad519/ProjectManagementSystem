﻿using AutoMapper;
using MediatR;
using ProjectManagementSystem.Api.Repositories.Interfaces;

namespace ProjectManagementSystem.Api.CQRS.Project.Commands.DeleteProject;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IRepository<Models.Project> _projectRepository;  // Use an abstraction for your data access
    private readonly IRepository<Models.User> _userRepository;
    private readonly IMapper _mapper;


    public DeleteProjectCommandHandler(IRepository<Models.Project> projectRepository, IRepository<Models.User> userRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project =  _projectRepository.GetByID(request.ProjectId);
        if (project == null)
        {
            return false;
        }

        _projectRepository.Delete(project);
        _projectRepository.SaveChanges();

        return true;
    }
}

