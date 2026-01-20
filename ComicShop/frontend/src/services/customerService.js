import api from './api';

export const customerService = {
    register: async (data) => {
        // data: { fullName, phoneNumber }
        const response = await api.post('/customers/register', data);
        return response.data;
    },
};
