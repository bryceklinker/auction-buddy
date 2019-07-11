import * as React from 'react';

interface Props {
    children: JSX.Element | JSX.Element[];
}

export function MainContent({ children }: Props) {
    return <div>{children}</div>;
}
