/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamCloud.API.Auth;
using TeamCloud.API.Controllers.Core;
using TeamCloud.API.Data;
using TeamCloud.API.Data.Results;
using TeamCloud.API.Services;
using TeamCloud.Data;
using TeamCloud.Model.Commands;
using TeamCloud.Model.Data;
using TeamCloud.Model.Validation;

namespace TeamCloud.API.Controllers
{
    [ApiController]
    [Route("orgs/{organizationId:organizationId}/projects/{projectId:projectId}/identities")]
    [Produces("application/json")]
    public class ProjectIdentitiesController : TeamCloudController
    {
        private readonly IProjectIdentityRepository projectIdentityRepository;

        public ProjectIdentitiesController(IProjectIdentityRepository projectIdentityRepository) : base()
        {
            this.projectIdentityRepository = projectIdentityRepository ?? throw new ArgumentNullException(nameof(projectIdentityRepository));
        }


        [HttpGet]
        [Authorize(Policy = AuthPolicies.ProjectMember)]
        [SwaggerOperation(OperationId = "GetProjectIdentities", Summary = "Gets all Project Identities.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all Project Identities.", typeof(DataResult<List<ProjectIdentity>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        public Task<IActionResult> Get() => ExecuteAsync<TeamCloudProjectContext>(async context =>
        {
            var identities = await projectIdentityRepository
                .ListAsync(context.Project.Id)
                .ToListAsync()
                .ConfigureAwait(false);

            return DataResult<List<ProjectIdentity>>
                .Ok(identities)
                .ToActionResult();
        });


        [HttpGet("{projectIdentityId}")]
        [Authorize(Policy = AuthPolicies.ProjectMember)]
        [SwaggerOperation(OperationId = "GetProjectIdentity", Summary = "Gets a Project Identity.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a ProjectIdentity.", typeof(DataResult<ProjectIdentity>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A ProjectIdentity with the projectIdentityId provided was not found.", typeof(ErrorResult))]
        [SuppressMessage("Usage", "CA1801: Review unused parameters", Justification = "Used by base class and makes signiture unique")]
        public Task<IActionResult> Get(string projectIdentityId) => ExecuteAsync<TeamCloudProjectContext>(async context =>
        {
            var projectIdentity = await projectIdentityRepository
                .GetAsync(context.Project.Id, projectIdentityId)
                .ConfigureAwait(false);

            return DataResult<ProjectIdentity>
                .Ok(projectIdentity)
                .ToActionResult();
        });


        [HttpPost]
        [Authorize(Policy = AuthPolicies.ProjectMember)]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "CreateProjectIdentity", Summary = "Creates a new Project Identity.")]
        [SwaggerResponse(StatusCodes.Status201Created, "The new Project Identity was created.", typeof(DataResult<ProjectIdentity>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status409Conflict, "A Project Identity already exists with the ID provided in the request body.", typeof(ErrorResult))]
        public Task<IActionResult> Post([FromBody] ProjectIdentityDefinition projectIdentityDefinition) => ExecuteAsync<TeamCloudProjectContext>(async context =>
        {
            if (projectIdentityDefinition is null)
                return ErrorResult
                    .BadRequest("Request body must not be empty.", ResultErrorCode.ValidationError)
                    .ToActionResult();

            var projectIdentity = new ProjectIdentity
            {
                Id = Guid.NewGuid().ToString(),
                Organization = context.Organization.Id,
                ProjectId = context.Project.Id,
                DisplayName = projectIdentityDefinition.DisplayName,
                DeploymentScopeId = projectIdentityDefinition.DeploymentScopeId
            };

            var command = new ProjectIdentityCreateCommand(context.ContextUser, projectIdentity);

            return await Orchestrator
                .InvokeAndReturnActionResultAsync(command, Request)
                .ConfigureAwait(false);
        });


        [HttpPut("{projectIdentityId}")]
        [Authorize(Policy = AuthPolicies.ProjectMember)]
        [Consumes("application/json")]
        [SwaggerOperation(OperationId = "UpdateProjectIdentity", Summary = "Updates an existing Project Identity.")]
        [SwaggerResponse(StatusCodes.Status202Accepted, "Starts updating the Project Identity. Returns a StatusResult object that can be used to track progress of the long-running operation.", typeof(StatusResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Project with the provided projectId was not found, or a Identity with the key provided in the request body was not found.", typeof(ErrorResult))]
        public Task<IActionResult> Put([FromRoute] string projectIdentityId, [FromBody] ProjectIdentity projectIdentity) => ExecuteAsync<TeamCloudProjectIdentityContext>(async context =>
        {
            if (projectIdentity is null)
                throw new ArgumentNullException(nameof(projectIdentity));

            var validation = await projectIdentity
                .ValidateAsync()
                .ConfigureAwait(false);

            if (!validation.IsValid)
                return ErrorResult
                    .BadRequest(validation)
                    .ToActionResult();

            if (!projectIdentity.Id.Equals(projectIdentityId, StringComparison.Ordinal))
                return ErrorResult
                    .BadRequest(new ValidationError { Field = "id", Message = $"Project Identity's id does match the identifier provided in the path." })
                    .ToActionResult();

            context.ProjectIdentity.RedirectUrls = projectIdentity.RedirectUrls;

            var command = new ProjectIdentityUpdateCommand(context.ContextUser, context.ProjectIdentity);

            return await Orchestrator
                .InvokeAndReturnActionResultAsync(command, Request)
                .ConfigureAwait(false);
        });


        [HttpDelete("{projectIdentityId}")]
        [Authorize(Policy = AuthPolicies.ProjectMember)]
        [SwaggerOperation(OperationId = "DeleteProjectIdentity", Summary = "Deletes a Project Identity.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The ProjectIdentity was deleted.", typeof(DataResult<ProjectIdentity>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "A validation error occured.", typeof(ErrorResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A ProjectIdentity with the projectIdentityId provided was not found.", typeof(ErrorResult))]
        [SuppressMessage("Usage", "CA1801: Review unused parameters", Justification = "Used by base class and makes signiture unique")]
        public Task<IActionResult> Delete([FromRoute] string projectIdentityId) => ExecuteAsync<TeamCloudProjectIdentityContext>(async context =>
        {
            var command = new ProjectIdentityDeleteCommand(context.ContextUser, context.ProjectIdentity);

            return await Orchestrator
                .InvokeAndReturnActionResultAsync(command, Request)
                .ConfigureAwait(false);
        });
    }
}
