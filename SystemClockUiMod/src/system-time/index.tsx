import React, {useEffect, useState, useRef} from "react";
import styles from "./system-time.module.css";
import {ModuleRegistryExtend} from "cs2/modding";
import {bindValue, useValue} from "cs2/api";
import mod from "../../mod.json"


export const SystemTime : ModuleRegistryExtend = (Component) => {
    const getTime = bindValue<string>(mod.id, "GetTime")
    return (props) => {
        const { children, ...otherProps } = props || {};

        const getTimeValue = useValue(getTime)
        const [time, setTime] = useState(useValue(getTime));
        
        useEffect(() => {
            setTime(getTimeValue);
            console.log(time)
        }, [getTimeValue]);

        return (
            <>
                <div className={"field_eKJ"}>
                    <div className={"container_kOI container_MC3"}>
                        <div className={"label_qsp label_mWz content_syM"}>
                            {time}
                        </div>
                    </div>
                </div>
                <Component {...otherProps}></Component>
            </>
        );
    };
}