import React, {useEffect, useState, useRef} from "react";
import styles from "./system-time.module.css";
import {ModuleRegistryExtend} from "cs2/modding";
import {TimeFormat, useLocalization} from "cs2/l10n";


export const SystemTime : ModuleRegistryExtend = (Component) => {
    return (props) => {
        const { children, ...otherProps } = props || {};

        const { translate, unitSettings } = useLocalization();
        
        const [time, setTime] = useState("");
        const is12Hour = useRef(!(unitSettings.timeFormat == (0 satisfies TimeFormat.TwentyFourHours)));
        const localeKoKr = useRef<Element|null>()

        // 시간 포맷팅 함수
        const formatTime = (date: Date) => {
            let hours = date.getHours();
            const minutes = date.getMinutes();
            const seconds = date.getSeconds();

            // 12시간제인 경우
            let ampm: string | null = "";
            if (is12Hour.current) {
                ampm = hours >= 12 ? translate("SystemClock.PostMeridiem", "PM") :
                    translate("SystemClock.AnteMeridiem", "AM");
                hours = hours % 12;
                if (hours === 0) hours = 12; // 0시를 12시로
            }

            // 두 자리 숫자 포맷
            const pad = (num: number) => String(num).padStart(2, "0");

            return `${is12Hour.current && localeKoKr.current ? ampm + " " : ""}
            ${pad(hours)}:${pad(minutes)}:${pad(seconds)}
            ${is12Hour.current && !localeKoKr.current ?  " "  + ampm : ""}`;
        };

        useEffect(() => {
            const timer = setInterval(() => {
                localeKoKr.current = document.querySelector('.locale-ko-KR');
                is12Hour.current = document.querySelector("[class^=time-period]") != null
                const now = new Date();
                console.log(is12Hour.current);
                setTime(formatTime(now));
            }, 1000);

            // 컴포넌트 언마운트 시 타이머 정리
            return () => clearInterval(timer);
        }, []);

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