import api from './api';

export const comicService = {
    getAll: async () => {
        const response = await api.get('/comicbooks');
        return response.data;
    },

    create: async (data) => {
        const response = await api.post('/comicbooks', data);
        return response.data;
    },

    update: async (id, data) => {
        const response = await api.put(`/comicbooks/${id}`, data);
        return response.data;
    },

    delete: async (id) => {
        const response = await api.delete(`/comicbooks/${id}`);
        return response.data;
    },
};
