import React, {useEffect, useState} from "react";
import {ModuleRegistryExtend} from "cs2/modding";
import {bindValue, useValue} from "cs2/api";
import mod from "../../mod.json"

export const SystemTime : ModuleRegistryExtend = (Component) => {
    const formattedSystemTime$ = bindValue<string>(mod.id, "FormattedSystemTime")
    const clockSize$ = bindValue<string>(mod.id, "ClockSize")

    type SizeMap = {
        [key: string]: string
        small: string
        medium: string
        large: string
    }
    const sizes: SizeMap = {
        small: "120rem",
        medium: "150rem",
        large: "240rem"
    }
    
    return (props) => {
        const { children, ...otherProps } = props || {};

        const formattedSystemTimeValue = useValue(formattedSystemTime$)
        const clockSizeValue = useValue(clockSize$);
        
        const [clock, setClock] = useState(useValue(formattedSystemTime$));
        
        useEffect(() => {
            setClock(formattedSystemTimeValue);
            console.log(clock)
            console.log(sizes[clockSizeValue.toLowerCase()])
        }, [formattedSystemTimeValue]);

        return (
            <>
                <div className={"field_eKJ"} style={{width: sizes[clockSizeValue.toLowerCase()]}}>
                    <div className={"container_kOI container_MC3"}>
                        <div className={"label_qsp label_mWz content_syM"}>
                            {clock}
                        </div>
                    </div>
                </div>
                <Component {...otherProps}>{children}</Component>
            </>
        );
    };
}