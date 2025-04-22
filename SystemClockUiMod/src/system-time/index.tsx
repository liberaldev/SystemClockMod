import React, {useEffect, useState, useRef} from "react";
import styles from "./system-time.module.css";
import {ModuleRegistryExtend} from "cs2/modding";
import {bindValue, useValue} from "cs2/api";
import mod from "../../mod.json"


export const SystemTime : ModuleRegistryExtend = (Component) => {
    const getFormattedSystemTime = bindValue<string>(mod.id, "GetFormattedSystemTime")
    return (props) => {
        const { children, ...otherProps } = props || {};

        const getFormattedSystemTimeValue = useValue(getFormattedSystemTime)
        const [formattedSystemTime, setFormattedSystemTime] = useState(useValue(getFormattedSystemTime));
        
        useEffect(() => {
            setFormattedSystemTime(getFormattedSystemTimeValue);
            console.log(formattedSystemTime)
        }, [getFormattedSystemTimeValue]);

        return (
            <>
                <div className={"field_eKJ"}>
                    <div className={"container_kOI container_MC3"}>
                        <div className={"label_qsp label_mWz content_syM"}>
                            {formattedSystemTime}
                        </div>
                    </div>
                </div>
                <Component {...otherProps}></Component>
            </>
        );
    };
}