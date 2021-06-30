// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

import { useMutation, useQueryClient } from 'react-query'
import { ErrorResult, ProjectTemplateDefinition } from 'teamcloud';
import { api } from '../API';
import { useOrg } from '.';

export const useCreateProjectTemplate = () => {

    const { data: org } = useOrg();

    const queryClient = useQueryClient();

    return useMutation(async (templateDef: ProjectTemplateDefinition) => {
        if (!org) throw Error('No Org');

        const { data, code, _response } = await api.createProjectTemplate(org.id, { body: templateDef });

        if (code && code >= 400) {
            const error = JSON.parse(_response.bodyAsText) as ErrorResult;
            throw error;
        }

        return data;
    }, {
        onSuccess: data => {
            if (data)
                queryClient.setQueryData(['org', org!.id, 'templates'], [data]);
        }
    }).mutateAsync
}
