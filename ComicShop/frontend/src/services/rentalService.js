import api from './api';

export const rentalService = {
    getAll: async () => {
        const response = await api.get('/rentals/rentals');
        return response.data;
    },

    createRental: async (data) => {
        // data: { customerId, rentalDetails: [{ comicBookId, quantity: 1 }] }
        const response = await api.post('/rentals', data);
        return response.data;
    },

    returnRental: async (rentalDetailId) => {
        // Assuming specific endpoint or logic, but for now just placeholder
        // Per swagger, maybe PUT /rentals/{id}? Or DELETE?
        // Swagger showed: GET /rentals/rentals, POST /rentals. 
        // I'll stick to listing and creating for now.
        return null;
    }
};
