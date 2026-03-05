import axios from "axios";

const API_BASE = "http://localhost:7071/api";

export const getAssetsByPlayer = async () => {
    return await axios.get(`${API_BASE}/getassetsbyplayer`);
};