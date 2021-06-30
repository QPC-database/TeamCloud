﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TeamCloud.Adapters;
using TeamCloud.Model.Data;

namespace TeamCloud.Data.Expanders
{
    public sealed class DeploymentScopeExpander : DocumentExpander,
        IDocumentExpander<DeploymentScope>
    {
        private readonly IEnumerable<IAdapter> adapters;

        public DeploymentScopeExpander(IEnumerable<IAdapter> adapters) : base(false)
        {
            this.adapters = adapters ?? Enumerable.Empty<IAdapter>();
        }

        public async Task<DeploymentScope> ExpandAsync(DeploymentScope document)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));

            if (adapters.TryGetAdapter(document.Type, out var adapter))
            {
                document.InputDataSchema = await adapter
                    .GetInputDataSchemaAsync()
                    .ConfigureAwait(false);

                document.ComponentTypes = adapter.ComponentTypes.ToList();

                if (adapter is IAdapterAuthorize adapterAuthorize)
                {
                    document.Authorizable = true;
                    document.Authorized = await adapterAuthorize
                        .IsAuthorizedAsync(document)
                        .ConfigureAwait(false);
                }
                else
                {
                    document.Authorizable = false;
                    document.Authorized = !(adapter is null);
                }

                if (document.Type == DeploymentScopeType.AzureResourceManager && !string.IsNullOrWhiteSpace(document.InputData))
                {
                    // TODO: remove this special handling for AzureResourceManager deployment scopes
                    // when adapters are fully implemented and ManagementGroupId and SubscriptionIds are gone.

                    var inputData = JObject.Parse(document.InputData);

                    document.ManagementGroupId = inputData.SelectToken("$..managementGroupId")?.ToString();
                    document.SubscriptionIds = (inputData.SelectToken("$..subscriptionIds") as JArray)?.Select(t => Guid.Parse(t.ToString())).ToList() ?? new List<Guid>();
                }
            }

            return document;
        }
    }
}
