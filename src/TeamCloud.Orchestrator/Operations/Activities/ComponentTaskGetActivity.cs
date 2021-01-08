/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using TeamCloud.Data;
using TeamCloud.Model.Data;

namespace TeamCloud.Orchestrator.Operations.Activities
{
    public sealed class ComponentTaskGetActivity
    {
        private readonly IComponentTaskRepository componentTaskRepository;
        private readonly IComponentRepository componentRepository;

        public ComponentTaskGetActivity(IComponentTaskRepository componentTaskRepository, IComponentRepository componentRepository)
        {
            this.componentTaskRepository = componentTaskRepository ?? throw new ArgumentNullException(nameof(componentTaskRepository));
            this.componentRepository = componentRepository ?? throw new ArgumentNullException(nameof(componentRepository));
        }

        [FunctionName(nameof(ComponentTaskGetActivity))]
        public async Task<ComponentTask> Run(
            [ActivityTrigger] IDurableActivityContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var input = context.GetInput<Input>();

            var componentTask = await componentTaskRepository
                .GetAsync(input.ComponentId, input.ComponentTaskId)
                .ConfigureAwait(false);

            if (string.IsNullOrEmpty(componentTask.StorageId))
            {
                var component = await componentRepository
                    .GetAsync(componentTask.ProjectId, componentTask.ComponentId)
                    .ConfigureAwait(false);

                componentTask.StorageId = component.StorageId;
            }

            return componentTask;
        }

        internal struct Input
        {
            public string ComponentTaskId { get; set; }

            public string ComponentId { get; set; }
        }
    }
}