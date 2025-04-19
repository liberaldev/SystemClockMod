import React, {useEffect, useState} from "react";
import styles from "./system-time.module.css";
import {ModuleRegistryExtend} from "cs2/modding";

export const SystemTime : ModuleRegistryExtend = (Component) => {
    return (props) => {
        const { children, ...otherProps } = props || {};
        
        const [time, setTime] = useState("");

        // 사용자 시간제 감지 (단순히 12시간제인지 여부를 추정)
        const prefers12Hour = (() => {
            const test = new Date().toLocaleTimeString();
            return /AM|PM/i.test(test);
        })();

        // 시간 포맷팅 함수
        const formatTime = (date: Date, is12Hour: boolean) => {
            let hours = date.getHours();
            const minutes = date.getMinutes();
            const seconds = date.getSeconds();

            // 12시간제인 경우
            let ampm = "";
            if (is12Hour) {
                ampm = hours >= 12 ? "오후" : "오전";
                hours = hours % 12;
                if (hours === 0) hours = 12; // 0시를 12시로
            }

            // 두 자리 숫자 포맷
            const pad = (num: number) => String(num).padStart(2, "0");

            return `${is12Hour ? ampm + " " : ""}${pad(hours)}:${pad(minutes)}:${pad(seconds)}`;
        };

        useEffect(() => {
            const timer = setInterval(() => {
                const now = new Date();
                setTime(formatTime(now, true));
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