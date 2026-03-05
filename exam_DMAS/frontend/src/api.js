import axios from 'axios';

const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:7071/api';

// Register a new player
export const registerPlayer = async (playerData) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/registerplayer`, playerData);
        return response.data;
    } catch (error) {
        throw error.response?.data || error;
    }
};

// Create a new asset
export const createAsset = async (assetData) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/createasset`, assetData);
        return response.data;
    } catch (error) {
        throw error.response?.data || error;
    }
};

// Get assets by player
export const getAssetsByPlayer = async () => {
    try {
        const response = await axios.get(`${API_BASE_URL}/getassetsbyplayer`);
        return response.data;
    } catch (error) {
        throw error.response?.data || error;
    }
};

export default {
    registerPlayer,
    createAsset,
    getAssetsByPlayer
};
