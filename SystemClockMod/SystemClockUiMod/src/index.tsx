import { ModRegistrar } from "cs2/modding";
import { SystemTime } from "./mods/system-time";

const register: ModRegistrar = (moduleRegistry) => {
    moduleRegistry.extend('game-ui/game/components/toolbar/bottom/happiness-field/happiness-field.tsx', 'HappinessField', SystemTime);
}

export default register;