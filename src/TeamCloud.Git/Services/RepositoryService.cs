/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamCloud.Git.Caching;
using TeamCloud.Model.Data;

namespace TeamCloud.Git.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly GitHubService github;
        private readonly DevOpsService devops;

        public RepositoryService(IRepositoryCache cache)
        {
            github = new GitHubService(cache);
            devops = new DevOpsService();
        }

        public Task<RepositoryReference> GetRepositoryReferenceAsync(string url, string version, string token)
            => GetRepositoryReferenceAsync(new RepositoryReference { Url = url, Token = token, Version = version });

        public Task<RepositoryReference> GetRepositoryReferenceAsync(RepositoryReference repository)
            => GetRepositoryReferenceInternalAsync(repository.ParseUrl());

        private Task<RepositoryReference> GetRepositoryReferenceInternalAsync(RepositoryReference repository) => repository?.Provider switch
        {
            RepositoryProvider.DevOps => DevOpsService.GetRepositoryReferenceAsync(repository),
            RepositoryProvider.GitHub => github.GetRepositoryReferenceAsync(repository),
            _ => throw new NotSupportedException("Only GitHub and Azure DevOps git repositories are supported. Generic git repositories are not supported.")
        } ?? throw new ArgumentNullException(nameof(repository));

        public async Task<ProjectTemplate> UpdateProjectTemplateAsync(ProjectTemplate projectTemplate)
        {
            if (projectTemplate is null)
                throw new ArgumentNullException(nameof(projectTemplate));

            if (projectTemplate.Repository.Provider == RepositoryProvider.Unknown || projectTemplate.Repository.Type == RepositoryReferenceType.Branch)
                projectTemplate.Repository = await GetRepositoryReferenceAsync(projectTemplate.Repository)
                    .ConfigureAwait(false);

            return await UpdateProjectTemplateInternalAsync(projectTemplate)
                .ConfigureAwait(false);
        }

        private Task<ProjectTemplate> UpdateProjectTemplateInternalAsync(ProjectTemplate projectTemplate) => projectTemplate?.Repository?.Provider switch
        {
            RepositoryProvider.DevOps => devops.UpdateProjectTemplateAsync(projectTemplate),
            RepositoryProvider.GitHub => github.UpdateProjectTemplateAsync(projectTemplate),
            _ => throw new NotSupportedException("Only GitHub and Azure DevOps git repositories are supported. Generic git repositories are not supported.")
        } ?? throw new ArgumentNullException(nameof(projectTemplate));


        public Task<ComponentTemplate> GetComponentTemplateAsync(ProjectTemplate projectTemplate, string templateId) => projectTemplate?.Repository?.Provider switch
        {
            RepositoryProvider.DevOps => devops.GetComponentTemplateAsync(projectTemplate, templateId),
            RepositoryProvider.GitHub => github.GetComponentTemplateAsync(projectTemplate, templateId),
            _ => throw new NotSupportedException("Only GitHub and Azure DevOps git repositories are supported. Generic git repositories are not supported.")
        } ?? throw new ArgumentNullException(nameof(projectTemplate));

        public IAsyncEnumerable<ComponentTemplate> GetComponentTemplatesAsync(ProjectTemplate projectTemplate) => projectTemplate?.Repository?.Provider switch
        {
            RepositoryProvider.DevOps => devops.GetComponentTemplatesAsync(projectTemplate),
            RepositoryProvider.GitHub => github.GetComponentTemplatesAsync(projectTemplate),
            _ => throw new NotSupportedException("Only GitHub and Azure DevOps git repositories are supported. Generic git repositories are not supported.")
        } ?? throw new ArgumentNullException(nameof(projectTemplate));

    }
}
