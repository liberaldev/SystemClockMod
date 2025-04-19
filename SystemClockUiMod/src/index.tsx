import { ModRegistrar } from "cs2/modding";

import {SystemTime} from "./system-time";

const register: ModRegistrar = (moduleRegistry) => {
    /*console.log(
        'List of everything toolbar',
        moduleRegistry.find(/toolbar/)
    );*/
    moduleRegistry.extend('game-ui/game/components/toolbar/bottom/happiness-field/happiness-field.tsx', 'HappinessField', SystemTime);
}

export default register;