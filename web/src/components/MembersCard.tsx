// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

import React, { useState } from 'react';
import { ICommandBarItemProps } from '@fluentui/react';
import { ErrorResult } from 'teamcloud';
import { api } from '../API';
import { Member, ProjectMember } from '../model'
import { DetailCard, MembersForm, MemberFacepile } from '.';
import { useAddProjectMembers, useGraphUser, useProjectMembers } from '../hooks';

export interface IMembersCardProps { }

export const MembersCard: React.FC<IMembersCardProps> = (props) => {

    const [addMembersPanelOpen, setAddMembersPanelOpen] = useState(false);

    const { data: graphUser } = useGraphUser();

    const { data: members } = useProjectMembers();

    const addMembers = useAddProjectMembers();

    const _removeMember = async (member: Member) => {
        const projectId = (member as ProjectMember)?.projectMembership?.projectId;
        if (projectId) {
            const result = await api.deleteProjectUser(member.user.id, member.user.organization, projectId);
            if (result.code !== 204 && (result as ErrorResult).errors) {
                console.error(result as ErrorResult);
            }
        } else {
            console.log('Deleting a user from a Org is not implemented yet.')
        }
    };

    const _isAdmin = () => {
        const member = graphUser && members?.find(m => m.user.id === graphUser.id);
        if (!member) return false;
        const role = (member as ProjectMember)?.projectMembership?.role ?? member.user.role;
        return role.toLowerCase() === 'owner' || role.toLowerCase() === 'admim';
    };

    const _getCommandBarItems = (): ICommandBarItemProps[] => [
        { key: 'addUser', text: 'Add', iconProps: { iconName: 'PeopleAdd' }, onClick: () => setAddMembersPanelOpen(true), disabled: !_isAdmin() },
    ];

    return (
        <>
            <DetailCard
                title='Members'
                callout={members?.filter(m => m.graphPrincipal)?.length}
                commandBarItems={_getCommandBarItems()}>
                <MemberFacepile
                    members={members}
                    onRemoveMember={_removeMember} />
            </DetailCard>
            <MembersForm
                members={members}
                panelIsOpen={addMembersPanelOpen}
                onFormClose={() => setAddMembersPanelOpen(false)}
                addMembers={addMembers} />
        </>
    );
}
