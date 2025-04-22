import React, {useEffect, useState, useRef} from "react";
import styles from "./system-time.module.css";
import {ModuleRegistryExtend} from "cs2/modding";
import {bindValue, useValue} from "cs2/api";
import mod from "../../mod.json"


export const SystemTime : ModuleRegistryExtend = (Component) => {
    const getSystemTime = bindValue<string>(mod.id, "GetSystemTime")
    return (props) => {
        const { children, ...otherProps } = props || {};

        const getSystemTimeValue = useValue(getSystemTime)
        const [systemTime, setSystemTime] = useState(useValue(getSystemTime));
        
        useEffect(() => {
            setSystemTime(getSystemTimeValue);
            console.log(systemTime)
        }, [getSystemTimeValue]);

        return (
            <>
                <div className={"field_eKJ"}>
                    <div className={"container_kOI container_MC3"}>
                        <div className={"label_qsp label_mWz content_syM"}>
                            {systemTime}
                        </div>
                    </div>
                </div>
                <Component {...otherProps}></Component>
            </>
        );
    };
}