import * as React from "react";

interface Props {
    children: any
}

export function MainContent({children}: Props) {
    return (
        <div>
            {children}
        </div>
    )
}