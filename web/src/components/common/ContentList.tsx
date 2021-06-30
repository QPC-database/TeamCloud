// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

import React, { PropsWithChildren, useState } from 'react';
import { CheckboxVisibility, DetailsList, DetailsListLayoutMode, getTheme, IColumn, IDetailsHeaderProps, IDetailsRowProps, IRenderFunction, PrimaryButton, SearchBox, Stack } from '@fluentui/react';
import { NoData, CalloutLabel } from '.';

export interface IContentListProps<T> {
    items?: T[];
    columns?: IColumn[];
    noCheck?: boolean;
    noHeader?: boolean;
    filterPlaceholder?: string;
    onRenderBeforeSearchBox?: () => JSX.Element;
    onRenderAfterSearchBox?: () => JSX.Element;
    applyFilter?: (item: T, filter: string) => boolean;
    onItemInvoked?: (item: T) => void;
    buttonText?: string;
    buttonIcon?: string;
    onButtonClick?: () => void;
    secondaryButtonText?: string;
    secondaryButtonIcon?: string;
    onSecondaryButtonClick?: () => void;
    noDataTitle?: string;
    noDataDescription?: string;
    noDataImage?: string;
    noDataButtonText?: string;
    noDataButtonIcon?: string;
    onNoDataButtonClick?: () => void;
}

export const ContentList = <T,>(props: PropsWithChildren<IContentListProps<T>>) => {

    const [itemFilter, setItemFilter] = useState<string>();

    const theme = getTheme();

    const _onRenderRow: IRenderFunction<IDetailsRowProps> = (rowProps?: IDetailsRowProps, defaultRender?: (rowProps?: IDetailsRowProps) => JSX.Element | null): JSX.Element | null => {
        if (rowProps) rowProps.styles = {
            root: { borderBottom: (props.noHeader ?? false) && items.length === 1 ? 0 : undefined },
            fields: { alignItems: 'center' }, check: { minHeight: '62px' }, cell: { fontSize: '14px' }
        }
        return defaultRender ? defaultRender(rowProps) : null;
    };

    const _onRenderDetailsHeader: IRenderFunction<IDetailsHeaderProps> = (headProps?: IDetailsHeaderProps, defaultRender?: (headProps?: IDetailsHeaderProps) => JSX.Element | null): JSX.Element | null => {
        if (headProps) headProps.styles = { root: { paddingTop: '8px' } }
        return defaultRender ? defaultRender(headProps) : null;
    };

    const items: T[] = props.items ? (itemFilter && props.applyFilter !== undefined) ? props.items.filter(i => props.applyFilter!(i, itemFilter)) : props.items : [];

    const _renderDataArea = (): JSX.Element => {

        if (props.items === undefined) {

            return (<></>);

        } else if (props.items.length === 0) {

            return (<NoData
                title={props.noDataTitle ?? 'No data'}
                image={props.noDataImage}
                description={props.noDataDescription}
                buttonText={props.noDataButtonText}
                buttonIcon={props.noDataButtonIcon}
                onButtonClick={props.onNoDataButtonClick} />);

        } else {

            return (<>
                {!(props.noHeader ?? false) && (
                    <Stack horizontal verticalFill verticalAlign='baseline' horizontalAlign='space-between'
                        styles={{ root: { padding: '16px 16px 0px 16px', } }}>
                        <Stack.Item>
                            <CalloutLabel title='Total' callout={props.items.length} />
                        </Stack.Item>
                        {props.buttonText && (
                            <Stack.Item>
                                <PrimaryButton
                                    text={props.buttonText}
                                    iconProps={props.buttonIcon ? { iconName: props.buttonIcon } : undefined}
                                    onClick={props.onButtonClick} />
                            </Stack.Item>
                        )}
                    </Stack>
                )}

                <DetailsList
                    items={items}
                    columns={props.columns}
                    styles={(props.noHeader ?? false) && items.length === 1 ? {
                        root: { padding: '2px 0px' },
                    } : undefined}
                    isHeaderVisible={!(props.noHeader ?? false)}
                    onRenderRow={_onRenderRow}
                    onRenderDetailsHeader={_onRenderDetailsHeader}
                    // selectionMode={SelectionMode.none}
                    layoutMode={DetailsListLayoutMode.justified}
                    checkboxVisibility={(props.noCheck ?? false) ? CheckboxVisibility.hidden : CheckboxVisibility.always}
                    selectionPreservedOnEmptyClick={true}
                    onItemInvoked={props.onItemInvoked} />
            </>);

        }
    };

    return (
        <Stack tokens={{ childrenGap: '20px' }}>
            { (props.applyFilter) && (
                <Stack tokens={{ childrenGap: '20px' }} horizontal styles={{
                    root: {
                        padding: '10px 16px 10px 0px',
                        borderRadius: theme.effects.roundedCorner4,
                        boxShadow: theme.effects.elevation4,
                        backgroundColor: theme.palette.white
                    }
                }} >
                    { props.onRenderBeforeSearchBox && (
                        <Stack.Item>
                            {props.onRenderBeforeSearchBox()}
                        </Stack.Item>
                    )}
                    <Stack.Item grow>
                        <SearchBox
                            placeholder={props.filterPlaceholder ?? 'Filter items'}
                            iconProps={{ iconName: 'Filter' }}
                            onChange={(_ev, val) => setItemFilter(val)}
                            styles={{
                                root: {
                                    border: 'none !important', selectors: {
                                        '::after': { border: 'none !important' },
                                        ':hover .ms-SearchBox-iconContainer': { color: theme.palette.neutralTertiary }
                                    }
                                },
                                iconContainer: { color: theme.palette.neutralTertiary, },
                                field: { border: 'none !important' }
                            }} />
                    </Stack.Item>
                    { props.onRenderAfterSearchBox && (
                        <Stack.Item>
                            {props.onRenderAfterSearchBox()}
                        </Stack.Item>
                    )}
                </Stack>
            )}
            <Stack styles={{
                root: {
                    borderRadius: theme.effects.roundedCorner4,
                    boxShadow: theme.effects.elevation4,
                    backgroundColor: theme.palette.white
                }
            }}>
                {_renderDataArea()}
            </Stack>
        </Stack>
    );
}
