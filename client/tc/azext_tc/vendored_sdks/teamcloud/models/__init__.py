# coding=utf-8
# --------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. See License.txt in the project root for license information.
# Code generated by Microsoft (R) AutoRest Code Generator.
# Changes may cause incorrect behavior and will be lost if the code is regenerated.
# --------------------------------------------------------------------------

try:
    from ._models_py3 import AzureResourceGroup
    from ._models_py3 import Component
    from ._models_py3 import ComponentDataResult
    from ._models_py3 import ComponentListDataResult
    from ._models_py3 import ComponentOffer
    from ._models_py3 import ComponentOfferDataResult
    from ._models_py3 import ComponentOfferListDataResult
    from ._models_py3 import ComponentRequest
    from ._models_py3 import ErrorResult
    from ._models_py3 import Project
    from ._models_py3 import ProjectDataResult
    from ._models_py3 import ProjectDefinition
    from ._models_py3 import ProjectIdentity
    from ._models_py3 import ProjectIdentityDataResult
    from ._models_py3 import ProjectLink
    from ._models_py3 import ProjectLinkDataResult
    from ._models_py3 import ProjectListDataResult
    from ._models_py3 import ProjectMembership
    from ._models_py3 import ProjectReferenceLinks
    from ._models_py3 import ProjectType
    from ._models_py3 import ProjectTypeDataResult
    from ._models_py3 import ProjectTypeListDataResult
    from ._models_py3 import Provider
    from ._models_py3 import ProviderData
    from ._models_py3 import ProviderDataListDataResult
    from ._models_py3 import ProviderDataResult
    from ._models_py3 import ProviderDataReturnResult
    from ._models_py3 import ProviderEventSubscription
    from ._models_py3 import ProviderListDataResult
    from ._models_py3 import ProviderReference
    from ._models_py3 import ReferenceLink
    from ._models_py3 import ResultError
    from ._models_py3 import StatusResult
    from ._models_py3 import StringDictionaryDataResult
    from ._models_py3 import TeamCloudApplication
    from ._models_py3 import TeamCloudInstance
    from ._models_py3 import TeamCloudInstanceDataResult
    from ._models_py3 import User
    from ._models_py3 import UserDataResult
    from ._models_py3 import UserDefinition
    from ._models_py3 import UserListDataResult
    from ._models_py3 import ValidationError
except (SyntaxError, ImportError):
    from ._models import AzureResourceGroup  # type: ignore
    from ._models import Component  # type: ignore
    from ._models import ComponentDataResult  # type: ignore
    from ._models import ComponentListDataResult  # type: ignore
    from ._models import ComponentOffer  # type: ignore
    from ._models import ComponentOfferDataResult  # type: ignore
    from ._models import ComponentOfferListDataResult  # type: ignore
    from ._models import ComponentRequest  # type: ignore
    from ._models import ErrorResult  # type: ignore
    from ._models import Project  # type: ignore
    from ._models import ProjectDataResult  # type: ignore
    from ._models import ProjectDefinition  # type: ignore
    from ._models import ProjectIdentity  # type: ignore
    from ._models import ProjectIdentityDataResult  # type: ignore
    from ._models import ProjectLink  # type: ignore
    from ._models import ProjectLinkDataResult  # type: ignore
    from ._models import ProjectListDataResult  # type: ignore
    from ._models import ProjectMembership  # type: ignore
    from ._models import ProjectReferenceLinks  # type: ignore
    from ._models import ProjectType  # type: ignore
    from ._models import ProjectTypeDataResult  # type: ignore
    from ._models import ProjectTypeListDataResult  # type: ignore
    from ._models import Provider  # type: ignore
    from ._models import ProviderData  # type: ignore
    from ._models import ProviderDataListDataResult  # type: ignore
    from ._models import ProviderDataResult  # type: ignore
    from ._models import ProviderDataReturnResult  # type: ignore
    from ._models import ProviderEventSubscription  # type: ignore
    from ._models import ProviderListDataResult  # type: ignore
    from ._models import ProviderReference  # type: ignore
    from ._models import ReferenceLink  # type: ignore
    from ._models import ResultError  # type: ignore
    from ._models import StatusResult  # type: ignore
    from ._models import StringDictionaryDataResult  # type: ignore
    from ._models import TeamCloudApplication  # type: ignore
    from ._models import TeamCloudInstance  # type: ignore
    from ._models import TeamCloudInstanceDataResult  # type: ignore
    from ._models import User  # type: ignore
    from ._models import UserDataResult  # type: ignore
    from ._models import UserDefinition  # type: ignore
    from ._models import UserListDataResult  # type: ignore
    from ._models import ValidationError  # type: ignore

from ._team_cloud_client_enums import (
    ComponentOfferScope,
    ComponentOfferType,
    ComponentScope,
    ComponentType,
    ProjectLinkType,
    ProjectMembershipRole,
    ProviderCommandMode,
    ProviderDataScope,
    ProviderDataType,
    ProviderType,
    ResultErrorCode,
    UserRole,
    UserType,
)

__all__ = [
    'AzureResourceGroup',
    'Component',
    'ComponentDataResult',
    'ComponentListDataResult',
    'ComponentOffer',
    'ComponentOfferDataResult',
    'ComponentOfferListDataResult',
    'ComponentRequest',
    'ErrorResult',
    'Project',
    'ProjectDataResult',
    'ProjectDefinition',
    'ProjectIdentity',
    'ProjectIdentityDataResult',
    'ProjectLink',
    'ProjectLinkDataResult',
    'ProjectListDataResult',
    'ProjectMembership',
    'ProjectReferenceLinks',
    'ProjectType',
    'ProjectTypeDataResult',
    'ProjectTypeListDataResult',
    'Provider',
    'ProviderData',
    'ProviderDataListDataResult',
    'ProviderDataResult',
    'ProviderDataReturnResult',
    'ProviderEventSubscription',
    'ProviderListDataResult',
    'ProviderReference',
    'ReferenceLink',
    'ResultError',
    'StatusResult',
    'StringDictionaryDataResult',
    'TeamCloudApplication',
    'TeamCloudInstance',
    'TeamCloudInstanceDataResult',
    'User',
    'UserDataResult',
    'UserDefinition',
    'UserListDataResult',
    'ValidationError',
    'ComponentOfferScope',
    'ComponentOfferType',
    'ComponentScope',
    'ComponentType',
    'ProjectLinkType',
    'ProjectMembershipRole',
    'ProviderCommandMode',
    'ProviderDataScope',
    'ProviderDataType',
    'ProviderType',
    'ResultErrorCode',
    'UserRole',
    'UserType',
]
