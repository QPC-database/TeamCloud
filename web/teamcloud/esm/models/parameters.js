/*
 * Copyright (c) Microsoft Corporation.
 * Licensed under the MIT License.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator.
 * Changes may cause incorrect behavior and will be lost if the code is regenerated.
 */
import { ComponentDefinition as ComponentDefinitionMapper, ComponentTaskDefinition as ComponentTaskDefinitionMapper, DeploymentScopeDefinition as DeploymentScopeDefinitionMapper, DeploymentScope as DeploymentScopeMapper, OrganizationDefinition as OrganizationDefinitionMapper, UserDefinition as UserDefinitionMapper, User as UserMapper, ProjectDefinition as ProjectDefinitionMapper, ProjectIdentityDefinition as ProjectIdentityDefinitionMapper, ProjectIdentity as ProjectIdentityMapper, ProjectTemplateDefinition as ProjectTemplateDefinitionMapper, ProjectTemplate as ProjectTemplateMapper, ScheduleDefinition as ScheduleDefinitionMapper } from "../models/mappers";
export var accept = {
    parameterPath: "accept",
    mapper: {
        defaultValue: "application/json",
        isConstant: true,
        serializedName: "Accept",
        type: {
            name: "String"
        }
    }
};
export var $host = {
    parameterPath: "$host",
    mapper: {
        serializedName: "$host",
        required: true,
        type: {
            name: "String"
        }
    },
    skipEncoding: true
};
export var deleted = {
    parameterPath: ["options", "deleted"],
    mapper: {
        serializedName: "deleted",
        type: {
            name: "Boolean"
        }
    }
};
export var organizationId = {
    parameterPath: "organizationId",
    mapper: {
        serializedName: "organizationId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var projectId = {
    parameterPath: "projectId",
    mapper: {
        serializedName: "projectId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var contentType = {
    parameterPath: ["options", "contentType"],
    mapper: {
        defaultValue: "application/json",
        isConstant: true,
        serializedName: "Content-Type",
        type: {
            name: "String"
        }
    }
};
export var body = {
    parameterPath: ["options", "body"],
    mapper: ComponentDefinitionMapper
};
export var componentId = {
    parameterPath: "componentId",
    mapper: {
        serializedName: "componentId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body1 = {
    parameterPath: ["options", "body"],
    mapper: ComponentTaskDefinitionMapper
};
export var id = {
    parameterPath: "id",
    mapper: {
        serializedName: "id",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body2 = {
    parameterPath: ["options", "body"],
    mapper: DeploymentScopeDefinitionMapper
};
export var deploymentScopeId = {
    parameterPath: "deploymentScopeId",
    mapper: {
        serializedName: "deploymentScopeId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body3 = {
    parameterPath: ["options", "body"],
    mapper: DeploymentScopeMapper
};
export var contentType1 = {
    parameterPath: ["options", "contentType"],
    mapper: {
        defaultValue: "application/json-patch+json",
        isConstant: true,
        serializedName: "Content-Type",
        type: {
            name: "String"
        }
    }
};
export var body4 = {
    parameterPath: ["options", "body"],
    mapper: OrganizationDefinitionMapper
};
export var body5 = {
    parameterPath: ["options", "body"],
    mapper: UserDefinitionMapper
};
export var userId = {
    parameterPath: "userId",
    mapper: {
        serializedName: "userId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body6 = {
    parameterPath: ["options", "body"],
    mapper: UserMapper
};
export var body7 = {
    parameterPath: ["options", "body"],
    mapper: ProjectDefinitionMapper
};
export var body8 = {
    parameterPath: ["options", "body"],
    mapper: ProjectIdentityDefinitionMapper
};
export var projectIdentityId = {
    parameterPath: "projectIdentityId",
    mapper: {
        serializedName: "projectIdentityId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body9 = {
    parameterPath: ["options", "body"],
    mapper: ProjectIdentityMapper
};
export var body10 = {
    parameterPath: ["options", "body"],
    mapper: {
        serializedName: "body",
        type: {
            name: "Dictionary",
            value: { type: { name: "String" } }
        }
    }
};
export var tagKey = {
    parameterPath: "tagKey",
    mapper: {
        serializedName: "tagKey",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body11 = {
    parameterPath: ["options", "body"],
    mapper: ProjectTemplateDefinitionMapper
};
export var projectTemplateId = {
    parameterPath: "projectTemplateId",
    mapper: {
        serializedName: "projectTemplateId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var body12 = {
    parameterPath: ["options", "body"],
    mapper: ProjectTemplateMapper
};
export var body13 = {
    parameterPath: ["options", "body"],
    mapper: ScheduleDefinitionMapper
};
export var scheduleId = {
    parameterPath: "scheduleId",
    mapper: {
        serializedName: "scheduleId",
        required: true,
        type: {
            name: "String"
        }
    }
};
export var trackingId = {
    parameterPath: "trackingId",
    mapper: {
        serializedName: "trackingId",
        required: true,
        type: {
            name: "Uuid"
        }
    }
};
//# sourceMappingURL=parameters.js.map